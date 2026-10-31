using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using BOL;
using DAL;
using Entidades;

namespace UI
{
    public partial class FrmProveedor : Form
    {
        private readonly ProveedorBol _bol;
        private int _idActual;
        private bool _cargandoGrid; //Evita las seleccion miestras se renderiza los grid

        //Definir constante
        Color coloarActivo = Color.LightGoldenrodYellow;
        Color colorBase = Color.White;

        //Constructor
        public FrmProveedor(ProveedorBol bol)
        {
            InitializeComponent(); //Construir UI

            _bol = bol;

            //Enlace EVENTO...
            Load += async (_, _) => await CargarAsync();
            btnBuscar.Click += async (_, _) => await CargarAsync();
        }

        private async Task CargarListAsync()
        {
            var list = await _bol.ListarAsync(txtBuscar.Text);

            _cargandoGrid = true;
            grid.DataSource = list;
            grid.ClearSelection();
            _cargandoGrid = false;

            configurarDrid();
            LimpiarFormulario();
        }

        private async Task EjecutarAsync(Func<Task> action)
        {
            Control? foco = null;

            try
            {
                UseWaitCursor = true;
                Enabled = true;
                await action();
            }
            catch (ReglaNegocioException ex)
            {
                MessageBox.Show(ex.Message, "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                foco = ex.Campo switch
                {
                    nameof(Proveedor.RazonSocial) => txtRazonSocial,
                    nameof(Proveedor.Ruc) => txtRuc,
                    nameof(Proveedor.Telefono) => txtTelefono,
                    nameof(Proveedor.Correo) => txtCorreo,
                    _ => null
                };
            }
            catch (ApiException ex)
            {
                MessageBox.Show(ex.Message, "Error de validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("El servidor esta demasiado ocupado", "Tiempo ocupado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HttpIOException ex)
            {
                MessageBox.Show(ex.Message, "No hay comunicacion en el server", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Enabled = true;
                UseWaitCursor = false;
            }

            foco?.Focus(); //Elemento activo
        }

        private Task CargarAsync() => EjecutarAsync(CargarListAsync);

        private async Task GuardarAsync()
        {
            var proveedor = new Proveedor
            {
                Id = _idActual,
                RazonSocial = txtRazonSocial.Text,
                Ruc = txtRuc.Text,
                Telefono = txtTelefono.Text,
                Correo = txtCorreo.Text,
            };

            await EjecutarAsync(async () =>
            {
                await _bol.GuardarAsync(proveedor);
                await CargarListAsync();
                LimpiarFormulario();

                MessageBox.Show("Proveedor guarda", "Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }

        private void LimpiarFormulario()
        {
            _idActual = 0;
            txtRazonSocial.Clear();
            txtRuc.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            grid.ClearSelection();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await GuardarAsync();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            txtRazonSocial.Focus();
        }

        private void txtRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloDigitos(sender, e);
        }

        /// <summary>
        /// Bloquea Cualquier caracter ingresado e excepcion de numeros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void SoloDigitos(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloDigitos(sender, e);
        }

        private void configurarDrid()
        {
            if (grid.Columns.Count == 0) return;

            //ancho columnas
            grid.Columns[0].Visible = false;
            grid.Columns[1].Width = 240;
            grid.Columns[2].Width = 140;
            grid.Columns[3].Width = 140;
            grid.Columns[4].Width = 200;

            //Filas Alternadas
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan;
        }

        private void txtRazonSocial_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox tb) tb.BackColor = coloarActivo;
        }

        private void txtRazonSocial_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox tb) tb.BackColor = colorBase;
        }

        private void grid_SelectionChanged(object sender, EventArgs e)
        {
            //Este evento se ejecuta cuando cambias la fila activa
            MostrarSeleccionado();
        }

        private void MostrarSeleccionado()
        {
            if (_cargandoGrid || grid.SelectedRows.Count == 0) return;
            if (grid.SelectedRows[0].DataBoundItem is not Proveedor p) return; //API

            _idActual = p.Id;
            txtRazonSocial.Text = p.RazonSocial;
            txtRuc.Text = p.Ruc;
            txtTelefono.Text = p.Telefono;
            txtCorreo.Text = p.Correo;
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            await EliminarAsync();
        }

        private async Task EliminarAsync()
        {
            if (_idActual == 0) return;

            string datosProveedor = $"{txtRazonSocial.Text}";
            DialogResult rpta = MessageBox.Show("¿Desea Eliminar", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rpta == DialogResult.No) return;

            var id = _idActual;

            //1. Eliminar > 2 . Actualizar
            await EjecutarAsync(async () =>
            {
                await _bol.EliminarAsync(id);
                await CargarListAsync();
            });

            LimpiarFormulario();
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            await CargarAsync();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            //Keypress (presiona, Libera) vs keyDown (boton presionado)
            //Identificar el enter
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnBuscar.PerformClick();
            }
        }
    }
}

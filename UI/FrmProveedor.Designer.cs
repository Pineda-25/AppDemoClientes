namespace UI
{
    partial class FrmProveedor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            grid = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            txtRazonSocial = new TextBox();
            txtRuc = new TextBox();
            txtCorreo = new TextBox();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            txtTelefono = new TextBox();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            SuspendLayout();
            // 
            // grid
            // 
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToOrderColumns = true;
            grid.AllowUserToResizeColumns = false;
            grid.AllowUserToResizeRows = false;
            grid.BackgroundColor = SystemColors.ActiveCaption;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid.Location = new Point(12, 37);
            grid.MultiSelect = false;
            grid.Name = "grid";
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.Size = new Size(761, 176);
            grid.TabIndex = 18;
            grid.SelectionChanged += grid_SelectionChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(48, 15);
            label1.TabIndex = 0;
            label1.Text = "Buscar :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 227);
            label2.Name = "label2";
            label2.Size = new Size(76, 15);
            label2.TabIndex = 3;
            label2.Text = "Razón Social:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(260, 227);
            label3.Name = "label3";
            label3.Size = new Size(33, 15);
            label3.TabIndex = 5;
            label3.Text = "RUC:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(512, 227);
            label4.Name = "label4";
            label4.Size = new Size(46, 15);
            label4.TabIndex = 7;
            label4.Text = "Correo:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(13, 291);
            label5.Name = "label5";
            label5.Size = new Size(55, 15);
            label5.TabIndex = 9;
            label5.Text = "Teléfono:";
            // 
            // txtRazonSocial
            // 
            txtRazonSocial.Location = new Point(13, 245);
            txtRazonSocial.Name = "txtRazonSocial";
            txtRazonSocial.Size = new Size(228, 23);
            txtRazonSocial.TabIndex = 4;
            txtRazonSocial.Enter += txtRazonSocial_Enter;
            txtRazonSocial.Leave += txtRazonSocial_Leave;
            // 
            // txtRuc
            // 
            txtRuc.Location = new Point(260, 245);
            txtRuc.MaxLength = 11;
            txtRuc.Name = "txtRuc";
            txtRuc.Size = new Size(234, 23);
            txtRuc.TabIndex = 6;
            txtRuc.Enter += txtRazonSocial_Enter;
            txtRuc.KeyPress += txtRuc_KeyPress;
            txtRuc.Leave += txtRazonSocial_Leave;
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(512, 245);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(261, 23);
            txtCorreo.TabIndex = 8;
            txtCorreo.Enter += txtRazonSocial_Enter;
            txtCorreo.Leave += txtRazonSocial_Leave;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(66, 6);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(492, 23);
            txtBuscar.TabIndex = 1;
            txtBuscar.KeyDown += txtBuscar_KeyDown;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(564, 6);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(75, 23);
            btnBuscar.TabIndex = 2;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(13, 310);
            txtTelefono.MaxLength = 15;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(228, 23);
            txtTelefono.TabIndex = 10;
            txtTelefono.Enter += txtRazonSocial_Enter;
            txtTelefono.KeyPress += txtTelefono_KeyPress;
            txtTelefono.Leave += txtRazonSocial_Leave;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(13, 376);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(118, 23);
            btnNuevo.TabIndex = 15;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(155, 376);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(97, 23);
            btnGuardar.TabIndex = 16;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(273, 376);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(119, 23);
            btnEliminar.TabIndex = 17;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // FrmProveedor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(btnNuevo);
            Controls.Add(btnBuscar);
            Controls.Add(txtCorreo);
            Controls.Add(txtTelefono);
            Controls.Add(txtBuscar);
            Controls.Add(txtRuc);
            Controls.Add(txtRazonSocial);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(grid);
            Name = "FrmProveedor";
            Text = "FrmProveedor";
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView grid;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtRazonSocial;
        private TextBox txtRuc;
        private TextBox txtCorreo;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private TextBox txtTelefono;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEliminar;
    }
}

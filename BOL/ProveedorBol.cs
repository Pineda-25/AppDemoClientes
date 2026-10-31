using System;
using System.Collections.Generic;
using System.Text;
using Entidades;
using DAL;
using System.Text.RegularExpressions;

namespace BOL
{
    /// <summary>
    /// La UI solo puede conversar con esta clases
    /// </summary>
    public class ProveedorBol
    {
        //Utilizaremos la interfaz para conocer los metodos a plicar
        private readonly IProveedorDal _dal;
        public ProveedorBol(IProveedorDal dal) => _dal = dal;

        /// <summary>
        /// Este metodo sirve la accion listar para la UI , tomando como refecnia  la clase BOL y su interfaz
        /// </summary>
        /// <param name="buscar"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<List<Proveedor>> ListarAsync(string? buscar = null, CancellationToken ct = default) => _dal.ListarAsync(buscar?.Trim(), ct);

        /// <summary>
        /// Buscador de proveedor por ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<Proveedor?> ObtenerAsync(int id, CancellationToken ct = default) => _dal.ObtenerAsync(id, ct);

        public async Task GuardarAsync(Proveedor proveedor, CancellationToken ct = default)
        {
            //Validacion
            Normalizar(proveedor);            //Quitar los espacios en blanco
            Validar(proveedor);

            //La UI nos envia un ID = 0 (   Nuevo
            //UI nos envia un id = 7 (actualizar
            if (proveedor.Id == 0) await _dal.CrearAsync(proveedor, ct);
            else await _dal.ActualizarAsync(proveedor, ct);
        }

        public Task EliminarAsync(int id, CancellationToken ct = default)
        {
            //En el UI manejamos un ID = 0 (No hay un registro activo)
            if (id == 0) { throw new ReglaNegocioException("Selecciona un proveedor para eliminar"); }
            return _dal.Eliminar(id, ct);
        }

        private static void Normalizar(Proveedor proveedor)
        {
            proveedor.RazonSocial = proveedor.RazonSocial.Trim();
            proveedor.Ruc = proveedor.Ruc.Trim();
            proveedor.Telefono = proveedor.Telefono.Trim();
            proveedor.Correo = proveedor.Correo.Trim();
        }

        private static void Validar(Proveedor proveedor)
        {
            if (proveedor.RazonSocial.Length == 0) { throw new ReglaNegocioException("Ingresar la razón social ", nameof(Proveedor.RazonSocial)); }
            if (proveedor.Ruc.Length != 11) { throw new ReglaNegocioException("Escriba correctamente RUC", nameof(Proveedor.Ruc)); }

            if (!Regex.IsMatch(proveedor.Ruc, @"^\d{11}$")) { throw new ReglaNegocioException("El RUC debe tener 11 digitos", nameof(Proveedor.Ruc)); }
            if (!string.IsNullOrEmpty(proveedor.Telefono) && proveedor.Telefono.Length < 7) { throw new ReglaNegocioException("El telefono debe ser valido ", nameof(Proveedor.Telefono)); }
        }
    }
}

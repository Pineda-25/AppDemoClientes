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
    public class ClienteBol
    {
        //Utilizaremos la interfaz para conocer los metodos a plicar
        private readonly IClienteDal _dal;
        public ClienteBol(IClienteDal dal) => _dal = dal;
        
        /// <summary>
        /// Este metodo sirve la accion listar para la UI , tomando como refecnia  la clase BOL y su interfaz
        /// </summary>
        /// <param name="buscar"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<List<Cliente>> ListarAsync(string? buscar = null, CancellationToken ct = default) =>_dal.ListarAsync(buscar?.Trim(), ct);
        
        /// <summary>
        /// Buscador de cliente por ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<Cliente?> ObtenerAsync(int id, CancellationToken ct = default) => _dal.ObtenerAsync(id, ct);

        public async Task GuardarAsync(Cliente cliente, CancellationToken ct = default) 
        {
            //Validacion
            Normalizar(cliente);            //Quitar los espacios en blanco
            Validar(cliente);

            //La UI nos envia un ID = 0 (   Nuevo
            //UI nos envia un id = 7 (actualizar
            if (cliente.Id == 0) await _dal.CrearAsync(cliente, ct);
            else await _dal.ActualizarAsync(cliente, ct);
        }

        public Task EliminarAsync(int id, CancellationToken ct = default) 
        {
            //En el UI manejamos un ID = 0 (No hay un registro activo)
            if (id == 0) { throw new ReglaNegocioException("Selecciona un cliente para eliminar"); }
            return _dal.Eliminar(id, ct);
        }

        private static void Normalizar(Cliente cliente)
        {
            cliente.Apellidos = cliente.Apellidos.Trim();
            cliente.Nombres = cliente.Nombres.Trim();
            cliente.Dni = cliente.Dni.Trim();
            cliente.Telefono = cliente.Telefono.Trim();
            cliente.Direccion = cliente.Direccion.Trim();
            cliente.Correo = cliente.Correo.Trim();
        }

        private static void Validar(Cliente cliente)
        {
            if (cliente.Apellidos.Length == 0) { throw new ReglaNegocioException("Ingresar los apellidos ", nameof(Cliente.Apellidos)); }
            if (cliente.Nombres.Length == 0) { throw new ReglaNegocioException("Ingrese su nombre ", nameof(Cliente.Nombres)); }
            if (cliente.Dni.Length != 8) { throw new ReglaNegocioException("Escriab correctamente DNI", nameof(Cliente.Dni)); }

            if (!Regex.IsMatch(cliente.Dni, @"^\d{8}$")) { throw new ReglaNegocioException("El dni debe tener 8 digitos", nameof(Cliente.Dni)); }
            if (cliente.Telefono.Length != 9) { throw new ReglaNegocioException("El telefono debe tener 9 digitos ", nameof(cliente.Telefono)); }
        
        }
    }

}

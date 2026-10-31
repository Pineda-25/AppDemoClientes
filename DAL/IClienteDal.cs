using System;
using System.Collections.Generic;
using System.Text;

//Invocar a la capa de entidades
using Entidades;

namespace DAL
{
    public interface IClienteDal
    {
        //Listar
        //Se utilizara para consultar todas los datos  | buscador
        Task<List<Cliente>> ListarAsync(string? buscar = null, CancellationToken ct = default);

        //Obtener <Clase?>
        //Podemos retonar una clase o un null
        Task<Cliente?> ObtenerAsync(int id, CancellationToken ct = default);

        //Crear
        Task<Cliente?> CrearAsync(Cliente cliente, CancellationToken ct = default);

        //Actualizar
        //No retorna valor , pero si define un metodo asincrono
        Task ActualizarAsync(Cliente cliente, CancellationToken ct = default);

        //Eliminar
        Task Eliminar(int id, CancellationToken ct = default);
    }
}

using Entidades;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Net;

namespace DAL
{
    //  Java            :Herencia extends
    //Csharp            :Herencia ":"
    
    /// <summary>
    /// Acceso a datos de clientes. En esta arquitectura el acceso a la BD es por API REST
    /// Se utilizare HTTPClient en lugar de SQLCONNECTION
    /// </summary>

    public class ClienteDal : IClienteDal
    {

        //Objetos privados
        private const string RUTA = "api/clientes";
        private readonly HttpClient _http;

        //Constructor (nombre de la clase)
        public ClienteDal(HttpClient http)
        {
            _http = http;
        }


        
        public async Task ActualizarAsync(Cliente cliente, CancellationToken ct = default)
        {
            using var resp = await _http.PutAsJsonAsync($"{RUTA}/{cliente.Id}", cliente, ct);
            await AsegurarExitoAsync(resp, ct);
        }

        public async Task<Cliente?> CrearAsync(Cliente cliente, CancellationToken ct = default)
        {
            using var resp = await _http.PostAsJsonAsync(RUTA, cliente, ct);
            await AsegurarExitoAsync(resp, ct);
            return await resp.Content.ReadFromJsonAsync<Cliente>(ct);
        }

        public async Task Eliminar(int id, CancellationToken ct = default)
        {
            using var resp = await _http.DeleteAsync($"{RUTA}/{id}", ct);
            await AsegurarExitoAsync(resp, ct);
        }

        /// <summary>
        /// Retorna una lista de clientes utilizando un flitro de manera opcional
        /// </summary>
        /// <param name="buscar"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<List<Cliente>> ListarAsync(string? buscar = null, CancellationToken ct = default)
        {
            //¿Se nos envio datos en la variables buscar?
            var url = string.IsNullOrWhiteSpace(buscar) ? RUTA : $"{RUTA}?buscar={Uri.EscapeDataString(buscar)}";

            using var resp =  await _http.GetAsync(url, ct);
            await AsegurarExitoAsync(resp, ct);

            return await resp.Content.ReadFromJsonAsync<List<Cliente>>(ct) ?? new();
        }

        public async Task<Cliente?> ObtenerAsync(int id, CancellationToken ct = default)
        {
            using var resp = await _http.GetAsync($"{RUTA}/{id}", ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) { return null; }
            await AsegurarExitoAsync(resp, ct);

            //Es una busqueda por id , solo devolvemos un objeto
            return await resp.Content.ReadFromJsonAsync<Cliente>(ct);
        }


        private static async Task AsegurarExitoAsync(HttpResponseMessage resp, CancellationToken ct)
        {
            //serve code 2xx
            if (resp.IsSuccessStatusCode) return;

            //¿Y si algo no funciono?...
            var cuerpo = await resp.Content.ReadAsStringAsync(ct);
            var detalle = string.IsNullOrWhiteSpace(cuerpo) ? resp.ReasonPhrase : cuerpo;

            //Excepcion - Error (4xx - 5xx)
            throw new ApiException($"El servidor respondió: {(int)resp.StatusCode}: {detalle}");
        }
    }
}

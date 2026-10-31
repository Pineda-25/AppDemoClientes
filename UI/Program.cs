using System;
using System.Net.Http;
using System.Windows.Forms;
using BOL;
using DAL;

namespace UI
{
    internal static class Program
    {
        /// <summary>
        ///  Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Configurar cliente HTTP base hacia la API REST
            var http = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7177/"),
                Timeout = TimeSpan.FromSeconds(15)
            };

            // Inyección de dependencias para Clientes
            var clienteDal = new ClienteDal(http);
            var clienteBol = new ClienteBol(clienteDal);

            // Inyección de dependencias para Proveedores
            var proveedorDal = new ProveedorDal(http);
            var proveedorBol = new ProveedorBol(proveedorDal);

            // Iniciar aplicación desde el menú principal
            Application.Run(new FrmMain(clienteBol, proveedorBol));
        }
    }
}

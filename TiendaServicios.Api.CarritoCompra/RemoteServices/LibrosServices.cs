namespace TiendaServicios.Api.CarritoCompra.RemoteServices
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using TiendaServicios.Api.CarritoCompra.RemoteInterfaces;
    using TiendaServicios.Api.CarritoCompra.RemoteModels;

    public class LibrosServices : ILibrosServices
    {
        private readonly IHttpClientFactory httpClient;
        private readonly ILogger<LibrosServices> logger;

        public LibrosServices(IHttpClientFactory httpClient, ILogger<LibrosServices> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
        }

        public async Task<(bool resultado, LibroRemote libroRemote, string errorMessage)> GetLibro(Guid libroId)
        {
            try
            {
                using (var nombreCliente = this.httpClient.CreateClient("Libros"))
                {
                    using (var response = await nombreCliente.GetAsync($"api/Libro/{libroId}"))
                    {
                        //if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        if (response.IsSuccessStatusCode)
                        {
                            var json = await response.Content.ReadAsStringAsync();
                            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                            var libro = JsonSerializer.Deserialize<LibroRemote>(json, options);

                            return (true, libro, string.Empty);
                        }
                        return (false, null, response.ReasonPhrase);
                    }
                }
            }
            catch(Exception e)
            {
                this.logger?.LogError(e.Message);
                return (false, null, e.Message);
            }
        }
    }
}

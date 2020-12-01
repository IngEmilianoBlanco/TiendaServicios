namespace TiendaServicios.Api.CarritoCompra.RemoteServices
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using TiendaServicios.Api.CarritoCompra.RemoteInterfaces;
    using TiendaServicios.Api.CarritoCompra.RemoteModels;

    public class AutorServices : IAutorServices
    {
        private readonly IHttpClientFactory httpClient;
        private readonly ILogger<AutorServices> logger;

        public AutorServices(IHttpClientFactory httpClient, ILogger<AutorServices> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
        }
        public async Task<(bool resultado, AutorRemote autorRemote, string errorMessage)> GetAutor(Guid autorId)
        {
            try
            {
                using (var cliente = this.httpClient.CreateClient("Autor"))
                {
                    using (var response = await cliente.GetAsync($"api/Autor/{autorId}"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var json = await response.Content.ReadAsStringAsync();
                            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                            var autor = JsonSerializer.Deserialize<AutorRemote>(json, options);

                            return (true, autor, string.Empty);
                        }
                        return (false, null, response.ReasonPhrase);
                    }

                }
            }
            catch(Exception error)
            {
                this.logger?.LogError(error.Message);
                return (false, null, error.Message);
            }
        }
    }
}

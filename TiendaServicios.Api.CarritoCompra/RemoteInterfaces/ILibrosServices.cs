namespace TiendaServicios.Api.CarritoCompra.RemoteInterfaces
{
    using System;
    using System.Threading.Tasks;
    using TiendaServicios.Api.CarritoCompra.RemoteModels;

    public interface ILibrosServices
    {
        Task<(bool resultado, LibroRemote libroRemote, string errorMessage)> GetLibro(Guid libroId);
    }
}

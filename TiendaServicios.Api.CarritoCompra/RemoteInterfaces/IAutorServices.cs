namespace TiendaServicios.Api.CarritoCompra.RemoteInterfaces
{
    using System;
    using System.Threading.Tasks;
    using TiendaServicios.Api.CarritoCompra.RemoteModels;

    public interface IAutorServices
    {
        Task<(bool resultado, AutorRemote autorRemote, string errorMessage)> GetAutor(Guid autorId);
    }
}

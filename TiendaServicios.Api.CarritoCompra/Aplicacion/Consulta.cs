namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using TiendaServicios.Api.CarritoCompra.Persistencia;
    using TiendaServicios.Api.CarritoCompra.RemoteInterfaces;
    using TiendaServicios.Api.CarritoCompra.RemoteModels;

    public class Consulta
    {
        public class Ejecuta: IRequest<CarritoDto>
        {
            public int CarritoSesionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDto>
        {
            private readonly ContextoCarrito contexto;
            private readonly ILibrosServices services;
            private readonly IAutorServices autorServices;

            public Manejador(ContextoCarrito contexto, ILibrosServices services, IAutorServices autorServices)
            {
                this.contexto = contexto;
                this.services = services;
                this.autorServices = autorServices;
            }

            public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = await this.contexto.CarritoSesion.FirstOrDefaultAsync(x => x.CarritoSesionId == request.CarritoSesionId);
                var carritoSesionDetalles = await this.contexto.CarritoSesionDetalle.Where(x => x.CarritoSesionId == request.CarritoSesionId).ToListAsync();

                var listaCarritoDto = new List<CarritoDetalleDto>();
                foreach(var libro in carritoSesionDetalles)
                {
                    var response = await this.services.GetLibro(new Guid(libro.ProductoSeleccionado));

                    if (response.resultado)
                    {
                        var responseAutor = await this.autorServices.GetAutor(new Guid(response.libroRemote.AutorLibro.ToString()));

                        listaCarritoDto.Add(new CarritoDetalleDto()
                        {
                            TituloLibro = response.libroRemote.Titulo,
                            FechaPublicacion =  response.libroRemote.FechaPublicacion,
                            LibroId = response.libroRemote.LibreriaMaterialId,
                            AutorLibro = responseAutor.resultado ? $"{responseAutor.autorRemote.Nombre} {responseAutor.autorRemote.Apellido}" : string.Empty
                        });    
                    }
                }

                return new CarritoDto() 
                { 
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacionSesion = carritoSesion.FechaCreacion,
                    ListadoLibros = listaCarritoDto
                };
            }
        }

    }
}

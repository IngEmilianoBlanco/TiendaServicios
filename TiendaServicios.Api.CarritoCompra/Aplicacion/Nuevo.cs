namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using TiendaServicios.Api.CarritoCompra.Modelos;
    using TiendaServicios.Api.CarritoCompra.Persistencia;

    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public DateTime FechaCreacionSesion { get; set; }
            public List<string> ProductoLista { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoCarrito contextoCarrito;

            public Manejador(ContextoCarrito contextoCarrito)
            {
                this.contextoCarrito = contextoCarrito;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion()
                {
                    FechaCreacion = request.FechaCreacionSesion
                };

                await this.contextoCarrito.CarritoSesion.AddAsync(carritoSesion);
                var value = await this.contextoCarrito.SaveChangesAsync();

                if(value == 0)
                {
                    throw new Exception("Errores en la inserción del carrito de compras");
                }

                int sesionId = carritoSesion.CarritoSesionId;

                foreach(var x in request.ProductoLista)
                {
                    var sesionDetalle = new CarritoSesionDetalle()
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = sesionId,
                        ProductoSeleccionado = x
                    };

                    await this.contextoCarrito.CarritoSesionDetalle.AddAsync(sesionDetalle);
                }

                var value2 = await this.contextoCarrito.SaveChangesAsync();

                if(value2 > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el detalle del carrito de compras");
            }
        }

    }
}

using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Modelos;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Titulo { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public Guid? AutorLibro { get; set; }
        }

        public class EjecucionValidacion : AbstractValidator<Ejecuta>
        {
            public EjecucionValidacion()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
                RuleFor(x => x.AutorLibro).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly Contextolibreria contextolibreria;

            public Manejador(Contextolibreria contextolibreria)
            {
                this.contextolibreria = contextolibreria;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libro = this.contextolibreria.LibreriaMaterial.Add(new LibreriaMaterial 
                { 
                    Titulo = request.Titulo,
                    FechaPublicacion = request.FechaPublicacion,
                    AutorLibro = request.AutorLibro
                });

                var unit = await this.contextolibreria.SaveChangesAsync();

                if(unit > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo crear el libro");
            }
        }
    }
}

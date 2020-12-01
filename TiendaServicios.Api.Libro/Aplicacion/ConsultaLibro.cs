
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Modelos;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class ConsultaLibro
    {
        public class LibroUnico : IRequest<LibreriaMaterialDtocs> 
        {
            public Guid? libroGUID { get; set; }
        }

        public class Manejador : IRequestHandler<LibroUnico, LibreriaMaterialDtocs>
        {
            private readonly Contextolibreria contextolibreria;
            private readonly IMapper mapper;

            public Manejador(Contextolibreria contextolibreria, IMapper mapper)
            {
                this.contextolibreria = contextolibreria;
                this.mapper = mapper;
            }

            public async Task<LibreriaMaterialDtocs> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await this.contextolibreria.LibreriaMaterial.FirstOrDefaultAsync(x => x.LibreriaMaterialId == request.libroGUID);
                var libroDto = this.mapper.Map<LibreriaMaterial, LibreriaMaterialDtocs>(libro);
                return libroDto;
            }
        }
    }
}

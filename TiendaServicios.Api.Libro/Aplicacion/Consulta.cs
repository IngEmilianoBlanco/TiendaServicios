using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Modelos;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<LibreriaMaterialDtocs>> 
        {
            public Ejecuta() { }
        }

        public class Manejador : IRequestHandler<Ejecuta, List<LibreriaMaterialDtocs>>
        {
            private readonly Contextolibreria contextolibreria;
            private readonly IMapper mapper;

            public Manejador(Contextolibreria contextolibreria, IMapper mapper)
            {
                this.contextolibreria = contextolibreria;
                this.mapper = mapper;
            }

            public async Task<List<LibreriaMaterialDtocs>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libros = await this.contextolibreria.LibreriaMaterial.ToListAsync();
                var librosDto = this.mapper.Map<List<LibreriaMaterial>, List<LibreriaMaterialDtocs>>(libros);
                return librosDto;
            }
        }
    }
}

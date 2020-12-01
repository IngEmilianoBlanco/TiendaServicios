namespace TiendaServicios.Api.Autor.Aplicacion
{
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using TiendaServicios.Api.Autor.Modelos;
    using TiendaServicios.Api.Autor.Persistencia;
    public class Consulta
    {
        public class ListaAutor : IRequest<List<AutorLibroDto>> { }

        public class Manejador : IRequestHandler<ListaAutor, List<AutorLibroDto>>
        {
            private readonly ContextoAutor contexto;
            private readonly IMapper mapper;

            public Manejador(ContextoAutor contexto, IMapper mapper)
            {
                this.contexto = contexto;
                this.mapper = mapper;
            }
            public async Task<List<AutorLibroDto>> Handle(ListaAutor request, CancellationToken cancellationToken)
            {
                var autores = await this.contexto.AutorLibro.ToListAsync();
                var autoresDto = this.mapper.Map<List<AutorLibro>, List<AutorLibroDto>>(autores);
                return autoresDto;
            }
        }
    }
}

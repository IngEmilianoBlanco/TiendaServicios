namespace TiendaServicios.Api.Autor.Aplicacion
{
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Threading.Tasks;
    using TiendaServicios.Api.Autor.Modelos;
    using TiendaServicios.Api.Autor.Persistencia;
    public class ConsultaFiltro
    {
        public class AutorUnico : IRequest<AutorLibroDto> 
        {
            public string AutorLibroGuid { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnico, AutorLibroDto>
        {
            private readonly ContextoAutor contexto;
            private readonly IMapper mapper;

            public Manejador(ContextoAutor contexto, IMapper mapper)
            {
                this.contexto = contexto;
                this.mapper = mapper;
            }

            public async Task<AutorLibroDto> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await this.contexto.AutorLibro.FirstOrDefaultAsync(x => x.AutorLibroGuid == request.AutorLibroGuid);
                var autorDto = this.mapper.Map<AutorLibro, AutorLibroDto>(autor);
                return autorDto;
            }
        }
    }
}

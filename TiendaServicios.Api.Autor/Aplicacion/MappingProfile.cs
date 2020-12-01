namespace TiendaServicios.Api.Autor.Aplicacion
{
    using AutoMapper;
    using TiendaServicios.Api.Autor.Modelos;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AutorLibro, AutorLibroDto>();
        }
    }
}

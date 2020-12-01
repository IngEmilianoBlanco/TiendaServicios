namespace TiendaServicios.Api.Libro.Tests.Mapper
{
    using AutoMapper;
    using TiendaServicios.Api.Libro.Modelos;
    using TiendaServicios.Api.Libro.Aplicacion;

    public class MappingTest : Profile
    {
        public MappingTest()
        {
            CreateMap<LibreriaMaterial, LibreriaMaterialDtocs>();
        }
    }
}

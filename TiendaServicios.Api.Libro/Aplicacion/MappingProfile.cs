using AutoMapper;
using TiendaServicios.Api.Libro.Modelos;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<LibreriaMaterial, LibreriaMaterialDtocs>();
        }
    }
}

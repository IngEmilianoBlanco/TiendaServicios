namespace TiendaServicios.Api.Autor.Aplicacion
{
    using System;
    public class AutorLibroDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string AutorLibroGuid { get; set; }
    }
}

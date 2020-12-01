namespace TiendaServicios.Api.Autor.Modelos
{
    using System;
    using System.Collections.Generic;

    public class AutorLibro
    {
        public int AutorLibroId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public List<GradoAcademico> ListaGradoAcademico { get; set; }
        public string AutorLibroGuid { get; set; }
    }
}

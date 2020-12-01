namespace TiendaServicios.Api.Autor.Modelos
{
    using System;
    public class GradoAcademico
    {
        public int GradoAcademicoId { get; set; }
        public string String { get; set; }
        public string CentroAcademico { get; set; }
        public DateTime? FechaGrado { get; set; }
        public int AutorLibroId { get; set; }
        public AutorLibro Autor { get; set; }
        public string GradoAcademicoGuid { get; set; }
    }
}

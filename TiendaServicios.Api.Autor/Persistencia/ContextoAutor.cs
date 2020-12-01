namespace TiendaServicios.Api.Autor.Persistencia
{
    using Microsoft.EntityFrameworkCore;
    using TiendaServicios.Api.Autor.Modelos;
    public class ContextoAutor: DbContext
    {
        public ContextoAutor(DbContextOptions<ContextoAutor> dbContext)
            : base(dbContext) { }

        public DbSet<AutorLibro> AutorLibro { get; set; }
        public DbSet<GradoAcademico> GradoAcademico { get; set; }
    }
}

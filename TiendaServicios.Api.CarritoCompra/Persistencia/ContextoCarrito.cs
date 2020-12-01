namespace TiendaServicios.Api.CarritoCompra.Persistencia
{
    using Microsoft.EntityFrameworkCore;
    using TiendaServicios.Api.CarritoCompra.Modelos;

    public class ContextoCarrito : DbContext
    {
        public ContextoCarrito(DbContextOptions<ContextoCarrito> dbContext)
            : base(dbContext) { }

        public DbSet<CarritoSesion> CarritoSesion { get; set; }
        public DbSet<CarritoSesionDetalle> CarritoSesionDetalle { get; set; }
    }
}

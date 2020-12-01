using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Modelos;

namespace TiendaServicios.Api.Libro.Persistencia
{
    public class Contextolibreria : DbContext
    {
        public Contextolibreria() { }
        public Contextolibreria(DbContextOptions<Contextolibreria> dbContext)
            : base(dbContext) { }

        public virtual DbSet<LibreriaMaterial> LibreriaMaterial { get; set; }
    }
}

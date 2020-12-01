namespace TiendaServicios.Api.Libro.Tests
{
    using AutoMapper;
    using GenFu;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using TiendaServicios.Api.Libro.Aplicacion;
    using TiendaServicios.Api.Libro.Modelos;
    using TiendaServicios.Api.Libro.Persistencia;
    using TiendaServicios.Api.Libro.Tests.AsyncInterfaces;
    using TiendaServicios.Api.Libro.Tests.Mapper;
    using Xunit;

    public class LibrosServiceTest
    {
        private IEnumerable<LibreriaMaterial> ObtenerDataPrueba()
        {
            A.Configure<LibreriaMaterial>()
                .Fill(x => x.Titulo).AsArticleTitle()
                .Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid(); });

            var lista = A.ListOf<LibreriaMaterial>(30);
            lista[0].LibreriaMaterialId = Guid.Empty;

            return lista;
        }

        private Mock<Contextolibreria> BuildContext()
        {
            var data = this.ObtenerDataPrueba().AsQueryable();

            var dbSet = new Mock<DbSet<LibreriaMaterial>>();
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(data.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<LibreriaMaterial>(data.GetEnumerator()));

            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibreriaMaterial>(data.Provider));

            var contexto = new Mock<Contextolibreria>();
            contexto.Setup(x => x.LibreriaMaterial).Returns(dbSet.Object);
            return contexto;
        }

        [Fact]
        public async void GetLibros()
        {
            var mockContexto = this.BuildContext();
            var config = new MapperConfiguration(x => 
            {
                x.AddProfile(new MappingTest());
            });
            var mockIMapper = config.CreateMapper();
            var manejador = new Consulta.Manejador(mockContexto.Object, mockIMapper);
            var request = new Consulta.Ejecuta();
            var lista = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(lista.Any());
        }

        [Fact]
        public async void GetLibroId()
        {
            var mockContexto = this.BuildContext();
            var configMapper = new MapperConfiguration(x => 
            {
                x.AddProfile(new MappingTest());
            });
            var mapper = configMapper.CreateMapper();

            var libroUnico = new ConsultaLibro.LibroUnico() { libroGUID = Guid.Empty };
            var manejador = new ConsultaLibro.Manejador(mockContexto.Object, mapper);

            var libro = await manejador.Handle(libroUnico, new System.Threading.CancellationToken());

            Assert.NotNull(libro);
            Assert.True(libro.LibreriaMaterialId == Guid.Empty);
        }

        [Fact]
        public async void GuardarLibro()
        {
            System.Diagnostics.Debugger.Launch();

            var options = new DbContextOptionsBuilder<Contextolibreria>()
                .UseInMemoryDatabase(databaseName: "BaseDatosLibro")
                .Options;

            var contexto = new Contextolibreria(options);
            var ejecuta = new Nuevo.Ejecuta() 
            { 
                Titulo = "Libro de prueba",
                AutorLibro = Guid.Empty,
                FechaPublicacion = DateTime.Now
            };

            var manejador = new Nuevo.Manejador(contexto);
            var libro = await manejador.Handle(ejecuta, new System.Threading.CancellationToken());

            Assert.True(libro != null);
        }
    }
}

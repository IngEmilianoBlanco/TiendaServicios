namespace TiendaServicios.Api.Libro
{
    using AutoMapper;
    using FluentValidation.AspNetCore;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using TiendaServicios.Api.Libro.Aplicacion;
    using TiendaServicios.Api.Libro.Persistencia;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Nuevo.Ejecuta).Assembly);
            services.AddAutoMapper(typeof(Consulta.Manejador));
            services.AddDbContext<Contextolibreria>(optionsAction =>
            {
                optionsAction.UseSqlServer(Configuration.GetConnectionString("ConnectionString"));
            });
            services.AddControllers().AddFluentValidation(configurationExpression =>
            {
                configurationExpression.RegisterValidatorsFromAssemblyContaining<Nuevo>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TiendaServicios.Api.CarritoCompra.Aplicacion;
using TiendaServicios.Api.CarritoCompra.Persistencia;
using TiendaServicios.Api.CarritoCompra.RemoteInterfaces;
using TiendaServicios.Api.CarritoCompra.RemoteServices;

namespace TiendaServicios.Api.CarritoCompra
{
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
            services.AddDbContext<ContextoCarrito>(optionsAction => 
            {
                optionsAction.UseMySQL(Configuration.GetConnectionString("ConnectionString"));
            });
            services.AddMediatR(typeof(Nuevo.Manejador).Assembly);
            services.AddHttpClient("Libros", configureClient =>
            {
                configureClient.BaseAddress = new Uri(Configuration["Services:Libros"]);
            });
            services.AddHttpClient("Autor", configureClient => 
            {
                configureClient.BaseAddress = new Uri((Configuration["Services:Autores"]));
            });
            services.AddScoped<ILibrosServices, LibrosServices>();
            services.AddScoped<IAutorServices, AutorServices>();
            services.AddControllers();
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

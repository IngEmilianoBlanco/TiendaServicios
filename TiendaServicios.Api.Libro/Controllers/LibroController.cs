namespace TiendaServicios.Api.Libro.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TiendaServicios.Api.Libro.Aplicacion;
    using TiendaServicios.Api.Libro.Modelos;

    [ApiController]
    [Route("api/[controller]")]
    public class LibroController : ControllerBase
    {
        private readonly IMediator mediator;

        public LibroController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Post(Nuevo.Ejecuta data)
        {
            return await this.mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<LibreriaMaterialDtocs>>> Get()
        {
            return await this.mediator.Send(new Consulta.Ejecuta());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibreriaMaterialDtocs>> Get(string id)
        {
            return await this.mediator.Send(new ConsultaLibro.LibroUnico() { libroGUID = new Guid(id) });
        }
    }
}

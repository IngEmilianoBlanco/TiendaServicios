using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Aplicacion;
using TiendaServicios.Api.Autor.Modelos;

namespace TiendaServicios.Api.Autor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutorController: ControllerBase
    {
        private readonly IMediator _mediator;

        public AutorController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Post(Nuevo.Ejecuta ejecuta)
        {
            return await this._mediator.Send(ejecuta);
        }

        [HttpGet]
        public async Task<ActionResult<List<AutorLibroDto>>> Get()
        {
            return await this._mediator.Send(new Consulta.ListaAutor());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutorLibroDto>> Get(string id)
        {
            return await this._mediator.Send(new ConsultaFiltro.AutorUnico { AutorLibroGuid = id });
        }
    }
}

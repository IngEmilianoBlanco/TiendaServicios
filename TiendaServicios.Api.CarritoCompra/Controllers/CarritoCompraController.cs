using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Aplicacion;

namespace TiendaServicios.Api.CarritoCompra.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarritoCompraController : ControllerBase
    {
        private readonly IMediator mediator;

        public CarritoCompraController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Post(Nuevo.Ejecuta data)
        {
            return await this.mediator.Send(data);
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<CarritoDto>> GetCarrito(int id)
        {
            return await this.mediator.Send(new Consulta.Ejecuta { CarritoSesionId = id });
        }
    }
}

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    using System;
    using System.Collections.Generic;

    public class CarritoDto
    {
        public int CarritoId { get; set; }
        public DateTime? FechaCreacionSesion { get; set; }
        public List<CarritoDetalleDto> ListadoLibros { get; set; }
    }
}

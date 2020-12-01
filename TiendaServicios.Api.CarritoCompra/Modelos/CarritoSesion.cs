namespace TiendaServicios.Api.CarritoCompra.Modelos
{
    using System;
    using System.Collections.Generic;

    public class CarritoSesion
    {
        public int CarritoSesionId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<CarritoSesionDetalle> ListaDetalle { get; set; }
    }
}

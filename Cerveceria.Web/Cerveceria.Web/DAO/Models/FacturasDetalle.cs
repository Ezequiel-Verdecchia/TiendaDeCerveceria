using System;
using System.Collections.Generic;

#nullable disable

namespace Cerveceria.Web.DAO.Models
{
    public partial class FacturasDetalle
    {
        public int FacturaId { get; set; }
        public int DetalleId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}

using Cerveceria.Web.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cerveceria.Web.Models
{
    public class ProductosCarrito:Producto
    {
        public int? Cantidad { get; set; }
        public decimal? SubTotal { get; set; }
    }
    public class CarritoViewModel
    { 
        public List<ProductosCarrito> productosCarritos { get; set; }
    }
}

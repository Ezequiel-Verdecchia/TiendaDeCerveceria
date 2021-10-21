using Cerveceria.Web.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cerveceria.Web.Models
{
    public class ProductosViewModel
    {
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string rutaImagen { get; set; }
        public List<Producto> listadoProductos { get; set; }
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public string StockInformacion { get; set; }
        public string RutaImagen { get; set; }
        public string Medida { get; set; }
        public string Tipo { get; set; }
    }
}

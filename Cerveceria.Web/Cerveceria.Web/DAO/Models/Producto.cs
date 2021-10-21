using System;
using System.Collections.Generic;

#nullable disable

namespace Cerveceria.Web.DAO.Models
{
    public partial class Producto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public int ProveedorId { get; set; }
        public decimal Precio { get; set; }
        public decimal Costo { get; set; }
        public string Imagen { get; set; }
        public string Medida { get; set; }
        public string Tipo { get; set; }
        public DateTime Ifecha { get; set; }

        public virtual Proveedore Proveedor { get; set; }
    }
}

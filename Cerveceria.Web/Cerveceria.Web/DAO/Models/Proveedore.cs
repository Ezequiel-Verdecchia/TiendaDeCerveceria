using System;
using System.Collections.Generic;

#nullable disable

namespace Cerveceria.Web.DAO.Models
{
    public partial class Proveedore
    {
        public Proveedore()
        {
            Productos = new HashSet<Producto>();
        }

        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string Cuit { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}

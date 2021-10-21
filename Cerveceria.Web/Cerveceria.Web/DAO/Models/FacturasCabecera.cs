using System;
using System.Collections.Generic;

#nullable disable

namespace Cerveceria.Web.DAO.Models
{
    public partial class FacturasCabecera
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        public long Cae { get; set; }
        public DateTime FechaCae { get; set; }
        public long CodigoBarra { get; set; }
        public decimal Total { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Tipo { get; set; }
        public int? ComprobanteId { get; set; }
        public DateTime Ifecha { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}

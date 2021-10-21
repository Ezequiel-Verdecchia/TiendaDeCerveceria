using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cerveceria.Web.Models
{
    public class ConfirmacionCuentaViewModel
    {
        [Required (ErrorMessage="El campo 'Ingresar código' es requerido.")]
        public string TokenRegistro { get; set; }
    }
}

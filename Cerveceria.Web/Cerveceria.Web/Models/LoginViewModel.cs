using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cerveceria.Web.Models
{
    public class LoginViewModel
    {
        [Required (ErrorMessage ="El campo 'Email' es obligatorio.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "El campo 'Contraseña' es obligatorio.")]
        public string Password { get; set; }
    }
}

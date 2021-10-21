using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cerveceria.Web.Models
{
    public class RegistroViewModel
    {
        [Required (ErrorMessage ="El campo 'Email' es obligatorio.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo 'Contraseña' es obligatorio.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "El campo 'Confirmar contraseña' es obligatorio.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no son iguales.")]
        public string ConfirmarPassword { get; set; }
        [Required(ErrorMessage = "El campo 'Nombre' es obligatorio.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo 'Apellido' es obligatorio.")]
        public string Apellido { get; set; }
    }
}

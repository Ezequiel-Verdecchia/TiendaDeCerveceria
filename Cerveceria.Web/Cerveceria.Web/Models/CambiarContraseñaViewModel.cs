using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cerveceria.Web.Models
{
    public class CambiarContraseñaViewModel
    {
        [Required(ErrorMessage = "El campo 'Contraseña' es obligatorio.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "El campo 'Confirmar contraseña' es obligatorio.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no son iguales.")]
        public string ConfirmarPassword { get; set; }
    }
}

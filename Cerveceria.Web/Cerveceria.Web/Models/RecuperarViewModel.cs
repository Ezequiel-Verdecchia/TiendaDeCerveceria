using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cerveceria.Web.Models
{
    public class RecuperarViewModel
    {
        [Required(ErrorMessage = "El campo 'Email' es obligatorio.")]
        public string Email { get; set; }
    }
}

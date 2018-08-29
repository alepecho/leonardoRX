using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIMAMUS.UI.Models
{
    public class Usuario
    {
        [Key]
        private Int32 IdUsuario { get; set; }
        [Required(ErrorMessage ="Nombre de usuario requerido")]
        [StringLength(20, ErrorMessage = "Nombre de usuario tiene más de 20 caracteres")]
        private String NombreUsuario { get; set; }
        [Required(ErrorMessage = "Contraseña requerido")]
        [StringLength(20,ErrorMessage ="Constraseña tiene más de 20 caracteres")]
        private String Contrasenna { get; set; }
        private Int32 IdNivel { get; set; }
        private Int32 IdPersona { get; set; }
    }
}

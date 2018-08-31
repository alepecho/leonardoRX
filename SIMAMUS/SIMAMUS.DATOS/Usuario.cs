using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMAMUS.DATOS
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public String NombreUsuario { get; set; }
        public String Contrasenna { get; set; }
        public int IdNivel { get; set; }
        public int IdPersona { get; set; }
    }
}

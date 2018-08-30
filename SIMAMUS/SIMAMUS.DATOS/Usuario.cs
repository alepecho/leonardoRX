using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMAMUS.DATOS
{
    public class Usuario
    {
        public Int32 IdUsuario { get; set; }
        public String NombreUsuario { get; set; }
        public String Contrasenna { get; set; }
        public Int32 IdNivel { get; set; }
        public Int32 IdPersona { get; set; }
    }
}

using SIMAMUS.DAL.Interfaces;
using SIMAMUS.DATOS;
using SIMAMUS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMAMUS.DAL.Metodos
{
    public class MUsuario
    {
        SIMAMUSEntities contextoDB = new SIMAMUSEntities();

        public List<DATOS.Usuario> ListarUsuarios()
        {
            List<DATOS.Usuario> sUsuarios = new List<DATOS.Usuario>();
            DATOS.Usuario iUsuario = new DATOS.Usuario();


            var usuariosLista = contextoDB.Usuario.ToList();

            foreach (var _User in usuariosLista)
            {
                iUsuario.Contrasenna = _User.Contrasenna;
                iUsuario.IdNivel = _User.IdNivel;
                iUsuario.IdPersona = _User.IdPersona;
                iUsuario.IdUsuario = _User.IdUsuario;
                iUsuario.NombreUsuario = _User.NombreUsuario;

                sUsuarios.Add(iUsuario);
            }

            return sUsuarios;
        }
    }
}

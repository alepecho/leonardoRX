using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMAMUS.DAL.Interfaces
{
    public interface IUsuario
    {
        List<DATOS.Usuario> ListarUsuarios();

        DATOS.Usuario buscarUsuario(int idUsuario);

        void InsertarUsuario(DATOS.Usuario usuario);

        void ActualizarUsuario(DATOS.Usuario usuario);

        void EliminarUsuario(DATOS.Usuario usuario);
    }
}

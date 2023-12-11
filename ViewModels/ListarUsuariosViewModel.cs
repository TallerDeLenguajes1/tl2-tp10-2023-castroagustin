using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_castroagustin.ViewModels
{
    public class ListarUsuariosViewModel
    {
        public List<Usuario> Usuarios;
        public ListarUsuariosViewModel(List<Usuario> listaUsuarios)
        {
            Usuarios = listaUsuarios;
        }
    }
}
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_castroagustin.ViewModels
{
    public class ModificarUsuarioViewModel
    {
        public int Id { get; set; }
        public string NombreDeUsuario { get; set; }
        public string Contrasenia { get; set; }
        public Roles Rol { get; set; }

        public ModificarUsuarioViewModel() { }
        public ModificarUsuarioViewModel(Usuario usuario)
        {
            this.Id = usuario.Id;
            this.NombreDeUsuario = usuario.NombreDeUsuario;
            this.Contrasenia = usuario.Contrasenia;
            this.Rol = usuario.Rol;
        }
    }
}
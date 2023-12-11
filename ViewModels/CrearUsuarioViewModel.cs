using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_castroagustin.ViewModels
{
    public class CrearUsuarioViewModel
    {
        public string NombreDeUsuario { get; set; }
        public string Contrasenia { get; set; }
        public Roles Rol { get; set; }
        public CrearUsuarioViewModel() { }
        public CrearUsuarioViewModel(Usuario usuario)
        {
            this.NombreDeUsuario = usuario.NombreDeUsuario;
            this.Contrasenia = usuario.Contrasenia;
            this.Rol = usuario.Rol;
        }
    }
}
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_castroagustin.ViewModels
{
    public class CrearUsuarioViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(30)]
        [Display(Name = "Nombre de usuario")]
        public string NombreDeUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [PasswordPropertyText]
        [StringLength(15)]
        [Display(Name = "Contraseña")]
        public string Contrasenia { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Rol")]
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
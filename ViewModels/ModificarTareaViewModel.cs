using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_castroagustin.ViewModels
{
    public class ModificarTareaViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "ID Tablero")]
        public int IdTablero { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(30)]
        [Display(Name = "Nombre de la tarea")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(50)]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [StringLength(20)]
        [Display(Name = "Color")]
        public string? Color { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Estado")]
        public EstadoTarea Estado { get; set; }

        [Display(Name = "Usuario asignado")]
        public int? IdUsuarioAsignado { get; set; }
        public int IdUsuarioPropietario { get; set; }
        public List<Usuario> Usuarios { get; set; }

        public ModificarTareaViewModel()
        {
            Usuarios = new List<Usuario>();
        }
        public ModificarTareaViewModel(Tarea tarea, List<Usuario> usuarios, int idUsuarioPropietario)
        {
            this.Id = tarea.Id;
            this.IdTablero = tarea.IdTablero;
            this.Nombre = tarea.Nombre;
            this.Estado = tarea.Estado;
            this.Descripcion = tarea.Descripcion;
            this.Color = tarea.Color;
            this.IdUsuarioAsignado = tarea.IdUsuarioAsignado;
            this.IdUsuarioPropietario = idUsuarioPropietario;
            this.Usuarios = usuarios;
        }
    }
}
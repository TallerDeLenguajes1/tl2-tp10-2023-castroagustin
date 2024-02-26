using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace tl2_tp10_2023_castroagustin.ViewModels
{
    public class ModificarEstadoTareaViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "ID")]
        public int Id { get; set; }
        public int IdTablero { get; set; }
        public int IdUsuarioPropietario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Estado")]
        public EstadoTarea Estado { get; set; }

        public ModificarEstadoTareaViewModel(Tarea tarea)
        {
            this.Id = tarea.Id;
            this.IdTablero = tarea.IdTablero;
            this.IdUsuarioPropietario = tarea.IdUsuarioPropietario;
            this.Estado = tarea.Estado;
        }
        public ModificarEstadoTareaViewModel() { }
    }
}
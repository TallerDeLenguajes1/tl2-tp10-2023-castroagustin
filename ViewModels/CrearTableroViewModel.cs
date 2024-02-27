using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_castroagustin.ViewModels
{
    public class CrearTableroViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Id propietario")]
        public int IdUsuarioPropietario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(30)]
        [Display(Name = "Nombre del tablero")]
        public string Nombre { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Descripcion { get; set; }

        public CrearTableroViewModel() { }
        public CrearTableroViewModel(Tablero tablero)
        {
            this.IdUsuarioPropietario = tablero.IdUsuarioPropietario;
            this.Nombre = tablero.Nombre;
            this.Descripcion = tablero.Descripcion;
        }
    }
}
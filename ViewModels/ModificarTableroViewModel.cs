using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_castroagustin.ViewModels
{
    public class ModificarTableroViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Id propietario")]
        public int IdUsuarioPropietario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(30)]
        [Display(Name = "Nombre del tablero")]
        public string Nombre { get; set; }

        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        public ModificarTableroViewModel() { }
        public ModificarTableroViewModel(Tablero tablero)
        {
            this.Id = tablero.Id;
            this.IdUsuarioPropietario = tablero.IdUsuarioPropietario;
            this.Nombre = tablero.Nombre;
            this.Descripcion = tablero.Descripcion;
        }
    }
}
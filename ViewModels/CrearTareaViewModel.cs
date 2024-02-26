using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_castroagustin.ViewModels
{
    public class CrearTareaViewModel
    {
        public int IdTablero { get; set; }
        public string Nombre { get; set; }
        public EstadoTarea Estado { get; set; }
        public string Descripcion { get; set; }
        public string Color { get; set; }
        public int? IdUsuarioAsignado { get; set; }
        public int IdUsuarioPropietario { get; set; }

        public CrearTareaViewModel() { }
        public CrearTareaViewModel(Tarea tarea)
        {
            this.IdTablero = tarea.IdTablero;
            this.Nombre = tarea.Nombre;
            this.Estado = tarea.Estado;
            this.Descripcion = tarea.Descripcion;
            this.Color = tarea.Color;
            this.IdUsuarioAsignado = tarea.IdUsuarioAsignado;
            this.IdUsuarioPropietario = tarea.IdUsuarioPropietario;
        }
    }
}
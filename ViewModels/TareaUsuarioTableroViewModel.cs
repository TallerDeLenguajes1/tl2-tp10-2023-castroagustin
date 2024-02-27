using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_castroagustin.ViewModels
{
    public class TareaTableroUsuarioViewModel
    {
        public int Id { get; set; }
        public int IdTablero { get; set; }
        public string NombreTablero { get; set; }
        public string Nombre { get; set; }
        public EstadoTarea Estado { get; set; }
        public string Descripcion { get; set; }
        public string Color { get; set; }
        public int IdUsuarioPropietario { get; set; }
        public int? IdUsuarioAsignado { get; set; }
        public string? NombreUsuarioAsignado { get; set; }

        public TareaTableroUsuarioViewModel() { }
        public TareaTableroUsuarioViewModel(Tarea tarea, Tablero tablero, Usuario? usuario)
        {
            this.Id = tarea.Id;
            this.IdTablero = tarea.IdTablero;
            this.Nombre = tarea.Nombre;
            this.Estado = tarea.Estado;
            this.Descripcion = tarea.Descripcion;
            this.Color = tarea.Color;
            this.IdUsuarioAsignado = tarea.IdUsuarioAsignado;
            this.IdUsuarioPropietario = tarea.IdUsuarioPropietario;
            this.NombreTablero = tablero.Nombre;
            if (usuario != null) this.NombreUsuarioAsignado = usuario.NombreDeUsuario;
        }
    }
}
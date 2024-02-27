using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_castroagustin.ViewModels
{
    public class ListarTareasViewModel
    {
        public string NombreTablero { get; set; }
        public List<TareaTableroUsuarioViewModel> Tareas;
        public ListarTareasViewModel(List<Tarea> listaTareas, Tablero tablero, List<Usuario> usuarios)
        {
            Tareas = new List<TareaTableroUsuarioViewModel>();
            NombreTablero = tablero.Nombre;
            foreach (var tarea in listaTareas)
            {
                var usuario = usuarios.FirstOrDefault(u => u.Id == tarea.IdUsuarioAsignado);
                var tarTabUsuario = new TareaTableroUsuarioViewModel(tarea, tablero, usuario);
                Tareas.Add(tarTabUsuario);
            }
        }
    }
}
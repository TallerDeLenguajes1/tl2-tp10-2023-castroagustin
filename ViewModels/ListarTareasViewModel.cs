using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_castroagustin.ViewModels
{
    public class ListarTareasViewModel
    {
        public List<Tarea> Tareas;
        public ListarTareasViewModel(List<Tarea> listaTareas)
        {
            Tareas = listaTareas;
        }
    }
}
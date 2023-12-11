using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_castroagustin.ViewModels
{
    public class ListarTablerosViewModel
    {
        public List<Tablero> Tableros;
        public ListarTablerosViewModel(List<Tablero> listaTableros)
        {
            Tableros = listaTableros;
        }
    }
}
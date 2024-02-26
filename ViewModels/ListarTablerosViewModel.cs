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

        public ListarTablerosViewModel(List<Tablero> propietario, List<Tablero> asignados, List<Usuario> usuarios, int idUser)
        {
            Tableros = new List<Tablero>();
            var userLoguado = usuarios.FirstOrDefault(u => u.Id == idUser);
            foreach (var t in propietario)
            {
                Tableros.Add(t);
            }
            foreach (var t in asignados)
            {
                Tableros.Add(t);
            }
        }
    }
}
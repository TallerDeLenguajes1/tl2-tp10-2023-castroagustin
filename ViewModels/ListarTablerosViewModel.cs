using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_castroagustin.ViewModels
{
    public class ListarTablerosViewModel
    {
        public List<TableroUsuarioViewModel> Tableros;
        public ListarTablerosViewModel(List<Tablero> listaTableros, List<Usuario> usuarios, int idUser)
        {
            Tableros = new List<TableroUsuarioViewModel>();
            foreach (var t in listaTableros)
            {
                var usuario = usuarios.FirstOrDefault(u => u.Id == t.IdUsuarioPropietario)!;
                var tableroUsuario = new TableroUsuarioViewModel(t, usuario);
                Tableros.Add(tableroUsuario);
            }
        }

        public ListarTablerosViewModel(List<Tablero> propietario, List<Tablero> asignados, List<Usuario> usuarios, int idUser)
        {
            Tableros = new List<TableroUsuarioViewModel>();
            foreach (var t in propietario)
            {
                var usuario = usuarios.FirstOrDefault(u => u.Id == idUser)!;
                var tableroUsuario = new TableroUsuarioViewModel(t, usuario);

                if (Tableros.FirstOrDefault(tab => tab.Id == tableroUsuario.Id) == null)
                {
                    Tableros.Add(tableroUsuario);
                }
            }
            foreach (var t in asignados)
            {
                var usuario = usuarios.FirstOrDefault(u => u.Id == t.IdUsuarioPropietario)!;
                var tableroUsuario = new TableroUsuarioViewModel(t, usuario);

                if (Tableros.FirstOrDefault(tab => tab.Id == tableroUsuario.Id) == null)
                {
                    Tableros.Add(tableroUsuario);
                }
            }
        }
    }
}
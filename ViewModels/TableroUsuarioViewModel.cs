using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_castroagustin.ViewModels
{
    public class TableroUsuarioViewModel
    {
        public int Id { get; set; }
        public int IdUsuarioPropietario { get; set; }
        public string NombreUsuarioPropietario { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public TableroUsuarioViewModel() { }
        public TableroUsuarioViewModel(Tablero tablero, Usuario usuario)
        {
            this.Id = tablero.Id;
            this.IdUsuarioPropietario = usuario.Id;
            this.NombreUsuarioPropietario = usuario.NombreDeUsuario;
            this.Nombre = tablero.Nombre;
            this.Descripcion = tablero.Descripcion;
        }
    }
}
namespace tl2_tp10_2023_castroagustin.Repositorios
{
    public interface ITareaRepository
    {
        public void Create(int idTablero, Tarea tarea);
        public void Update(int id, Tarea tarea);
        public Tarea Get(int id);
        public List<Tarea> GetAllByUser(int idUsuario);
        public List<Tarea> GetAllByTablero(int idTablero);
        public List<Tarea> GetAllByEstado(int idTablero);
        public List<Tarea> GetAll();
        public void Remove(int id);
        public void AsignarUsuario(int idUsuario, int idTarea);
    }
}
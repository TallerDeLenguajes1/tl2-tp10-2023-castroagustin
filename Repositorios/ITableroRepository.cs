namespace tl2_tp10_2023_castroagustin.Repositorios
{
    public interface ITableroRepository
    {
        public void Create(Tablero tablero);
        public void Update(int id, Tablero tablero);
        public Tablero Get(int id);
        public List<Tablero> GetAll();
        public void Remove(int id);
        public List<Tablero> GetAllByUser(int idUsuario);
        public List<Tablero> GetAllByAssigned(int idUsuario);
    }
}
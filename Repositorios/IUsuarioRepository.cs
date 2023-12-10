namespace tl2_tp10_2023_castroagustin.Repositorios
{
    public interface IUsuarioRepository
    {
        public void Create(Usuario usuario);
        public void Update(int id, Usuario usuario);
        public List<Usuario> GetAll();
        public Usuario Get(int id);
        public void Remove(int id);
    }
}
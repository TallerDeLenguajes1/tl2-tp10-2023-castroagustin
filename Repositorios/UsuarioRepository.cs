using System.Data.SQLite;
using System.Security.Cryptography;

namespace tl2_tp10_2023_castroagustin.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string cadenaConexion;

        public UsuarioRepository(string CadenaConexion)
        {
            this.cadenaConexion = CadenaConexion;
        }
        public void Create(Usuario usuario)
        {
            var query = @"INSERT INTO usuario (nombre_de_usuario, contrasenia, rol) VALUES (@nombre, @contrasenia, @rol)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);
                connection.Open();

                command.Parameters.Add(new SQLiteParameter("@nombre", usuario.NombreDeUsuario));
                command.Parameters.Add(new SQLiteParameter("@contrasenia", usuario.Contrasenia));
                command.Parameters.Add(new SQLiteParameter("@rol", usuario.Rol));
                var filas = command.ExecuteNonQuery();
                connection.Close();

                if (filas == 0) throw new Exception("Hubo un problema al crear el usuario");

                connection.Close();
            }
        }

        public void Update(int id, Usuario usuario)
        {
            var query = @"UPDATE usuario SET nombre_de_usuario = @nombre, contrasenia = @contrasenia, rol = @rol WHERE id = @id";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);
                connection.Open();

                command.Parameters.Add(new SQLiteParameter("@nombre", usuario.NombreDeUsuario));
                command.Parameters.Add(new SQLiteParameter("@contrasenia", usuario.Contrasenia));
                command.Parameters.Add(new SQLiteParameter("@rol", usuario.Rol));
                command.Parameters.Add(new SQLiteParameter("@id", id));
                var filas = command.ExecuteNonQuery();
                connection.Close();

                if (filas == 0) throw new Exception("Hubo un problema al modificar el usuario");

                connection.Close();
            }
        }

        public List<Usuario> GetAll()
        {
            var query = @"SELECT * FROM usuario";
            List<Usuario> usuarios = null;

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    usuarios = new List<Usuario>();
                    while (reader.Read())
                    {
                        var usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        usuario.Contrasenia = reader["contrasenia"].ToString();
                        usuario.Rol = (Roles)Convert.ToInt32(reader["rol"]);
                        usuarios.Add(usuario);
                    }
                }
                connection.Close();
            }
            if (usuarios == null) throw new Exception("Usuario no encontrado");
            return usuarios;
        }

        public Usuario Get(int id)
        {
            var query = @"SELECT * FROM usuario WHERE id = @id";
            Usuario usuario = null;
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@id", id));

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        usuario.Contrasenia = reader["contrasenia"].ToString();
                        usuario.Rol = (Roles)Convert.ToInt32(reader["rol"]);
                    }
                }
                connection.Close();
            }
            if (usuario == null) throw new Exception("Usuario no encontrado");
            return usuario;
        }

        public void Remove(int id)
        {
            var query = @"DELETE FROM usuario WHERE id = @id";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@id", id));

                var filas = command.ExecuteNonQuery();
                connection.Close();

                if (filas == 0) throw new Exception("Hubo un problema al eliminar el usuario");

                connection.Close();
            }
        }
    }
}
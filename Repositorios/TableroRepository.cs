using System.Data.SQLite;
using System.Security.Cryptography;

namespace tl2_tp10_2023_castroagustin.Repositorios
{
    public class TableroRepository : ITableroRepository
    {
        private string cadenaConexion = "Data Source=db/kanban.db;Cache=Shared";
        public void Create(Tablero tablero)
        {
            var query = @"INSERT INTO tablero (nombre, descripcion, id_usuario_propietario) VALUES (@nombre, @desc, @idUsuario)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);
                connection.Open();

                command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@desc", tablero.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@idUsuario", tablero.IdUsuarioPropietario));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Update(int id, Tablero tablero)
        {
            var query = @"UPDATE tablero SET nombre = @nombre, descripcion = @desc, id_usuario_propietario = @idUsuario WHERE id = @id";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);
                connection.Open();

                command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@desc", tablero.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@idUsuario", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@id", id));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public List<Tablero> GetAll()
        {
            var query = @"SELECT * FROM tablero";
            List<Tablero> tableros = new List<Tablero>();

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tableros.Add(tablero);
                    }
                }

                connection.Close();
            }
            return tableros;
        }

        public Tablero Get(int id)
        {
            var query = @"SELECT * FROM tablero WHERE id = @id";
            var tablero = new Tablero();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@id", id));

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    }
                }
                connection.Close();
            }
            return tablero;
        }

        public List<Tablero> GetAllByUser(int idUsuario)
        {
            var query = @"SELECT * FROM tablero WHERE id_usuario_propietario = @idUsuario";
            List<Tablero> tableros = new List<Tablero>();

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tableros.Add(tablero);
                    }
                }

                connection.Close();
            }
            return tableros;
        }

        public void Remove(int id)
        {
            var query = @"DELETE FROM tablero WHERE id = @id";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@id", id));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
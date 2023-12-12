using System.Data.SQLite;
using System.Security.Cryptography;

namespace tl2_tp10_2023_castroagustin.Repositorios
{
    public class TareaRepository : ITareaRepository
    {
        private readonly string cadenaConexion;
        public TareaRepository(string CadenaConexion)
        {
            this.cadenaConexion = CadenaConexion;
        }
        public void Create(int idTablero, Tarea tarea)
        {
            var query = @"INSERT INTO tarea (id_tablero, nombre, estado, descripcion, color, id_usuario_asignado) VALUES (@idTablero, @nombre, @estado, @desc, @color, @idUsuario)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);
                connection.Open();

                command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));
                command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@desc", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@idUsuario", tarea.IdUsuarioAsignado));
                var filas = command.ExecuteNonQuery();
                connection.Close();

                if (filas == 0) throw new Exception("Hubo un problema al crear la tarea");

                connection.Close();
            }
        }

        public void Update(int id, Tarea tarea)
        {
            var query = $"UPDATE Tarea SET id_tablero = @idTablero, nombre = @nombre, estado = @estado, descripcion = @desc, color = @color, id_usuario_asignado = @idUsuario WHERE id = @id";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);
                connection.Open();

                command.Parameters.Add(new SQLiteParameter("@idTablero", tarea.IdTablero));
                command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@desc", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@idUsuario", tarea.IdUsuarioAsignado));
                command.Parameters.Add(new SQLiteParameter("@id", id));
                var filas = command.ExecuteNonQuery();
                connection.Close();

                if (filas == 0) throw new Exception("Hubo un problema al modificar la tarea");

                connection.Close();
            }
        }

        public Tarea Get(int id)
        {
            var query = @"SELECT * FROM tarea WHERE id = @id";
            Tarea tarea = null;
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@id", id));

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = reader["id_usuario_asignado"] != DBNull.Value ? Convert.ToInt32(reader["id_usuario_asignado"]) : (int?)null;
                    }
                }
                connection.Close();
            }
            if (tarea == null) throw new Exception("No se encontro la tarea");
            return tarea;
        }

        public List<Tarea> GetAllByUser(int idUsuario)
        {
            var query = @"SELECT * FROM tarea WHERE id_usuario_asignado = @idUsuario";
            List<Tarea> tareas = null;

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    tareas = new List<Tarea>();
                    while (reader.Read())
                    {
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = reader["id_usuario_asignado"] != DBNull.Value ? Convert.ToInt32(reader["id_usuario_asignado"]) : (int?)null;
                        tareas.Add(tarea);
                    }
                }

                connection.Close();
            }
            if (tareas == null) throw new Exception("No se encontro ninguna tarea");
            return tareas;
        }

        public List<Tarea> GetAllByTablero(int idTablero)
        {
            var query = @"SELECT * FROM tarea WHERE id_tablero = @idTablero";
            List<Tarea> tareas = null;

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    tareas = new List<Tarea>();
                    while (reader.Read())
                    {
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = reader["id_usuario_asignado"] != DBNull.Value ? Convert.ToInt32(reader["id_usuario_asignado"]) : (int?)null;
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            if (tareas == null) throw new Exception("No se encontro ninguna tarea");
            return tareas;
        }

        public List<Tarea> GetAllByEstado(int estado)
        {
            var query = @"SELECT * FROM tarea WHERE estado = @estado";
            List<Tarea> tareas = null;

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@estado", estado));

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    tareas = new List<Tarea>();
                    while (reader.Read())
                    {
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = reader["id_usuario_asignado"] != DBNull.Value ? Convert.ToInt32(reader["id_usuario_asignado"]) : (int?)null;
                        tareas.Add(tarea);
                    }
                }

                connection.Close();
            }
            if (tareas == null) throw new Exception("No se encontro ninguna tarea");
            return tareas;
        }

        public List<Tarea> GetAll()
        {
            var query = @"SELECT * FROM tarea";
            List<Tarea> tareas = null;

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    tareas = new List<Tarea>();
                    while (reader.Read())
                    {
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = reader["id_usuario_asignado"] != DBNull.Value ? Convert.ToInt32(reader["id_usuario_asignado"]) : (int?)null;
                        tareas.Add(tarea);
                    }
                }

                connection.Close();
            }
            if (tareas == null) throw new Exception("No se encontro ninguna tarea");
            return tareas;
        }

        public void Remove(int id)
        {
            var query = @"DELETE FROM tarea WHERE id = @id";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@id", id));

                var filas = command.ExecuteNonQuery();
                connection.Close();

                if (filas == 0) throw new Exception("Hubo un problema al eliminar la tarea");

                connection.Close();
            }
        }

        public void AsignarUsuario(int idUsuario, int idTarea)
        {
            var query = $"UPDATE Tarea SET id_usuario_asignado = @idUsuario WHERE id = @id";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);
                connection.Open();

                command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
                command.Parameters.Add(new SQLiteParameter("@id", idTarea));
                var filas = command.ExecuteNonQuery();
                connection.Close();

                if (filas == 0) throw new Exception("Hubo un problema al asignar la tarea al usuario");

                connection.Close();
            }
        }
    }
}
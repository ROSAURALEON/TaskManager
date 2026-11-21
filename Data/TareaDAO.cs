using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using TaskManager.Models;

namespace TaskManager.Data
{
    public class TareaDAO
    {
        private readonly ConexionBD _conexionBD;

        public TareaDAO(ConexionBD conexionBD)
        {
            _conexionBD = conexionBD;
        }

        // Método para agregar una tarea
        public void Agregar(Tarea tarea)
        {
            using var conexion = _conexionBD.ObtenerConexion();
            conexion.Open();

            string sql = @"
                INSERT INTO Tareas
                (Titulo, Descripcion, FechaVencimiento, Prioridad, Estado)
                VALUES
                (@titulo, @descripcion, @fecha, @prioridad, @estado);
            ";

            using var comando = new SqliteCommand(sql, conexion);

            comando.Parameters.AddWithValue("@titulo", tarea.Titulo);
            comando.Parameters.AddWithValue("@descripcion", tarea.Descripcion);
            comando.Parameters.AddWithValue("@fecha", tarea.FechaVencimiento.ToString("yyyy-MM-dd"));
            comando.Parameters.AddWithValue("@prioridad", tarea.Prioridad);
            comando.Parameters.AddWithValue("@estado", tarea.Estado);

            comando.ExecuteNonQuery();
        }

        // Método para consultar todas las tareas
        public List<Tarea> ObtenerTodas()
        {
            List<Tarea> tareas = new List<Tarea>();

            using var conexion = _conexionBD.ObtenerConexion();
            conexion.Open();

            string sql = "SELECT * FROM Tareas ORDER BY Id DESC;";

            using var comando = new SqliteCommand(sql, conexion);
            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                Tarea tarea = new Tarea(
                    lector.GetInt32(0),
                    lector.GetString(1),
                    lector.GetString(2),
                    DateTime.Parse(lector.GetString(3)),
                    lector.GetString(4),
                    lector.GetString(5)
                );

                tareas.Add(tarea);
            }

            return tareas;
        }

        // Método para actualizar una tarea
        public void Actualizar(Tarea tarea)
        {
            using var conexion = _conexionBD.ObtenerConexion();
            conexion.Open();

            string sql = @"
                UPDATE Tareas
                SET Titulo = @titulo,
                    Descripcion = @descripcion,
                    FechaVencimiento = @fecha,
                    Prioridad = @prioridad,
                    Estado = @estado
                WHERE Id = @id;
            ";

            using var comando = new SqliteCommand(sql, conexion);

            comando.Parameters.AddWithValue("@id", tarea.Id);
            comando.Parameters.AddWithValue("@titulo", tarea.Titulo);
            comando.Parameters.AddWithValue("@descripcion", tarea.Descripcion);
            comando.Parameters.AddWithValue("@fecha", tarea.FechaVencimiento.ToString("yyyy-MM-dd"));
            comando.Parameters.AddWithValue("@prioridad", tarea.Prioridad);
            comando.Parameters.AddWithValue("@estado", tarea.Estado);

            comando.ExecuteNonQuery();
        }

        // Método para eliminar una tarea
        public void Eliminar(int id)
        {
            using var conexion = _conexionBD.ObtenerConexion();
            conexion.Open();

            string sql = "DELETE FROM Tareas WHERE Id = @id;";

            using var comando = new SqliteCommand(sql, conexion);
            comando.Parameters.AddWithValue("@id", id);

            comando.ExecuteNonQuery();
        }
    }
}

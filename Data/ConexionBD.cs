
using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace TaskManager.Data
{
    public class ConexionBD
    {
        // Ruta de almacenamiento de la base de datos
        private readonly string _cadenaConexion;

        // Constructor
        public ConexionBD()
        {
            string rutaBD = Path.Combine(
                AppContext.BaseDirectory,
                "gestor_tareas.db"
            );

            _cadenaConexion = $"Data Source={rutaBD}";
        }

        // Método para establecer la conexión
        public SqliteConnection ObtenerConexion()
        {
            return new SqliteConnection(_cadenaConexion);
        }

        // Método para crear la base de datos y su tabla
        public void InicializarBD()
        {
            using var conexion = ObtenerConexion();

            conexion.Open();

            string sql = @"
                CREATE TABLE IF NOT EXISTS Tareas (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Titulo TEXT NOT NULL,
                    Descripcion TEXT NOT NULL,
                    FechaVencimiento TEXT NOT NULL,
                    Prioridad TEXT NOT NULL,
                    Estado TEXT NOT NULL
                );
            ";

            using var comando = new SqliteCommand(sql, conexion);

            comando.ExecuteNonQuery();
        }
    }
}


using System;

namespace TaskManager.Models
{
    public class Tarea
    {
        // Atributos privados de la clase
        private int _id;
        private string _titulo;
        private string _descripcion;
        private DateTime _fechaVencimiento;
        private string _prioridad;
        private string _estado;

        // Constructor
        public Tarea(int id, string titulo, string descripcion,
                     DateTime fechaVencimiento, string prioridad,
                     string estado)
        {
            _id = id;
            _titulo = titulo;
            _descripcion = descripcion;
            _fechaVencimiento = fechaVencimiento;
            _prioridad = prioridad;
            _estado = estado;
        }

        // Propiedades
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public DateTime FechaVencimiento
        {
            get { return _fechaVencimiento; }
            set { _fechaVencimiento = value; }
        }

        public string Prioridad
        {
            get { return _prioridad; }
            set { _prioridad = value; }
        }

        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
    }
}

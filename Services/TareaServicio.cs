
using System;
using System.Collections.Generic;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class TareaServicio
    {
        private readonly TareaDAO _tareaDAO;

        public TareaServicio(TareaDAO tareaDAO)
        {
            _tareaDAO = tareaDAO;
        }

        public void AgregarTarea(Tarea tarea)
        {
            ValidarTarea(tarea);
            _tareaDAO.Agregar(tarea);
        }

        public List<Tarea> ObtenerTareas()
        {
            return _tareaDAO.ObtenerTodas();
        }

        public void ActualizarTarea(Tarea tarea)
        {
            if (tarea.Id <= 0)
            {
                throw new Exception("Selecciona una tarea válida.");
            }

            ValidarTarea(tarea);
            _tareaDAO.Actualizar(tarea);
        }

        public void EliminarTarea(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Selecciona una tarea válida.");
            }

            _tareaDAO.Eliminar(id);
        }

        private void ValidarTarea(Tarea tarea)
        {
            if (string.IsNullOrWhiteSpace(tarea.Titulo))
            {
                throw new Exception("El título de la tarea es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(tarea.Descripcion))
            {
                throw new Exception("La descripción de la tarea es obligatoria.");
            }

            if (tarea.FechaVencimiento == default)
            {
                throw new Exception("Debes seleccionar una fecha de vencimiento.");
            }

            if (tarea.Prioridad != "Baja" &&
                tarea.Prioridad != "Media" &&
                tarea.Prioridad != "Alta")
            {
                throw new Exception("Selecciona una prioridad válida.");
            }

            if (tarea.Estado != "Pendiente" &&
                tarea.Estado != "Completada")
            {
                throw new Exception("Selecciona un estado válido.");
            }
        }
    }
}

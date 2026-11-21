
using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using TaskManager.Data;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager
{
    public partial class MainWindow : Window
    {
        private readonly TareaServicio _servicio;
        private readonly ObservableCollection<Tarea> _tareas;

        public MainWindow()
        {
            InitializeComponent();

            // Inicializar la conexión y la base de datos
            ConexionBD conexionBD = new ConexionBD();
            conexionBD.InicializarBD();

            // Crear el acceso a los datos y el servicio
            TareaDAO tareaDAO = new TareaDAO(conexionBD);
            _servicio = new TareaServicio(tareaDAO);

            // Preparar la lista que mostrará las tareas
            _tareas = new ObservableCollection<Tarea>();
            ListaTareas.ItemsSource = _tareas;

            CargarTareas();
        }

        // Consultar y mostrar las tareas guardadas
        private void CargarTareas()
        {
            _tareas.Clear();

            foreach (Tarea tarea in _servicio.ObtenerTareas())
            {
                _tareas.Add(tarea);
            }
        }

        // Obtener el texto seleccionado en un ComboBox
        private string ObtenerSeleccion(ComboBox combo)
        {
            if (combo.SelectedItem is ComboBoxItem elemento)
            {
                return elemento.Content?.ToString() ?? "";
            }

            return "";
        }

        // Agregar una nueva tarea
        private void Agregar_Click(object? sender, RoutedEventArgs e)
        {
            try
            {
                if (!DateTime.TryParse(TxtFecha.Text, out DateTime fecha))
                {
                    throw new Exception("Escribe una fecha válida. Ejemplo: 2026-12-15.");
                }

                Tarea tarea = new Tarea(
                    0,
                    TxtTitulo.Text ?? "",
                    TxtDescripcion.Text ?? "",
                    fecha,
                    ObtenerSeleccion(CmbPrioridad),
                    ObtenerSeleccion(CmbEstado)
                );

                _servicio.AgregarTarea(tarea);

                CargarTareas();
                LimpiarFormulario();

                TxtMensaje.Text = "La tarea se agregó correctamente.";
            }
            catch (Exception ex)
            {
                TxtMensaje.Text = ex.Message;
            }
        }

        // Actualizar la tarea seleccionada
        private void Actualizar_Click(object? sender, RoutedEventArgs e)
        {
            try
            {
                if (ListaTareas.SelectedItem is not Tarea seleccionada)
                {
                    throw new Exception("Primero selecciona una tarea de la lista.");
                }

                if (!DateTime.TryParse(TxtFecha.Text, out DateTime fecha))
                {
                    throw new Exception("Escribe una fecha válida. Ejemplo: 2026-12-15.");
                }

                Tarea tarea = new Tarea(
                    seleccionada.Id,
                    TxtTitulo.Text ?? "",
                    TxtDescripcion.Text ?? "",
                    fecha,
                    ObtenerSeleccion(CmbPrioridad),
                    ObtenerSeleccion(CmbEstado)
                );

                _servicio.ActualizarTarea(tarea);

                CargarTareas();
                LimpiarFormulario();

                TxtMensaje.Text = "La tarea se actualizó correctamente.";
            }
            catch (Exception ex)
            {
                TxtMensaje.Text = ex.Message;
            }
        }

        // Eliminar la tarea seleccionada
        private void Eliminar_Click(object? sender, RoutedEventArgs e)
        {
            try
            {
                if (ListaTareas.SelectedItem is not Tarea seleccionada)
                {
                    throw new Exception("Primero selecciona una tarea de la lista.");
                }

                _servicio.EliminarTarea(seleccionada.Id);

                CargarTareas();
                LimpiarFormulario();

                TxtMensaje.Text = "La tarea se eliminó correctamente.";
            }
            catch (Exception ex)
            {
                TxtMensaje.Text = ex.Message;
            }
        }

        // Cargar los datos de la tarea seleccionada en el formulario
        private void ListaTareas_SelectionChanged(
            object? sender,
            SelectionChangedEventArgs e)
        {
            if (ListaTareas.SelectedItem is Tarea tarea)
            {
                TxtTitulo.Text = tarea.Titulo;
                TxtDescripcion.Text = tarea.Descripcion;
                TxtFecha.Text = tarea.FechaVencimiento.ToString("yyyy-MM-dd");

                CmbPrioridad.SelectedIndex = tarea.Prioridad switch
                {
                    "Baja" => 0,
                    "Media" => 1,
                    "Alta" => 2,
                    _ => 1
                };

                CmbEstado.SelectedIndex = tarea.Estado == "Completada" ? 1 : 0;
            }
        }

        // Limpiar los campos del formulario
        private void Limpiar_Click(object? sender, RoutedEventArgs e)
        {
            LimpiarFormulario();
            TxtMensaje.Text = "";
        }

        private void LimpiarFormulario()
        {
            TxtTitulo.Text = "";
            TxtDescripcion.Text = "";
            TxtFecha.Text = "";

            CmbPrioridad.SelectedIndex = 1;
            CmbEstado.SelectedIndex = 0;

            ListaTareas.SelectedItem = null;
        }
    }
}

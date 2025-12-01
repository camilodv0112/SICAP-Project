using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace Presentacion.Views
{
    public partial class EventosUserControlView : UserControl
    {
        public EventosUserControlView()
        {
            InitializeComponent();
        }

        private void ExportarEventos(object sender, RoutedEventArgs e)
        {
            try
            {
                var eventos = (DataContext as ViewModels.EventosViewModel)?.Eventos;
                
                if (eventos == null || eventos.Count == 0)
                {
                    MessageBox.Show("No hay datos para exportar.", "Exportar", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV file (*.csv)|*.csv",
                    FileName = $"Eventos_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    var csv = new StringBuilder();
                    
                    // Encabezados
                    csv.AppendLine("Nombre,Fecha,Hora Inicio,Hora Fin,Sede,Salón,Participantes,Responsables");
                    
                    // Datos
                    foreach (var ev in eventos)
                    {
                        csv.AppendLine($"\"{ev.Nombre}\",\"{ev.Fecha:dd/MM/yyyy}\",\"{ev.HoraInicio:hh\\:mm}\",\"{ev.HoraFinal:hh\\:mm}\",\"{ev.SedeNombre}\",\"{ev.SalonNombre}\",{ev.CantidadParticipantes},{ev.CantidadResponsables}");
                    }

                    File.WriteAllText(saveFileDialog.FileName, csv.ToString(), Encoding.UTF8);
                    
                    MessageBox.Show($"Datos exportados exitosamente a:\n{saveFileDialog.FileName}", "Exportación Exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

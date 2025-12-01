using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace Presentacion.Views
{
    public partial class EstudiantesUserControlView : UserControl
    {
        public EstudiantesUserControlView()
        {
            InitializeComponent();
        }

        private void ExportarEstudiantes(object sender, RoutedEventArgs e)
        {
            try
            {
                var estudiantes = (DataContext as ViewModels.EstudiantesViewModel)?.Estudiantes;
                
                if (estudiantes == null || estudiantes.Count == 0)
                {
                    MessageBox.Show("No hay datos para exportar.", "Exportar", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV file (*.csv)|*.csv",
                    FileName = $"Estudiantes_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    var csv = new StringBuilder();
                    
                    // Encabezados
                    csv.AppendLine("Carnet,Nombre Completo,Carrera,Año,Grupo,Disciplina,Correo");
                    
                    // Datos
                    foreach (var est in estudiantes)
                    {
                        csv.AppendLine($"\"{est.Carnet}\",\"{est.NombreCompleto}\",\"{est.CarreraNombre}\",{est.Año},\"{est.Grupo}\",\"{est.DisciplinaNombre}\",\"{est.CorreoElectronico}\"");
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

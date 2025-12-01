using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace Presentacion.Views
{
    public partial class PersonalUserControlView : UserControl
    {
        public PersonalUserControlView()
        {
            InitializeComponent();
        }

        private void ExportarPersonal(object sender, RoutedEventArgs e)
        {
            try
            {
                var personal = (DataContext as ViewModels.PersonalViewModel)?.Personal;
                
                if (personal == null || personal.Count == 0)
                {
                    MessageBox.Show("No hay datos para exportar.", "Exportar", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV file (*.csv)|*.csv",
                    FileName = $"Personal_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    var csv = new StringBuilder();
                    
                    // Encabezados
                    csv.AppendLine("No. Empleado,Nombre Completo,Cédula,Cargo,Disciplina,Celular,Correo");
                    
                    // Datos
                    foreach (var p in personal)
                    {
                        csv.AppendLine($"\"{p.NumeroEmpleado}\",\"{p.NombreCompleto}\",\"{p.Cedula}\",\"{p.CargoNombre}\",\"{p.DisciplinaNombre}\",\"{p.Celular}\",\"{p.CorreoElectronico}\"");
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

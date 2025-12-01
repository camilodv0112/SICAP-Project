using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Modelos.ModelsDTO.Estudiante;

namespace Presentacion.Views
{
    public partial class CartasUserControlView : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private EstudianteCartaInfo _estudianteSeleccionado;
        public EstudianteCartaInfo EstudianteSeleccionado
        {
            get => _estudianteSeleccionado;
            set
            {
                _estudianteSeleccionado = value;
                OnPropertyChanged();
            }
        }

        private string _notasAdicionales = "";
        public string NotasAdicionales
        {
            get => _notasAdicionales;
            set
            {
                _notasAdicionales = value;
                OnPropertyChanged();
            }
        }

        private string _tituloDocumento = "";
        public string TituloDocumento
        {
            get => _tituloDocumento;
            set
            {
                _tituloDocumento = value;
                OnPropertyChanged();
            }
        }

        // Propiedades para Solicitud de Transporte
        private string _nombreSolicitante = "";
        public string NombreSolicitante
        {
            get => _nombreSolicitante;
            set { _nombreSolicitante = value; OnPropertyChanged(); }
        }

        private string _dependenciaSolicitante = "";
        public string DependenciaSolicitante
        {
            get => _dependenciaSolicitante;
            set { _dependenciaSolicitante = value; OnPropertyChanged(); }
        }

        private string _responsableActividad = "";
        public string ResponsableActividad
        {
            get => _responsableActividad;
            set { _responsableActividad = value; OnPropertyChanged(); }
        }

        private string _numeroPasajeros = "";
        public string NumeroPasajeros
        {
            get => _numeroPasajeros;
            set { _numeroPasajeros = value; OnPropertyChanged(); }
        }

        private string _descripcionActividad = "";
        public string DescripcionActividad
        {
            get => _descripcionActividad;
            set { _descripcionActividad = value; OnPropertyChanged(); }
        }

        private string _destino = "";
        public string Destino
        {
            get => _destino;
            set { _destino = value; OnPropertyChanged(); }
        }

        private string _departamento = "";
        public string Departamento
        {
            get => _departamento;
            set { _departamento = value; OnPropertyChanged(); }
        }

        private string _municipio = "";
        public string Municipio
        {
            get => _municipio;
            set { _municipio = value; OnPropertyChanged(); }
        }

        private string _kmAproximados = "";
        public string KmAproximados
        {
            get => _kmAproximados;
            set { _kmAproximados = value; OnPropertyChanged(); }
        }

        private string _fechaSalida = "";
        public string FechaSalida
        {
            get => _fechaSalida;
            set { _fechaSalida = value; OnPropertyChanged(); }
        }

        private string _horaSalida = "";
        public string HoraSalida
        {
            get => _horaSalida;
            set { _horaSalida = value; OnPropertyChanged(); }
        }

        private string _fechaRegreso = "";
        public string FechaRegreso
        {
            get => _fechaRegreso;
            set { _fechaRegreso = value; OnPropertyChanged(); }
        }

        private string _horaRegreso = "";
        public string HoraRegreso
        {
            get => _horaRegreso;
            set { _horaRegreso = value; OnPropertyChanged(); }
        }

        private string _tipoVehiculo = "";
        public string TipoVehiculo
        {
            get => _tipoVehiculo;
            set { _tipoVehiculo = value; OnPropertyChanged(); }
        }

        private string _numeroPlaca = "";
        public string NumeroPlaca
        {
            get => _numeroPlaca;
            set { _numeroPlaca = value; OnPropertyChanged(); }
        }

        private string _colorVehiculo = "";
        public string ColorVehiculo
        {
            get => _colorVehiculo;
            set { _colorVehiculo = value; OnPropertyChanged(); }
        }

        // Propiedades para Justificación de Estudiante
        private string _destinatarioNombre = "";
        public string DestinatarioNombre
        {
            get => _destinatarioNombre;
            set { _destinatarioNombre = value; OnPropertyChanged(); }
        }

        private string _destinatarioCargo = "";
        public string DestinatarioCargo
        {
            get => _destinatarioCargo;
            set { _destinatarioCargo = value; OnPropertyChanged(); }
        }

        private string _destinatarioInstitucion = "";
        public string DestinatarioInstitucion
        {
            get => _destinatarioInstitucion;
            set { _destinatarioInstitucion = value; OnPropertyChanged(); }
        }

        private string _fechaEvento = "";
        public string FechaEvento
        {
            get => _fechaEvento;
            set { _fechaEvento = value; OnPropertyChanged(); }
        }

        private string _horaInicio = "";
        public string HoraInicio
        {
            get => _horaInicio;
            set { _horaInicio = value; OnPropertyChanged(); }
        }

        private string _horaFin = "";
        public string HoraFin
        {
            get => _horaFin;
            set { _horaFin = value; OnPropertyChanged(); }
        }

        private string _nombreEvento = "";
        public string NombreEvento
        {
            get => _nombreEvento;
            set { _nombreEvento = value; OnPropertyChanged(); }
        }

        private string _descripcionEvento = "";
        public string DescripcionEvento
        {
            get => _descripcionEvento;
            set { _descripcionEvento = value; OnPropertyChanged(); }
        }

        private string _disciplinaEvento = "";
        public string DisciplinaEvento
        {
            get => _disciplinaEvento;
            set { _disciplinaEvento = value; OnPropertyChanged(); }
        }

        private string _asignaturaEstudiante = "";
        public string AsignaturaEstudiante
        {
            get => _asignaturaEstudiante;
            set { _asignaturaEstudiante = value; OnPropertyChanged(); }
        }

        private string _fechaDocumento = DateTime.Now.ToString("d 'de' MMMM 'de' yyyy", new System.Globalization.CultureInfo("es-ES"));
        public string FechaDocumento
        {
            get => _fechaDocumento;
            set { _fechaDocumento = value; OnPropertyChanged(); }
        }

        private string _firmaNombre = "Nabucodonosor Ganimedes Morales";
        public string FirmaNombre
        {
            get => _firmaNombre;
            set { _firmaNombre = value; OnPropertyChanged(); }
        }

        private string _firmaCargo = "Director del grupo de Teatro UNITE";
        public string FirmaCargo
        {
            get => _firmaCargo;
            set { _firmaCargo = value; OnPropertyChanged(); }
        }

        private string _firmaDepartamento = "Departamento de Cultura UNI";
        public string FirmaDepartamento
        {
            get => _firmaDepartamento;
            set { _firmaDepartamento = value; OnPropertyChanged(); }
        }

        // Propiedades para Solicitud de Recursos/Refrigerios
        private string _cantidadRefrigerios = "";
        public string CantidadRefrigerios
        {
            get => _cantidadRefrigerios;
            set { _cantidadRefrigerios = value; OnPropertyChanged(); }
        }

        private string _nombreActividad = "";
        public string NombreActividad
        {
            get => _nombreActividad;
            set { _nombreActividad = value; OnPropertyChanged(); }
        }

        private string _horaEvento = "";
        public string HoraEvento
        {
            get => _horaEvento;
            set { _horaEvento = value; OnPropertyChanged(); }
        }

        private Services.DocumentTemplateType _currentTemplateType = Services.DocumentTemplateType.JustificacionEstudiante;

        public ObservableCollection<EstudianteCartaInfo> Estudiantes { get; set; }

        public CartasUserControlView()
        {
            InitializeComponent();

            // Datos de prueba basados en EstudianteResponseDTO
            Estudiantes = new ObservableCollection<EstudianteCartaInfo>
            {
                new EstudianteCartaInfo(
                    new EstudianteResponseDTO
                    {
                        NombreCompleto = "Juan Pérez",
                        Carnet = "2021-0001U",
                        CarreraNombre = "Ingeniería de Sistemas",
                        Año = 3,
                        Grupo = "3M1-IS",
                        DisciplinaNombre = "Danza Folclórica"
                    },
                    "Incapacidad médica"
                ),
                new EstudianteCartaInfo(
                    new EstudianteResponseDTO
                    {
                        NombreCompleto = "María González",
                        Carnet = "2022-0045U",
                        CarreraNombre = "Arquitectura",
                        Año = 2,
                        Grupo = "2T1-ARQ",
                        DisciplinaNombre = "Teatro"
                    },
                    "Viaje académico"
                ),
                new EstudianteCartaInfo(
                    new EstudianteResponseDTO
                    {
                        NombreCompleto = "Carlos López",
                        Carnet = "2020-0123U",
                        CarreraNombre = "Ingeniería Civil",
                        Año = 4,
                        Grupo = "4M2-IC",
                        DisciplinaNombre = "Música"
                    },
                    "Compromiso familiar"
                )
            };

            EstudianteSeleccionado = Estudiantes[0]; // Juan Pérez por defecto

            // Valores de prueba para justificación
            DestinatarioNombre = "Silvio Solorzano Mondy";
            DestinatarioCargo = "Transferencia de calor";
            DestinatarioInstitucion = "UNI-RUPAP";
            FechaEvento = "27 de marzo";
            HoraInicio = "07:00 am";
            HoraFin = "04:00 pm";
            NombreEvento = "Día Internacional del Teatro y Natalicio del Poeta Guerrillero \"Leonel Rugama\"";
            DisciplinaEvento = "Teatro";
            AsignaturaEstudiante = "Transferencia de Calor";

            // Actualizar valores de firma para Solicitud de Espacios
            FirmaNombre = "Cleopatra Dadmira Morales Montiel";
            FirmaCargo = "Jefa de Departamento de Cultura";

            // Valores de prueba para Solicitud de Recursos/Refrigerios
            CantidadRefrigerios = "90";
            NombreActividad = "Festival de Bienvenida a las Fiestas Patrias. \"Todo San Jacinto\"";
            HoraEvento = "04:00 pm";

        }
            
            // DataContext = this; // Removed to allow ViewModel injection

        private void OpenDocumentPreview(object sender, RoutedEventArgs e)
        {
            // Determinar qué plantilla cargar según el Tag del botón
            if (sender is Button button && button.Tag is string tag)
            {
                // Usar el Tag para identificar el tipo de documento directamente
                _currentTemplateType = tag switch
                {
                    "SolicitudTransporte" => Services.DocumentTemplateType.SolicitudTransporte,
                    "JustificacionEstudiante" => Services.DocumentTemplateType.JustificacionEstudiante,
                    "SolicitudRecursos" => Services.DocumentTemplateType.SolicitudRecursos,
                    "SolicitudEspacios" => Services.DocumentTemplateType.SolicitudEspacios,
                    _ => Services.DocumentTemplateType.JustificacionEstudiante // Valor por defecto
                };
            }
            
            // Cargar la plantilla correspondiente
            LoadDocumentTemplate(_currentTemplateType);
            
            DocumentPreviewOverlay.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Carga una plantilla de documento en el FlowDocumentScrollViewer
        /// </summary>
        private void LoadDocumentTemplate(Services.DocumentTemplateType templateType)
        {
            try
            {
                // Establecer el título del documento según el tipo
                TituloDocumento = GetDocumentTitle(templateType);
                
                // Mostrar el formulario correcto según el tipo de documento
                MostrarFormularioCorrecto(templateType);
                
                // Cargar la plantilla usando el servicio
                var template = Services.DocumentTemplateLoader.LoadTemplate(templateType);
                
                // Establecer el DataContext para los bindings
                template.DataContext = this;
                
                // Asignar el documento al ScrollViewer
                DocumentScrollViewer.Document = template;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la plantilla: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        /// <summary>
        /// Muestra el formulario de entrada correcto según el tipo de documento
        /// </summary>
        private void MostrarFormularioCorrecto(Services.DocumentTemplateType templateType)
        {
            // Ocultar todos los formularios
            FormularioTransporte.Visibility = Visibility.Collapsed;
            FormularioJustificacion.Visibility = Visibility.Collapsed;
            FormularioEspacios.Visibility = Visibility.Collapsed;

            // Mostrar el formulario correspondiente
            switch (templateType)
            {
                case Services.DocumentTemplateType.SolicitudTransporte:
                    FormularioTransporte.Visibility = Visibility.Visible;
                    break;
                case Services.DocumentTemplateType.JustificacionEstudiante:
                    FormularioJustificacion.Visibility = Visibility.Visible;
                    break;
                case Services.DocumentTemplateType.SolicitudRecursos:
                case Services.DocumentTemplateType.SolicitudEspacios:
                    FormularioEspacios.Visibility = Visibility.Visible;
                    break;
            }
        }

        /// <summary>
        /// Obtiene el título del documento según el tipo de plantilla
        /// </summary>
        private string GetDocumentTitle(Services.DocumentTemplateType templateType)
        {
            return templateType switch
            {
                Services.DocumentTemplateType.JustificacionEstudiante => "Carta de Justificación - Inasistencia",
                Services.DocumentTemplateType.SolicitudTransporte => "SOLICITUD DE TRANSPORTE",
                Services.DocumentTemplateType.SolicitudRecursos => "SOLICITUD DE RECURSOS / REFRIGERIOS",
                Services.DocumentTemplateType.SolicitudEspacios => "SOLICITUD DE ESPACIOS",
                _ => "Documento"
            };
        }
        
        /// <summary>
        /// Busca un elemento visual hijo por tipo
        /// </summary>
        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T result)
                    return result;
                
                var childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null)
                    return childOfChild;
            }
            return null;
        }

        private void CloseDocumentPreview(object sender, RoutedEventArgs e)
        {
            DocumentPreviewOverlay.Visibility = Visibility.Collapsed;
        }

        private void PrintDocument(object sender, RoutedEventArgs e)
        {
            try
            {
                // Verificar que hay un documento cargado
                if (DocumentScrollViewer.Document == null)
                {
                    MessageBox.Show("No hay un documento cargado para imprimir.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                
                // Crear el diálogo de impresión
                PrintDialog printDialog = new PrintDialog();
                
                // Mostrar el diálogo y verificar si el usuario confirmó
                if (printDialog.ShowDialog() == true)
                {
                    // Obtener el FlowDocument actual
                    FlowDocument flowDocument = DocumentScrollViewer.Document;
                    
                    // Configurar el tamaño de página del documento
                    flowDocument.PageHeight = printDialog.PrintableAreaHeight;
                    flowDocument.PageWidth = printDialog.PrintableAreaWidth;
                    flowDocument.PagePadding = new Thickness(50);
                    flowDocument.ColumnGap = 0;
                    flowDocument.ColumnWidth = printDialog.PrintableAreaWidth;
                    
                    // Obtener el DocumentPaginator del FlowDocument
                    IDocumentPaginatorSource documentPaginatorSource = flowDocument;
                    
                    // Imprimir el documento
                    printDialog.PrintDocument(documentPaginatorSource.DocumentPaginator, "Carta - SICAP");
                    
                    MessageBox.Show("Documento enviado a la impresora correctamente.", "Impresión", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al imprimir el documento: {ex.Message}", "Error de Impresión", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Wrapper para agregar propiedades adicionales necesarias para cartas
    public class EstudianteCartaInfo
    {
        private readonly EstudianteResponseDTO _dto;
        private readonly string _motivo;

        public EstudianteCartaInfo(EstudianteResponseDTO dto, string motivo)
        {
            _dto = dto;
            _motivo = motivo;
        }

        public string Nombre => _dto.NombreCompleto;
        public string Carnet => _dto.Carnet;
        public string Carrera => _dto.CarreraNombre;
        public string Año => _dto.Año.ToString() + "°";
        public string Grupo => _dto.Grupo;
        public string Disciplina => _dto.DisciplinaNombre;
        public string Motivo => _motivo;
        public string DisplayText => $"{_dto.NombreCompleto} - {_dto.Carnet}";
    }
}

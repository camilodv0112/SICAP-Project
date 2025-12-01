using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Modelos.ModelsDTO.Estudiante;
using Modelos.ModelsDTO.CartaGenerada;
using Newtonsoft.Json;
using Presentacion.ViewModels;

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

        public ObservableCollection<EstudianteCartaInfo> Estudiantes { get; set; } = new ObservableCollection<EstudianteCartaInfo>();
        
        public CartasUserControlView()
        {
            InitializeComponent();
            this.Loaded += CartasUserControlView_Loaded;
        }

        private void CartasUserControlView_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is CartasViewModel viewModel)
            {
                // Sincronizar estudiantes cuando cambie la colección del ViewModel
                viewModel.Estudiantes.CollectionChanged += (s, args) => UpdateEstudiantesList(viewModel);
                
                // Carga inicial si ya hay datos
                if (viewModel.Estudiantes.Count > 0)
                {
                    UpdateEstudiantesList(viewModel);
                }
            }
        }

        private void UpdateEstudiantesList(CartasViewModel viewModel)
        {
            Estudiantes.Clear();
            foreach (var est in viewModel.Estudiantes)
            {
                Estudiantes.Add(new EstudianteCartaInfo(est, ""));
            }
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

        private async void PrintDocument(object sender, RoutedEventArgs e)
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

                    // Guardar en historial
                    await SaveToHistory();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al imprimir el documento: {ex.Message}", "Error de Impresión", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task SaveToHistory()
        {
            try
            {
                var viewModel = this.DataContext as CartasViewModel;
                if (viewModel == null) return;

                var datos = new Dictionary<string, object>
                {
                    // Transporte
                    { "NombreSolicitante", NombreSolicitante },
                    { "DependenciaSolicitante", DependenciaSolicitante },
                    { "ResponsableActividad", ResponsableActividad },
                    { "NumeroPasajeros", NumeroPasajeros },
                    { "DescripcionActividad", DescripcionActividad },
                    { "Destino", Destino },
                    { "Departamento", Departamento },
                    { "Municipio", Municipio },
                    { "KmAproximados", KmAproximados },
                    { "FechaSalida", FechaSalida },
                    { "HoraSalida", HoraSalida },
                    { "FechaRegreso", FechaRegreso },
                    { "HoraRegreso", HoraRegreso },
                    { "TipoVehiculo", TipoVehiculo },
                    { "NumeroPlaca", NumeroPlaca },
                    { "ColorVehiculo", ColorVehiculo },

                    // Justificación
                    { "DestinatarioNombre", DestinatarioNombre },
                    { "DestinatarioCargo", DestinatarioCargo },
                    { "DestinatarioInstitucion", DestinatarioInstitucion },
                    { "FechaEvento", FechaEvento },
                    { "HoraInicio", HoraInicio },
                    { "HoraFin", HoraFin },
                    { "NombreEvento", NombreEvento },
                    { "DescripcionEvento", DescripcionEvento },
                    { "DisciplinaEvento", DisciplinaEvento },
                    { "AsignaturaEstudiante", AsignaturaEstudiante },
                    // Guardamos el estudiante seleccionado si existe
                    { "EstudianteSeleccionado", EstudianteSeleccionado },

                    // Recursos/Espacios
                    { "CantidadRefrigerios", CantidadRefrigerios },
                    { "NombreActividad", NombreActividad },
                    { "HoraEvento", HoraEvento },

                    // Comunes
                    { "TituloDocumento", TituloDocumento },
                    { "FechaDocumento", FechaDocumento },
                    { "FirmaNombre", FirmaNombre },
                    { "FirmaCargo", FirmaCargo },
                    { "FirmaDepartamento", FirmaDepartamento },
                    { "NotasAdicionales", NotasAdicionales }
                };

                var json = JsonConvert.SerializeObject(datos);

                // Mapeo simple de ID de plantilla (asumiendo orden en enum)
                // JustificacionEstudiante = 0 -> DB 1
                // SolicitudTransporte = 1 -> DB 2
                // SolicitudRecursos = 2 -> DB 3
                // SolicitudEspacios = 3 -> DB 4
                // Ajustar según IDs reales en BD
                int plantillaId = (int)_currentTemplateType + 1; 
                // Nota: JustificacionEstudiante es 0 en enum, pero si en BD es 2, esto fallará.
                // Asumiremos: 
                // 1: JustificacionEstudiante
                // 2: SolicitudTransporte
                // 3: SolicitudRecursos
                // 4: SolicitudEspacios
                // Re-mapeo manual para seguridad:
                plantillaId = _currentTemplateType switch
                {
                    Services.DocumentTemplateType.JustificacionEstudiante => 1,
                    Services.DocumentTemplateType.SolicitudTransporte => 2,
                    Services.DocumentTemplateType.SolicitudRecursos => 3,
                    Services.DocumentTemplateType.SolicitudEspacios => 4,
                    _ => 1
                };

                var dto = new CartaGeneradaCreateDTO
                {
                    PlantillaId = plantillaId,
                    NombreCarta = TituloDocumento,
                    Datos = json,
                    UsuarioPersonalID = 1, // TODO: Obtener usuario real
                    FechaGeneracion = DateTime.Now,
                    RutaPDF = "N/A" // Valor temporal para cumplir validación
                };

                await viewModel.SaveCartaAsync(dto);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar historial: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ReprintFromHistory(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is CartaGeneradaResponseDTO carta)
            {
                try
                {
                    var datos = JsonConvert.DeserializeObject<Dictionary<string, object>>(carta.Datos);
                    if (datos == null) return;

                    // Restaurar propiedades
                    // Helper local para obtener string seguro
                    string GetStr(string key) => datos.ContainsKey(key) && datos[key] != null ? datos[key].ToString() : "";

                    NombreSolicitante = GetStr("NombreSolicitante");
                    DependenciaSolicitante = GetStr("DependenciaSolicitante");
                    ResponsableActividad = GetStr("ResponsableActividad");
                    NumeroPasajeros = GetStr("NumeroPasajeros");
                    DescripcionActividad = GetStr("DescripcionActividad");
                    Destino = GetStr("Destino");
                    Departamento = GetStr("Departamento");
                    Municipio = GetStr("Municipio");
                    KmAproximados = GetStr("KmAproximados");
                    FechaSalida = GetStr("FechaSalida");
                    HoraSalida = GetStr("HoraSalida");
                    FechaRegreso = GetStr("FechaRegreso");
                    HoraRegreso = GetStr("HoraRegreso");
                    TipoVehiculo = GetStr("TipoVehiculo");
                    NumeroPlaca = GetStr("NumeroPlaca");
                    ColorVehiculo = GetStr("ColorVehiculo");

                    DestinatarioNombre = GetStr("DestinatarioNombre");
                    DestinatarioCargo = GetStr("DestinatarioCargo");
                    DestinatarioInstitucion = GetStr("DestinatarioInstitucion");
                    FechaEvento = GetStr("FechaEvento");
                    HoraInicio = GetStr("HoraInicio");
                    HoraFin = GetStr("HoraFin");
                    NombreEvento = GetStr("NombreEvento");
                    DescripcionEvento = GetStr("DescripcionEvento");
                    DisciplinaEvento = GetStr("DisciplinaEvento");
                    AsignaturaEstudiante = GetStr("AsignaturaEstudiante");
                    
                    // Restaurar EstudianteSeleccionado es complejo porque es un objeto.
                    // Intentamos deserializarlo si existe
                    if (datos.ContainsKey("EstudianteSeleccionado") && datos["EstudianteSeleccionado"] != null)
                    {
                        try 
                        {
                            var estJson = datos["EstudianteSeleccionado"].ToString();
                            var est = JsonConvert.DeserializeObject<EstudianteCartaInfo>(estJson);
                            // Buscar si existe en la lista actual para seleccionarlo, o agregarlo
                            // Por simplicidad, si coincide el carnet, lo seleccionamos
                            if (est != null)
                            {
                                // Lógica simplificada: intentar seleccionar de la lista existente
                                // Si no está, tal vez agregarlo temporalmente?
                                // Por ahora, solo seteamos si encontramos coincidencia
                                foreach(var item in Estudiantes)
                                {
                                    if (item.Carnet == est.Carnet)
                                    {
                                        EstudianteSeleccionado = item;
                                        break;
                                    }
                                }
                            }
                        }
                        catch { /* Ignorar error al restaurar estudiante */ }
                    }

                    CantidadRefrigerios = GetStr("CantidadRefrigerios");
                    NombreActividad = GetStr("NombreActividad");
                    // HoraEvento ya está arriba

                    TituloDocumento = GetStr("TituloDocumento");
                    FechaDocumento = GetStr("FechaDocumento");
                    FirmaNombre = GetStr("FirmaNombre");
                    FirmaCargo = GetStr("FirmaCargo");
                    FirmaDepartamento = GetStr("FirmaDepartamento");
                    NotasAdicionales = GetStr("NotasAdicionales");

                    // Establecer tipo de plantilla
                    _currentTemplateType = carta.PlantillaId switch
                    {
                        1 => Services.DocumentTemplateType.JustificacionEstudiante,
                        2 => Services.DocumentTemplateType.SolicitudTransporte,
                        3 => Services.DocumentTemplateType.SolicitudRecursos,
                        4 => Services.DocumentTemplateType.SolicitudEspacios,
                        _ => Services.DocumentTemplateType.JustificacionEstudiante
                    };

                    // Cargar plantilla
                    LoadDocumentTemplate(_currentTemplateType);
                    DocumentPreviewOverlay.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                     MessageBox.Show($"Error al cargar carta del historial: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Wrapper para agregar propiedades adicionales necesarias para cartas
    // Wrapper para agregar propiedades adicionales necesarias para cartas
    public class EstudianteCartaInfo
    {
        public string Nombre { get; set; }
        public string Carnet { get; set; }
        public string Carrera { get; set; }
        public string Año { get; set; }
        public string Grupo { get; set; }
        public string Disciplina { get; set; }
        public string Motivo { get; set; }
        public string DisplayText => $"{Nombre} - {Carnet}";

        public EstudianteCartaInfo() { }

        public EstudianteCartaInfo(EstudianteResponseDTO dto, string motivo)
        {
            if (dto != null)
            {
                Nombre = dto.NombreCompleto;
                Carnet = dto.Carnet;
                Carrera = dto.CarreraNombre;
                Año = dto.Año.ToString() + "°";
                Grupo = dto.Grupo;
                Disciplina = dto.DisciplinaNombre;
            }
            Motivo = motivo;
        }
    }
}

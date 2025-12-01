using Presentacion.Core;
using Modelos.ModelsDTO.Evento;
using Modelos.ModelsDTO.Salon;
using Modelos.ModelsDTO.CategoriaNecesidad;
using Modelos.ModelsDTO.SubcategoriaNecesidad;
using Modelos.ModelsDTO.NecesidadEvento;
using Modelos.ModelsDTO.Personal;
using Modelos.ModelsDTO.Estudiante;
using Modelos.ModelsDTO.ParticipanteEvento;
using Modelos.ModelsDTO.ResponsableEvento;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Presentacion.ViewModels
{
    public class EventosViewModel : ViewModelBase
    {
        private readonly ApiClient _apiClient;

        public ObservableCollection<EventoResponseDTO> Eventos { get; set; }

        private EventoResponseDTO? _selectedEvento;
        public EventoResponseDTO? SelectedEvento
        {
            get => _selectedEvento;
            set => SetProperty(ref _selectedEvento, value);
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private string _errorMessage = string.Empty;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        // Propiedades para Creación de Evento
        public ObservableCollection<SalonResponseDTO> SalonesDisponibles { get; set; } = new ObservableCollection<SalonResponseDTO>();

        private SalonResponseDTO? _selectedSalon;
        public SalonResponseDTO? SelectedSalon
        {
            get => _selectedSalon;
            set => SetProperty(ref _selectedSalon, value);
        }

        private string _nuevoEventoNombre = string.Empty;
        public string NuevoEventoNombre { get => _nuevoEventoNombre; set => SetProperty(ref _nuevoEventoNombre, value); }

        private string _nuevoEventoDescripcion = string.Empty;
        public string NuevoEventoDescripcion { get => _nuevoEventoDescripcion; set => SetProperty(ref _nuevoEventoDescripcion, value); }

        private DateTime _nuevoEventoFecha = DateTime.Today;
        public DateTime NuevoEventoFecha { get => _nuevoEventoFecha; set => SetProperty(ref _nuevoEventoFecha, value); }

        private string _nuevoEventoHoraInicio = "08:00";
        public string NuevoEventoHoraInicio { get => _nuevoEventoHoraInicio; set => SetProperty(ref _nuevoEventoHoraInicio, value); }

        private string _nuevoEventoHoraFin = "10:00";
        public string NuevoEventoHoraFin { get => _nuevoEventoHoraFin; set => SetProperty(ref _nuevoEventoHoraFin, value); }

        // Propiedades para Recursos (Necesidades)
        public ObservableCollection<CategoriaNecesidadResponseDTO> CategoriasDisponibles { get; set; } = new ObservableCollection<CategoriaNecesidadResponseDTO>();
        public ObservableCollection<SubcategoriaNecesidadResponseDTO> SubcategoriasDisponibles { get; set; } = new ObservableCollection<SubcategoriaNecesidadResponseDTO>();
        public ObservableCollection<SubcategoriaNecesidadResponseDTO> SubcategoriasFiltradas { get; set; } = new ObservableCollection<SubcategoriaNecesidadResponseDTO>();
        
        // Wrapper para mostrar en la DataGrid (incluye nombre de subcategoría)
        public class NecesidadItemDisplay
        {
            public int SubcategoriaID { get; set; }
            public string SubcategoriaNombre { get; set; }
            public int Cantidad { get; set; }
            public string Observaciones { get; set; }
        }
        public ObservableCollection<NecesidadItemDisplay> NecesidadesAgregadas { get; set; } = new ObservableCollection<NecesidadItemDisplay>();

        private CategoriaNecesidadResponseDTO? _selectedCategoria;
        public CategoriaNecesidadResponseDTO? SelectedCategoria
        {
            get => _selectedCategoria;
            set
            {
                if (SetProperty(ref _selectedCategoria, value))
                {
                    FilterSubcategorias();
                    OnPropertyChanged(nameof(IsCategoriaSelected));
                }
            }
        }

        public bool IsCategoriaSelected => SelectedCategoria != null;

        private SubcategoriaNecesidadResponseDTO? _selectedSubcategoria;
        public SubcategoriaNecesidadResponseDTO? SelectedSubcategoria
        {
            get => _selectedSubcategoria;
            set => SetProperty(ref _selectedSubcategoria, value);
        }

        private int _nuevaNecesidadCantidad = 1;
        public int NuevaNecesidadCantidad { get => _nuevaNecesidadCantidad; set => SetProperty(ref _nuevaNecesidadCantidad, value); }

        private string _nuevaNecesidadObservacion = string.Empty;
        public string NuevaNecesidadObservacion { get => _nuevaNecesidadObservacion; set => SetProperty(ref _nuevaNecesidadObservacion, value); }


        // Propiedades para Participantes y Responsables
        public class PersonalSelectionItem : ViewModelBase
        {
            public PersonalResponseDTO Personal { get; set; }
            private bool _isSelected;
            public bool IsSelected { get => _isSelected; set => SetProperty(ref _isSelected, value); }
        }

        public class EstudianteSelectionItem : ViewModelBase
        {
            public EstudianteResponseDTO Estudiante { get; set; }
            private bool _isSelected;
            public bool IsSelected { get => _isSelected; set => SetProperty(ref _isSelected, value); }
        }

        public ObservableCollection<PersonalSelectionItem> PersonalDisponible { get; set; } = new ObservableCollection<PersonalSelectionItem>();
        public ObservableCollection<EstudianteSelectionItem> EstudiantesDisponibles { get; set; } = new ObservableCollection<EstudianteSelectionItem>();


        // Comandos
        public ICommand LoadEventosCommand { get; }
        public ICommand CreateEventoCommand { get; }
        public ICommand DeleteEventoCommand { get; }
        public ICommand SaveEventoCommand { get; }
        public ICommand CancelCreateCommand { get; }
        public ICommand AddNecesidadCommand { get; }
        public ICommand RemoveNecesidadCommand { get; }

        // Referencia a la ventana modal
        private Window? _currentModal;

        public EventosViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            Eventos = new ObservableCollection<EventoResponseDTO>();

            // Inicializar Comandos
            LoadEventosCommand = new RelayCommand(async o => await LoadEventosAsync());
            CreateEventoCommand = new RelayCommand(async o => await OpenCreateModal());
            DeleteEventoCommand = new RelayCommand(async o => await ExecuteDeleteEvento(), o => SelectedEvento != null);
            
            SaveEventoCommand = new RelayCommand(async o => await ExecuteSaveEvento());
            CancelCreateCommand = new RelayCommand(o => CloseModal());
            
            AddNecesidadCommand = new RelayCommand(o => ExecuteAddNecesidad());
            RemoveNecesidadCommand = new RelayCommand(o => ExecuteRemoveNecesidad(o));

            // Cargar eventos al iniciar
            _ = LoadEventosAsync();
        }

        private async Task LoadEventosAsync()
        {
            IsLoading = true;
            ErrorMessage = string.Empty;

            try
            {
                var eventos = await _apiClient.Evento.GetAllAsync();
                Eventos.Clear();
                foreach (var evento in eventos)
                {
                    Eventos.Add(evento);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al cargar eventos: {ex.Message}";
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task OpenCreateModal()
        {
            // 1. Limpiar campos
            NuevoEventoNombre = string.Empty;
            NuevoEventoDescripcion = string.Empty;
            NuevoEventoFecha = DateTime.Today.AddDays(1);
            NuevoEventoHoraInicio = "09:00";
            NuevoEventoHoraFin = "11:00";
            SelectedSalon = null;
            ErrorMessage = string.Empty;

            // Limpiar recursos
            SelectedCategoria = null;
            SelectedSubcategoria = null;
            NuevaNecesidadCantidad = 1;
            NuevaNecesidadObservacion = string.Empty;
            NecesidadesAgregadas.Clear();

            // 2. Cargar Datos (Salones, Categorías, Personas)
            IsLoading = true;
            await LoadSalonesAsync();
            await LoadRecursosCatalogosAsync();
            await LoadPersonasAsync();
            IsLoading = false;

            // 3. Abrir Modal
            _currentModal = new Views.EventoFormView
            {
                DataContext = this,
                Owner = Application.Current.MainWindow
            };
            _currentModal.ShowDialog();
        }

        private async Task LoadSalonesAsync()
        {
            try
            {
                var salones = await _apiClient.Salon.GetAllAsync();
                SalonesDisponibles.Clear();
                foreach (var s in salones)
                {
                    SalonesDisponibles.Add(s);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar salones: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task LoadRecursosCatalogosAsync()
        {
            try
            {
                var categorias = await _apiClient.CategoriaNecesidad.GetAllAsync();
                CategoriasDisponibles.Clear();
                foreach (var c in categorias) CategoriasDisponibles.Add(c);

                var subcategorias = await _apiClient.SubcategoriaNecesidad.GetAllAsync();
                SubcategoriasDisponibles.Clear();
                foreach (var s in subcategorias) SubcategoriasDisponibles.Add(s);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar catálogos de recursos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task LoadPersonasAsync()
        {
            try
            {
                var personal = await _apiClient.Personal.GetAllAsync();
                PersonalDisponible.Clear();
                foreach (var p in personal)
                {
                    PersonalDisponible.Add(new PersonalSelectionItem { Personal = p, IsSelected = false });
                }

                var estudiantes = await _apiClient.Estudiante.GetAllAsync();
                EstudiantesDisponibles.Clear();
                foreach (var e in estudiantes)
                {
                    EstudiantesDisponibles.Add(new EstudianteSelectionItem { Estudiante = e, IsSelected = false });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar personas: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FilterSubcategorias()
        {
            SubcategoriasFiltradas.Clear();
            if (SelectedCategoria != null)
            {
                var filtered = SubcategoriasDisponibles.Where(s => s.CategoriaID == SelectedCategoria.CategoriaID);
                foreach (var s in filtered) SubcategoriasFiltradas.Add(s);
            }
        }

        private void ExecuteAddNecesidad()
        {
            if (SelectedSubcategoria == null)
            {
                MessageBox.Show("Seleccione una subcategoría.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (NuevaNecesidadCantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a 0.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NecesidadesAgregadas.Add(new NecesidadItemDisplay
            {
                SubcategoriaID = SelectedSubcategoria.SubcategoriaID,
                SubcategoriaNombre = SelectedSubcategoria.Nombre,
                Cantidad = NuevaNecesidadCantidad,
                Observaciones = NuevaNecesidadObservacion
            });

            // Reset campos de necesidad
            NuevaNecesidadCantidad = 1;
            NuevaNecesidadObservacion = string.Empty;
        }

        private void ExecuteRemoveNecesidad(object parameter)
        {
            if (parameter is NecesidadItemDisplay item)
            {
                NecesidadesAgregadas.Remove(item);
            }
        }

        private void CloseModal()
        {
            _currentModal?.Close();
            _currentModal = null;
        }

        private async Task ExecuteSaveEvento()
        {
            // Validaciones Básicas
            if (string.IsNullOrWhiteSpace(NuevoEventoNombre))
            {
                ErrorMessage = "El nombre del evento es obligatorio.";
                return;
            }
            if (SelectedSalon == null)
            {
                ErrorMessage = "Debe seleccionar un salón.";
                return;
            }
            
            if (!TimeSpan.TryParse(NuevoEventoHoraInicio, out TimeSpan horaInicio) ||
                !TimeSpan.TryParse(NuevoEventoHoraFin, out TimeSpan horaFin))
            {
                ErrorMessage = "Formato de hora inválido (Use HH:mm).";
                return;
            }

            if (horaFin <= horaInicio)
            {
                ErrorMessage = "La hora final debe ser posterior a la hora de inicio.";
                return;
            }

            IsLoading = true;
            ErrorMessage = string.Empty;
            var errores = new System.Collections.Generic.List<string>();
            int exitos = 0;

            try
            {
                // 1. Crear Evento
                var nuevoEvento = new EventoCreateDTO
                {
                    Nombre = NuevoEventoNombre,
                    Descripcion = NuevoEventoDescripcion,
                    Fecha = NuevoEventoFecha,
                    HoraInicio = horaInicio,
                    HoraFinal = horaFin,
                    Salon = SelectedSalon.SalonID
                };

                var eventoCreado = await _apiClient.Evento.CreateAsync(nuevoEvento);
                
                if (eventoCreado != null)
                {
                    // 2. Agregar Necesidades
                    foreach (var necesidad in NecesidadesAgregadas)
                    {
                        try
                        {
                            var necesidadDTO = new NecesidadEventoCreateDTO
                            {
                                EventoID = eventoCreado.EventoID,
                                SubcategoriaID = necesidad.SubcategoriaID,
                                Cantidad = necesidad.Cantidad,
                                Observaciones = necesidad.Observaciones
                            };
                            await _apiClient.NecesidadEvento.CreateAsync(necesidadDTO);
                        }
                        catch (Exception ex)
                        {
                            errores.Add($"Error al agregar recurso '{necesidad.SubcategoriaNombre}': {ex.Message}");
                        }
                    }

                    // 3. Agregar Responsables (Personal)
                    var responsablesSeleccionados = PersonalDisponible.Where(p => p.IsSelected).ToList();
                    foreach (var resp in responsablesSeleccionados)
                    {
                        try
                        {
                            var respDTO = new ResponsableEventoCreateDTO
                            {
                                EventoID = eventoCreado.EventoID,
                                PersonalID = resp.Personal.PersonalID
                            };
                            await _apiClient.ResponsableEvento.CreateAsync(respDTO);
                            exitos++;
                        }
                        catch (Exception ex)
                        {
                            errores.Add($"Error al asignar responsable '{resp.Personal.NombreCompleto}': {ex.Message}");
                        }
                    }

                    // 4. Agregar Participantes (Estudiantes)
                    var estudiantesSeleccionados = EstudiantesDisponibles.Where(e => e.IsSelected).ToList();
                    foreach (var est in estudiantesSeleccionados)
                    {
                        try
                        {
                            var partDTO = new ParticipanteEventoCreateDTO
                            {
                                EventoID = eventoCreado.EventoID,
                                EstudianteID = est.Estudiante.EstudianteID
                            };
                            await _apiClient.ParticipanteEvento.CreateAsync(partDTO);
                            exitos++;
                        }
                        catch (Exception ex)
                        {
                            errores.Add($"Error al asignar estudiante '{est.Estudiante.NombreCompleto}': {ex.Message}");
                        }
                    }

                    // Reporte Final
                    if (errores.Count > 0)
                    {
                        string msg = $"El evento se creó, pero hubo errores al asignar detalles:\n\n";
                        msg += string.Join("\n", errores.Take(5));
                        if (errores.Count > 5) msg += "\n...";
                        
                        MessageBox.Show(msg, "Advertencia: Guardado Parcial", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Evento y todos sus detalles guardados exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    CloseModal();
                    await LoadEventosAsync();
                }
            }
            catch (Exception ex)
            {
                var fullMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    fullMessage += $"\nDetalle: {ex.InnerException.Message}";
                }
                ErrorMessage = $"Error crítico al crear evento: {fullMessage}";
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task ExecuteDeleteEvento()
        {
            if (SelectedEvento == null) return;

            var result = MessageBox.Show(
                $"¿Está seguro de eliminar el evento '{SelectedEvento.Nombre}'?",
                "Confirmar Eliminación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await _apiClient.Evento.DeleteAsync(SelectedEvento.EventoID);
                    Eventos.Remove(SelectedEvento);
                    MessageBox.Show("Evento eliminado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar evento: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}

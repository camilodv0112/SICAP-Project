using Presentacion.Core;
using Modelos.ModelsDTO.Estudiante;
using Modelos.ModelsDTO.Municipio;
using Modelos.ModelsDTO.Carrera;
using Modelos.ModelsDTO.DisciplinaArtistica;
using System.Collections.ObjectModel;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Linq;
using System.Windows.Controls;

namespace Presentacion.ViewModels
{
    public class EstudiantesViewModel : ViewModelBase
    {
        private readonly ApiClient _apiClient;
        private Window? _currentModal;

        public ObservableCollection<EstudianteResponseDTO> Estudiantes { get; set; }

        private EstudianteResponseDTO? _selectedEstudiante;
        public EstudianteResponseDTO? SelectedEstudiante
        {
            get => _selectedEstudiante;
            set => SetProperty(ref _selectedEstudiante, value);
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

        // Propiedades para Creación
        private EstudianteCreateDTO _nuevoEstudiante = new EstudianteCreateDTO();
        public EstudianteCreateDTO NuevoEstudiante
        {
            get => _nuevoEstudiante;
            set => SetProperty(ref _nuevoEstudiante, value);
        }

        public ObservableCollection<MunicipioResponseDTO> MunicipiosDisponibles { get; set; } = new ObservableCollection<MunicipioResponseDTO>();
        public ObservableCollection<CarreraResponseDTO> CarrerasDisponibles { get; set; } = new ObservableCollection<CarreraResponseDTO>();
        public ObservableCollection<DisciplinaArtisticaResponseDTO> DisciplinasDisponibles { get; set; } = new ObservableCollection<DisciplinaArtisticaResponseDTO>();

        private MunicipioResponseDTO? _selectedMunicipio;
        public MunicipioResponseDTO? SelectedMunicipio
        {
            get => _selectedMunicipio;
            set
            {
                if (SetProperty(ref _selectedMunicipio, value) && value != null)
                {
                    NuevoEstudiante.Municipio = value.MunicipioID;
                }
            }
        }

        private CarreraResponseDTO? _selectedCarrera;
        public CarreraResponseDTO? SelectedCarrera
        {
            get => _selectedCarrera;
            set
            {
                if (SetProperty(ref _selectedCarrera, value) && value != null)
                {
                    NuevoEstudiante.Carrera = value.CarreraID;
                }
            }
        }

        private DisciplinaArtisticaResponseDTO? _selectedDisciplina;
        public DisciplinaArtisticaResponseDTO? SelectedDisciplina
        {
            get => _selectedDisciplina;
            set
            {
                if (SetProperty(ref _selectedDisciplina, value) && value != null)
                {
                    NuevoEstudiante.Disciplina = value.DisciplinaID;
                }
            }
        }

        public ICommand LoadEstudiantesCommand { get; }
        public ICommand CreateEstudianteCommand { get; }
        public ICommand DeleteEstudianteCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public EstudiantesViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            Estudiantes = new ObservableCollection<EstudianteResponseDTO>();

            LoadEstudiantesCommand = new RelayCommand(async o => await LoadEstudiantesAsync());
            CreateEstudianteCommand = new RelayCommand(async o => await ExecuteCreateEstudiante());
            DeleteEstudianteCommand = new RelayCommand(async o => await ExecuteDeleteEstudiante(), o => SelectedEstudiante != null);
            SaveCommand = new RelayCommand(async o => await ExecuteSaveEstudiante());
            CancelCommand = new RelayCommand(o => ExecuteCancelEstudiante());

            _ = LoadEstudiantesAsync();
        }

        private async Task LoadEstudiantesAsync()
        {
            IsLoading = true;
            ErrorMessage = string.Empty;

            try
            {
                var estudiantesList = await _apiClient.Estudiante.GetAllAsync();
                Estudiantes.Clear();
                foreach (var item in estudiantesList)
                {
                    Estudiantes.Add(item);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al cargar estudiantes: {ex.Message}";
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task LoadCatalogosAsync()
        {
            try
            {
                var municipios = await _apiClient.Municipio.GetAllAsync();
                MunicipiosDisponibles.Clear();
                foreach (var m in municipios) MunicipiosDisponibles.Add(m);

                var carreras = await _apiClient.Carrera.GetAllAsync();
                CarrerasDisponibles.Clear();
                foreach (var c in carreras) CarrerasDisponibles.Add(c);

                var disciplinas = await _apiClient.DisciplinaArtistica.GetAllAsync();
                DisciplinasDisponibles.Clear();
                foreach (var d in disciplinas) DisciplinasDisponibles.Add(d);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar catálogos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task ExecuteCreateEstudiante()
        {
            NuevoEstudiante = new EstudianteCreateDTO { Sexo = "M", Año = 1, Grupo = "A" }; // Valores por defecto
            SelectedMunicipio = null;
            SelectedCarrera = null;
            SelectedDisciplina = null;
            ErrorMessage = string.Empty;

            IsLoading = true;
            await LoadCatalogosAsync();
            IsLoading = false;

            _currentModal = new Views.EstudianteFormView
            {
                DataContext = this,
                Owner = Application.Current.MainWindow
            };
            _currentModal.ShowDialog();
        }

        private async Task ExecuteSaveEstudiante()
        {
            if (string.IsNullOrWhiteSpace(NuevoEstudiante.PrimerNombre) ||
                string.IsNullOrWhiteSpace(NuevoEstudiante.PrimerApellido) ||
                string.IsNullOrWhiteSpace(NuevoEstudiante.Cedula) ||
                string.IsNullOrWhiteSpace(NuevoEstudiante.Carnet) ||
                SelectedMunicipio == null ||
                SelectedCarrera == null ||
                SelectedDisciplina == null)
            {
                ErrorMessage = "Por favor complete todos los campos obligatorios.";
                return;
            }

            // Validar ComboBoxItem para Sexo si viene del binding directo
            if (NuevoEstudiante.Sexo != "M" && NuevoEstudiante.Sexo != "F")
            {
                // Si el binding devuelve un ComboBoxItem, extraer el contenido (esto depende de cómo se bindea en la vista)
                // En este caso, asumimos que el binding en la vista usa SelectedValuePath="Content"
            }

            IsLoading = true;
            ErrorMessage = string.Empty;

            try
            {
                await _apiClient.Estudiante.CreateAsync(NuevoEstudiante);
                
                // Cerrar modal
                if (_currentModal != null)
                {
                    _currentModal.Close();
                    _currentModal = null;
                }

                MessageBox.Show("Estudiante registrado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                await LoadEstudiantesAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error al guardar estudiante.";
                try
                {
                    // Intentar parsear el error como JSON (formato de validación de API)
                    var errorObj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(ex.Message);
                    if (errorObj != null && errorObj.errors != null)
                    {
                        ErrorMessage += "\nErrores de validación:";
                        foreach (var error in errorObj.errors)
                        {
                            foreach (var msg in error.Value)
                            {
                                ErrorMessage += $"\n• {msg}";
                            }
                        }
                    }
                    else
                    {
                        ErrorMessage += $"\n{ex.Message}";
                    }
                }
                catch
                {
                    // Si no es JSON, mostrar el mensaje original
                    ErrorMessage += $"\n{ex.Message}";
                    if (ex.InnerException != null)
                    {
                        ErrorMessage += $"\nDetalles: {ex.InnerException.Message}";
                    }
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void ExecuteCancelEstudiante()
        {
            if (_currentModal != null)
            {
                _currentModal.Close();
                _currentModal = null;
            }
        }

        private async Task ExecuteDeleteEstudiante()
        {
            if (SelectedEstudiante == null) return;

            var result = MessageBox.Show(
                $"¿Está seguro de eliminar a '{SelectedEstudiante.NombreCompleto}'?",
                "Confirmar Eliminación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await _apiClient.Estudiante.DeleteAsync(SelectedEstudiante.EstudianteID);
                    Estudiantes.Remove(SelectedEstudiante);
                    MessageBox.Show("Estudiante eliminado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar estudiante: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}

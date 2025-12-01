using Presentacion.Core;
using Modelos.ModelsDTO.Personal;
using Modelos.ModelsDTO.Municipio;
using Modelos.ModelsDTO.Cargo;
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
    public class PersonalViewModel : ViewModelBase
    {
        private readonly ApiClient _apiClient;
        private Window? _currentModal;

        public ObservableCollection<PersonalResponseDTO> Personal { get; set; }

        private PersonalResponseDTO? _selectedPersonal;
        public PersonalResponseDTO? SelectedPersonal
        {
            get => _selectedPersonal;
            set => SetProperty(ref _selectedPersonal, value);
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
        private PersonalCreateDTO _nuevoPersonal = new PersonalCreateDTO();
        public PersonalCreateDTO NuevoPersonal
        {
            get => _nuevoPersonal;
            set => SetProperty(ref _nuevoPersonal, value);
        }

        public ObservableCollection<MunicipioResponseDTO> MunicipiosDisponibles { get; set; } = new ObservableCollection<MunicipioResponseDTO>();
        public ObservableCollection<CargoResponseDTO> CargosDisponibles { get; set; } = new ObservableCollection<CargoResponseDTO>();
        public ObservableCollection<DisciplinaArtisticaResponseDTO> DisciplinasDisponibles { get; set; } = new ObservableCollection<DisciplinaArtisticaResponseDTO>();

        private MunicipioResponseDTO? _selectedMunicipio;
        public MunicipioResponseDTO? SelectedMunicipio
        {
            get => _selectedMunicipio;
            set
            {
                if (SetProperty(ref _selectedMunicipio, value) && value != null)
                {
                    NuevoPersonal.Municipio = value.MunicipioID;
                }
            }
        }

        private CargoResponseDTO? _selectedCargo;
        public CargoResponseDTO? SelectedCargo
        {
            get => _selectedCargo;
            set
            {
                if (SetProperty(ref _selectedCargo, value) && value != null)
                {
                    NuevoPersonal.Cargo = value.CargoID;
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
                    NuevoPersonal.Disciplina = value.DisciplinaID;
                }
            }
        }

        public ICommand LoadPersonalCommand { get; }
        public ICommand CreatePersonalCommand { get; }
        public ICommand DeletePersonalCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public PersonalViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            Personal = new ObservableCollection<PersonalResponseDTO>();

            LoadPersonalCommand = new RelayCommand(async o => await LoadPersonalAsync());
            CreatePersonalCommand = new RelayCommand(async o => await ExecuteCreatePersonal());
            DeletePersonalCommand = new RelayCommand(async o => await ExecuteDeletePersonal(), o => SelectedPersonal != null);
            SaveCommand = new RelayCommand(async o => await ExecuteSavePersonal());
            CancelCommand = new RelayCommand(o => ExecuteCancelPersonal());

            _ = LoadPersonalAsync();
        }

        private async Task LoadPersonalAsync()
        {
            IsLoading = true;
            ErrorMessage = string.Empty;

            try
            {
                var personalList = await _apiClient.Personal.GetAllAsync();
                Personal.Clear();
                foreach (var item in personalList)
                {
                    Personal.Add(item);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al cargar personal: {ex.Message}";
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

                var cargos = await _apiClient.Cargo.GetAllAsync();
                CargosDisponibles.Clear();
                foreach (var c in cargos) CargosDisponibles.Add(c);

                var disciplinas = await _apiClient.DisciplinaArtistica.GetAllAsync();
                DisciplinasDisponibles.Clear();
                foreach (var d in disciplinas) DisciplinasDisponibles.Add(d);

                // DEBUG: Verificar conteos
                // MessageBox.Show($"Cargados: {MunicipiosDisponibles.Count} Municipios, {CargosDisponibles.Count} Cargos, {DisciplinasDisponibles.Count} Disciplinas", "Debug", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar catálogos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task ExecuteCreatePersonal()
        {
            NuevoPersonal = new PersonalCreateDTO { Sexo = "M" }; // Valores por defecto
            SelectedMunicipio = null;
            SelectedCargo = null;
            SelectedDisciplina = null;
            ErrorMessage = string.Empty;

            IsLoading = true;
            await LoadCatalogosAsync();
            IsLoading = false;

            _currentModal = new Views.PersonalFormView
            {
                DataContext = this,
                Owner = Application.Current.MainWindow
            };
            _currentModal.ShowDialog();
        }

        private async Task ExecuteSavePersonal()
        {
            if (string.IsNullOrWhiteSpace(NuevoPersonal.PrimerNombre) ||
                string.IsNullOrWhiteSpace(NuevoPersonal.PrimerApellido) ||
                string.IsNullOrWhiteSpace(NuevoPersonal.Cedula) ||
                SelectedMunicipio == null ||
                SelectedCargo == null ||
                SelectedDisciplina == null)
            {
                ErrorMessage = "Por favor complete todos los campos obligatorios.";
                return;
            }

            // Validar ComboBoxItem para Sexo si viene del binding directo
            if (NuevoPersonal.Sexo != "M" && NuevoPersonal.Sexo != "F")
            {
                // Si el binding devuelve un ComboBoxItem, extraer el contenido (esto depende de cómo se bindea en la vista)
                // En este caso, asumimos que el binding en la vista usa SelectedValuePath="Content"
            }

            IsLoading = true;
            ErrorMessage = string.Empty;

            try
            {
                await _apiClient.Personal.CreateAsync(NuevoPersonal);

                // Cerrar modal
                if (_currentModal != null)
                {
                    _currentModal.Close();
                    _currentModal = null;
                }

                MessageBox.Show("Personal registrado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                await LoadPersonalAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error al guardar personal.";
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

        private void ExecuteCancelPersonal()
        {
            if (_currentModal != null)
            {
                _currentModal.Close();
                _currentModal = null;
            }
        }

        private async Task ExecuteDeletePersonal()
        {
            if (SelectedPersonal == null) return;

            var result = MessageBox.Show(
                $"¿Está seguro de eliminar a '{SelectedPersonal.NombreCompleto}'?",
                "Confirmar Eliminación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await _apiClient.Personal.DeleteAsync(SelectedPersonal.PersonalID);
                    Personal.Remove(SelectedPersonal);
                    MessageBox.Show("Personal eliminado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar personal: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}

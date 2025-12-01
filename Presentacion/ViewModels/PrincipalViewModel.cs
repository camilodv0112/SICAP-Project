using Presentacion.Core;
using System.Windows.Input;

namespace Presentacion.ViewModels
{
    public class PrincipalViewModel : ViewModelBase
    {
        private object _currentView;
        private readonly ApiClient _apiClient;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public ICommand NavigateToHomeCommand { get; }
        public ICommand NavigateToPersonalCommand { get; }
        public ICommand NavigateToEstudiantesCommand { get; }
        public ICommand NavigateToEventosCommand { get; }
        public ICommand NavigateToCartasCommand { get; }

        public PrincipalViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;

            // Inicializar comandos
            NavigateToHomeCommand = new RelayCommand(o => CurrentView = new HomeViewModel(_apiClient));
            NavigateToPersonalCommand = new RelayCommand(o => CurrentView = new PersonalViewModel(_apiClient));
            NavigateToEstudiantesCommand = new RelayCommand(o => CurrentView = new EstudiantesViewModel(_apiClient));
            NavigateToEventosCommand = new RelayCommand(o => CurrentView = new EventosViewModel(_apiClient));
            NavigateToCartasCommand = new RelayCommand(o => CurrentView = new CartasViewModel(_apiClient));

            // Vista por defecto
            CurrentView = new HomeViewModel(_apiClient);
        }
    }
}

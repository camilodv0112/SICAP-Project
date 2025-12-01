using Presentacion.Core;
using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;

namespace Presentacion.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly ApiClient _apiClient;
        private string _usuario = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isLoading;

        public string Usuario
        {
            get => _usuario;
            set => SetProperty(ref _usuario, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public ICommand LoginCommand { get; }

        public event EventHandler? LoginSuccess;

        public LoginViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        private bool CanExecuteLogin(object obj)
        {
            return !string.IsNullOrWhiteSpace(Usuario) && !string.IsNullOrWhiteSpace(Password) && !IsLoading;
        }

        private async void ExecuteLogin(object obj)
        {
            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;

                string token = await _apiClient.Auth.LoginPersonalAsync(Usuario, Password);

                if (!string.IsNullOrEmpty(token))
                {
                    _apiClient.SetAuthToken(token);
                    
                    // Invocar el evento de login exitoso
                    LoginSuccess?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    ErrorMessage = "No se pudo obtener el token de autenticación.";
                }
            }
            catch (HttpRequestException)
            {
                ErrorMessage = "Error de conexión con el servidor. Verifique que la API esté ejecutándose.";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}

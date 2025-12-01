using Presentacion.Views;
using Presentacion.ViewModels;
using System;
using System.Windows;

namespace Presentacion
{
    public partial class App : Application
    {
        private ApiClient? _apiClient;
        private LoginView? _currentLoginView;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _apiClient = new ApiClient();
            ShowLogin();
        }

        private void ShowLogin()
        {
            var loginViewModel = new LoginViewModel(_apiClient!);
            loginViewModel.LoginSuccess += OnLoginSuccess;
            
            _currentLoginView = new LoginView(loginViewModel);
            _currentLoginView.Show();
        }

        private void OnLoginSuccess(object? sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {
                    // 1. Crear la ventana principal ANTES de cerrar login
                    var principalViewModel = new PrincipalViewModel(_apiClient!);
                    var principalView = new PrincipalWindowView(principalViewModel);
                    
                    // 2. Mostrar la ventana principal
                    principalView.Show();
                    
                    // 3. Establecer como ventana principal
                    Application.Current.MainWindow = principalView;
                    
                    // 4. Cambiar el modo de cierre
                    Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                    
                    // 5. AHORA cerrar el login
                    if (_currentLoginView != null)
                    {
                        _currentLoginView.Close();
                        _currentLoginView = null;
                    }
                    
                    MessageBox.Show("¡Bienvenido a SICAP!", "Login Exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }));
        }
    }
}

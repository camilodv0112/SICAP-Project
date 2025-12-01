using Presentacion.ViewModels;
using System.Windows;


namespace Presentacion.Views
{
    public partial class LoginView : Window
    {
        public LoginView(LoginViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}

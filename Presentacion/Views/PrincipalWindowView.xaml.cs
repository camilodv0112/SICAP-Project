using System.Windows;
using Presentacion.ViewModels;

namespace Presentacion.Views
{
    public partial class PrincipalWindowView : Window
    {
        public PrincipalWindowView(PrincipalViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}

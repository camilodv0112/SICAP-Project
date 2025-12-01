using Presentacion.Core;

namespace Presentacion.ViewModels
{
    public class CartasViewModel : ViewModelBase
    {
        private readonly ApiClient _apiClient;

        public CartasViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
    }
}

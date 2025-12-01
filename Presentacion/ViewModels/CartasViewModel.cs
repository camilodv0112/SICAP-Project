using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Modelos.ModelsDTO.CartaGenerada;
using Presentacion.Core;

namespace Presentacion.ViewModels
{
    public class CartasViewModel : ViewModelBase
    {
        private readonly ApiClient _apiClient;

        public ObservableCollection<CartaGeneradaResponseDTO> Historial { get; set; } = new();

        public ICommand LoadHistorialCommand { get; }
        public ICommand SaveCartaCommand { get; }

        public CartasViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            LoadHistorialCommand = new RelayCommand(async _ => await LoadHistorialAsync());
            SaveCartaCommand = new RelayCommand<CartaGeneradaCreateDTO>(async (dto) => await SaveCartaAsync(dto));
            _ = LoadHistorialAsync();
        }

        public async Task LoadHistorialAsync()
        {
            try
            {
                var cartas = await _apiClient.CartaGenerada.GetAllAsync();
                Historial.Clear();
                foreach (var c in cartas.OrderByDescending(x => x.FechaGeneracion))
                {
                    Historial.Add(c);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar historial: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task SaveCartaAsync(CartaGeneradaCreateDTO dto)
        {
            try
            {
                await _apiClient.CartaGenerada.CreateAsync(dto);
                await LoadHistorialAsync(); // Recargar historial
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar en historial: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

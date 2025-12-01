using Modelos.ModelsDTO.Evento;
using Presentacion.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Presentacion.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly ApiClient _apiClient;

        private ObservableCollection<EventoResponseDTO> _eventos;
        public ObservableCollection<EventoResponseDTO> Eventos
        {
            get => _eventos;
            set => SetProperty(ref _eventos, value);
        }

        private HashSet<DateTime> _fechasConEventos;
        public HashSet<DateTime> FechasConEventos
        {
            get => _fechasConEventos;
            set => SetProperty(ref _fechasConEventos, value);
        }

        public HomeViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            Eventos = new ObservableCollection<EventoResponseDTO>();
            FechasConEventos = new HashSet<DateTime>();
            
            // Load events when ViewModel is created
            _ = LoadEventosAsync();
        }

        public async Task LoadEventosAsync()
        {
            try
            {
                var eventos = await _apiClient.Evento.GetAllAsync();
                
                // Filter for upcoming events (today onwards)
                var eventosFuturos = eventos.Where(e => e.Fecha.Date >= DateTime.Now.Date)
                                            .OrderBy(e => e.Fecha)
                                            .ThenBy(e => e.HoraInicio)
                                            .ToList();

                Eventos = new ObservableCollection<EventoResponseDTO>(eventosFuturos);

                // Populate HashSet for calendar
                FechasConEventos = new HashSet<DateTime>(eventos.Select(e => e.Fecha.Date));
            }
            catch (Exception ex)
            {
                // Log or handle error silently for the dashboard
                Console.WriteLine($"Error loading events: {ex.Message}");
            }
        }
    }
}

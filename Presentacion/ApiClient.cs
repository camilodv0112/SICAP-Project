using Presentacion.Repositories;
using Presentacion.Repositories.IRepositories;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Presentacion
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        // Autenticación
        public IAuthRepository Auth { get; }
        public IUsuarioPersonalRepository UsuarioPersonal { get; }
        public IUsuarioEstudianteRepository UsuarioEstudiante { get; }

        // Catálogos
        public IDisciplinaArtisticaRepository DisciplinaArtistica { get; }
        public ICargoRepository Cargo { get; }
        public IDepartamentoRepository Departamento { get; }
        public IMunicipioRepository Municipio { get; }
        public ISedeRepository Sede { get; }
        public ISalonRepository Salon { get; }
        public ICarreraRepository Carrera { get; }
        public IEstadoUsuarioRepository EstadoUsuario { get; }
        public ICategoriaNecesidadRepository CategoriaNecesidad { get; }
        public ISubcategoriaNecesidadRepository SubcategoriaNecesidad { get; }

        // Personas
        public IPersonalRepository Personal { get; }
        public IProfesorRepository Profesor { get; }
        public IEstudianteRepository Estudiante { get; }

        // Eventos y Documentos
        public IEventoRepository Evento { get; }
        public INecesidadEventoRepository NecesidadEvento { get; }
        public IParticipanteEventoRepository ParticipanteEvento { get; }
        public IResponsableEventoRepository ResponsableEvento { get; }
        public IPlantillaCartaRepository PlantillaCarta { get; }
        public ICartaGeneradaRepository CartaGenerada { get; }

        public ApiClient()
        {
            // TODO: Configurar URL base desde App.config o usar valor por defecto
            string apiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"] ?? "https://localhost:7174/api/";
            _httpClient = new HttpClient { BaseAddress = new Uri(apiBaseUrl) };

            // Inicializar Repositorios
            Auth = new AuthRepository(_httpClient, "Auth");
            UsuarioPersonal = new UsuarioPersonalRepository(_httpClient, "UsuarioPersonal");
            UsuarioEstudiante = new UsuarioEstudianteRepository(_httpClient, "UsuarioEstudiante");

            DisciplinaArtistica = new DisciplinaArtisticaRepository(_httpClient, "DisciplinaArtistica");
            Cargo = new CargoRepository(_httpClient, "Cargo");
            Departamento = new DepartamentoRepository(_httpClient, "Departamento");
            Municipio = new MunicipioRepository(_httpClient, "Municipio");
            Sede = new SedeRepository(_httpClient, "Sede");
            Salon = new SalonRepository(_httpClient, "Salon");
            Carrera = new CarreraRepository(_httpClient, "Carrera");
            EstadoUsuario = new EstadoUsuarioRepository(_httpClient, "EstadoUsuario");
            CategoriaNecesidad = new CategoriaNecesidadRepository(_httpClient, "CategoriaNecesidad");
            SubcategoriaNecesidad = new SubcategoriaNecesidadRepository(_httpClient, "SubcategoriaNecesidad");

            Personal = new PersonalRepository(_httpClient, "Personal");
            Profesor = new ProfesorRepository(_httpClient, "Profesor");
            Estudiante = new EstudianteRepository(_httpClient, "Estudiante");

            Evento = new EventoRepository(_httpClient, "Evento");
            NecesidadEvento = new NecesidadEventoRepository(_httpClient, "NecesidadEvento");
            ParticipanteEvento = new ParticipanteEventoRepository(_httpClient, "ParticipanteEvento");
            ResponsableEvento = new ResponsableEventoRepository(_httpClient, "ResponsableEvento");
            PlantillaCarta = new PlantillaCartaRepository(_httpClient, "PlantillaCarta");
            CartaGenerada = new CartaGeneradaRepository(_httpClient, "CartaGenerada");
        }

        public void SetAuthToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
    }
}

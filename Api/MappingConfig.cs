using AutoMapper;
using Modelos.Models;
using Modelos.ModelsDTO.Cargo;
using Modelos.ModelsDTO.Carrera;
using Modelos.ModelsDTO.CartaGenerada;
using Modelos.ModelsDTO.CategoriaNecesidad;
using Modelos.ModelsDTO.Departamento;
using Modelos.ModelsDTO.DisciplinaArtistica;
using Modelos.ModelsDTO.EstadoUsuario;
using Modelos.ModelsDTO.Estudiante;
using Modelos.ModelsDTO.Evento;
using Modelos.ModelsDTO.Municipio;
using Modelos.ModelsDTO.NecesidadEvento;
using Modelos.ModelsDTO.Personal;
using Modelos.ModelsDTO.PlantillaCarta;
using Modelos.ModelsDTO.Profesor;
using Modelos.ModelsDTO.Salon;
using Modelos.ModelsDTO.Sede;
using Modelos.ModelsDTO.SubcategoriaNecesidad;
using Modelos.ModelsDTO.UsuarioEstudiante;
using Modelos.ModelsDTO.UsuarioPersonal;
using Modelos.ModelsDTO.ParticipanteEvento;
using Modelos.ModelsDTO.ResponsableEvento;

namespace Api;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        // =============================================
        // CATÁLOGOS BÁSICOS
        // =============================================

        // DisciplinaArtistica
        CreateMap<DisciplinaArtistica, DisciplinaArtisticaCreateDTO>().ReverseMap();
        CreateMap<DisciplinaArtistica, DisciplinaArtisticaUpdateDTO>().ReverseMap();
        CreateMap<DisciplinaArtistica, DisciplinaArtisticaResponseDTO>().ReverseMap();

        // Cargo
        CreateMap<Cargo, CargoCreateDTO>().ReverseMap();
        CreateMap<Cargo, CargoUpdateDTO>().ReverseMap();
        CreateMap<Cargo, CargoResponseDTO>().ReverseMap();

        // Departamento
        CreateMap<Departamento, DepartamentoCreateDTO>().ReverseMap();
        CreateMap<Departamento, DepartamentoUpdateDTO>().ReverseMap();
        CreateMap<Departamento, DepartamentoResponseDTO>().ReverseMap();

        // EstadoUsuario
        CreateMap<EstadoUsuario, EstadoUsuarioCreateDTO>().ReverseMap();
        CreateMap<EstadoUsuario, EstadoUsuarioUpdateDTO>().ReverseMap();
        CreateMap<EstadoUsuario, EstadoUsuarioResponseDTO>().ReverseMap();

        // Carrera
        CreateMap<Carrera, CarreraCreateDTO>().ReverseMap();
        CreateMap<Carrera, CarreraUpdateDTO>().ReverseMap();
        CreateMap<Carrera, CarreraResponseDTO>().ReverseMap();

        // CategoriaNecesidad
        CreateMap<CategoriaNecesidad, CategoriaNecesidadCreateDTO>().ReverseMap();
        CreateMap<CategoriaNecesidad, CategoriaNecesidadUpdateDTO>().ReverseMap();
        CreateMap<CategoriaNecesidad, CategoriaNecesidadResponseDTO>().ReverseMap();

        // =============================================
        // UBICACIÓN GEOGRÁFICA Y SEDES
        // =============================================

        // Municipio
        CreateMap<Municipio, MunicipioCreateDTO>().ReverseMap();
        CreateMap<Municipio, MunicipioUpdateDTO>().ReverseMap();
        CreateMap<Municipio, MunicipioResponseDTO>()
            .ForMember(dest => dest.DepartamentoNombre, 
                opt => opt.MapFrom(src => src.Departamento.Nombre));

        // Sede
        CreateMap<Sede, SedeCreateDTO>().ReverseMap();
        CreateMap<Sede, SedeUpdateDTO>().ReverseMap();
        CreateMap<Sede, SedeResponseDTO>()
            .ForMember(dest => dest.MunicipioNombre, 
                opt => opt.MapFrom(src => src.Municipio.Nombre));

        // Salon
        CreateMap<Salon, SalonCreateDTO>().ReverseMap();
        CreateMap<Salon, SalonUpdateDTO>().ReverseMap();
        CreateMap<Salon, SalonResponseDTO>()
            .ForMember(dest => dest.SedeNombre, 
                opt => opt.MapFrom(src => src.Sede.Nombre));

        // =============================================
        // PERSONAS
        // =============================================

        // Profesor
        CreateMap<Profesor, ProfesorCreateDTO>().ReverseMap();
        CreateMap<Profesor, ProfesorUpdateDTO>().ReverseMap();
        CreateMap<Profesor, ProfesorResponseDTO>()
            .ForMember(dest => dest.NombreCompleto, 
                opt => opt.MapFrom(src => $"{src.GradoAcademico} {src.PrimerNombre} {src.SegundoNombre} {src.PrimerApellido} {src.SegundoApellido}"));

        // Estudiante
        CreateMap<Estudiante, EstudianteCreateDTO>().ReverseMap();
        CreateMap<Estudiante, EstudianteUpdateDTO>().ReverseMap();
        CreateMap<Estudiante, EstudianteResponseDTO>()
            .ForMember(dest => dest.NombreCompleto, 
                opt => opt.MapFrom(src => $"{src.PrimerNombre} {src.SegundoNombre} {src.PrimerApellido} {src.SegundoApellido}"))
            .ForMember(dest => dest.MunicipioNombre, 
                opt => opt.MapFrom(src => src.MunicipioNavigation.Nombre))
            .ForMember(dest => dest.CarreraNombre, 
                opt => opt.MapFrom(src => src.CarreraNavigation.Nombre))
            .ForMember(dest => dest.DisciplinaNombre, 
                opt => opt.MapFrom(src => src.DisciplinaNavigation.Nombre));

        // Personal
        CreateMap<Personal, PersonalCreateDTO>().ReverseMap();
        CreateMap<Personal, PersonalUpdateDTO>().ReverseMap();
        CreateMap<Personal, PersonalResponseDTO>()
            .ForMember(dest => dest.NombreCompleto, 
                opt => opt.MapFrom(src => $"{src.PrimerNombre} {src.SegundoNombre} {src.PrimerApellido} {src.SegundoApellido}"))
            .ForMember(dest => dest.CargoNombre, 
                opt => opt.MapFrom(src => src.CargoNavigation.Nombre))
            .ForMember(dest => dest.DisciplinaNombre, 
                opt => opt.MapFrom(src => src.DisciplinaNavigation.Nombre))
            .ForMember(dest => dest.MunicipioNombre, 
                opt => opt.MapFrom(src => src.MunicipioNavigation.Nombre));

        // =============================================
        // EVENTOS Y NECESIDADES
        // =============================================

        // Evento
        CreateMap<Evento, EventoCreateDTO>().ReverseMap();
        CreateMap<Evento, EventoUpdateDTO>().ReverseMap();
        CreateMap<Evento, EventoResponseDTO>()
            .ForMember(dest => dest.SalonNombre, 
                opt => opt.MapFrom(src => src.SalonNavigation.Nombre))
            .ForMember(dest => dest.SedeNombre, 
                opt => opt.MapFrom(src => src.SalonNavigation.Sede.Nombre))
            .ForMember(dest => dest.CantidadParticipantes, 
                opt => opt.MapFrom(src => src.Participantes != null ? src.Participantes.Count : 0))
            .ForMember(dest => dest.CantidadResponsables, 
                opt => opt.MapFrom(src => src.Responsables != null ? src.Responsables.Count : 0));

        // SubcategoriaNecesidad
        CreateMap<SubcategoriaNecesidad, SubcategoriaNecesidadCreateDTO>().ReverseMap();
        CreateMap<SubcategoriaNecesidad, SubcategoriaNecesidadUpdateDTO>().ReverseMap();
        CreateMap<SubcategoriaNecesidad, SubcategoriaNecesidadResponseDTO>()
            .ForMember(dest => dest.CategoriaNombre, 
                opt => opt.MapFrom(src => src.Categoria.Nombre));

        // NecesidadEvento
        CreateMap<NecesidadEvento, NecesidadEventoCreateDTO>().ReverseMap();
        CreateMap<NecesidadEvento, NecesidadEventoUpdateDTO>().ReverseMap();
        CreateMap<NecesidadEvento, NecesidadEventoResponseDTO>()
            .ForMember(dest => dest.EventoNombre, 
                opt => opt.MapFrom(src => src.Evento.Nombre))
            .ForMember(dest => dest.SubcategoriaNombre, 
                opt => opt.MapFrom(src => src.Subcategoria.Nombre))
            .ForMember(dest => dest.CategoriaNombre, 
                opt => opt.MapFrom(src => src.Subcategoria.Categoria.Nombre));

        // =============================================
        // USUARIOS
        // =============================================

        // UsuarioPersonal
        CreateMap<UsuarioPersonalCreateDTO, UsuarioPersonal>()
            .ForMember(dest => dest.Contraseña, opt => opt.Ignore()); // La contraseña se hashea en el servicio

        CreateMap<UsuarioPersonalUpdateDTO, UsuarioPersonal>()
            .ForMember(dest => dest.Contraseña, opt => opt.Ignore()); // La contraseña se hashea en el servicio

        CreateMap<UsuarioPersonal, UsuarioPersonalResponseDTO>()
            .ForMember(dest => dest.PersonalNombre, 
                opt => opt.MapFrom(src => $"{src.PersonalNavigation.PrimerNombre} {src.PersonalNavigation.PrimerApellido}"))
            .ForMember(dest => dest.EstadoNombre, 
                opt => opt.MapFrom(src => src.EstadoNavigation.Nombre));

        // UsuarioEstudiante
        CreateMap<UsuarioEstudianteCreateDTO, UsuarioEstudiante>()
            .ForMember(dest => dest.Contraseña, opt => opt.Ignore()); // La contraseña se hashea en el servicio

        CreateMap<UsuarioEstudianteUpdateDTO, UsuarioEstudiante>()
            .ForMember(dest => dest.Contraseña, opt => opt.Ignore()); // La contraseña se hashea en el servicio

        CreateMap<UsuarioEstudiante, UsuarioEstudianteResponseDTO>()
            .ForMember(dest => dest.EstudianteNombre, 
                opt => opt.MapFrom(src => $"{src.EstudianteNavigation.PrimerNombre} {src.EstudianteNavigation.PrimerApellido}"))
            .ForMember(dest => dest.EstadoNombre, 
                opt => opt.MapFrom(src => src.EstadoNavigation.Nombre));

        // =============================================
        // PLANTILLAS Y CARTAS
        // =============================================

        // PlantillaCarta
        CreateMap<PlantillaCarta, PlantillaCartaCreateDTO>().ReverseMap();
        CreateMap<PlantillaCarta, PlantillaCartaUpdateDTO>().ReverseMap();
        CreateMap<PlantillaCarta, PlantillaCartaResponseDTO>().ReverseMap();

        // CartaGenerada
        CreateMap<CartaGenerada, CartaGeneradaCreateDTO>().ReverseMap();
        CreateMap<CartaGenerada, CartaGeneradaUpdateDTO>().ReverseMap();
        CreateMap<CartaGenerada, CartaGeneradaResponseDTO>()
            .ForMember(dest => dest.PlantillaNombre, 
                opt => opt.MapFrom(src => src.Plantilla.Nombre))
            .ForMember(dest => dest.UsuarioPersonalNombre, 
                opt => opt.MapFrom(src => src.UsuarioPersonal.Usuario));
            // =============================================
            // PARTICIPANTES Y RESPONSABLES
            // =============================================

            // ParticipanteEvento
            CreateMap<ParticipanteEvento, ParticipanteEventoCreateDTO>().ReverseMap();
            CreateMap<ParticipanteEvento, ParticipanteEventoResponseDTO>()
                .ForMember(dest => dest.EstudianteNombre, 
                    opt => opt.MapFrom(src => $"{src.Estudiante.PrimerNombre} {src.Estudiante.PrimerApellido}"));

            // ResponsableEvento
            CreateMap<ResponsableEvento, ResponsableEventoCreateDTO>().ReverseMap();
            CreateMap<ResponsableEvento, ResponsableEventoResponseDTO>()
                .ForMember(dest => dest.PersonalNombre, 
                    opt => opt.MapFrom(src => $"{src.Personal.PrimerNombre} {src.Personal.PrimerApellido}"));
    }
}

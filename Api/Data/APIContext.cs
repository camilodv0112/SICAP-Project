using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Api.Data;

public class APIContext : DbContext
{
    public APIContext(DbContextOptions<APIContext> options) : base(options)
    {
    }

    // =============================================
    // CATÁLOGOS BÁSICOS
    // =============================================
    public DbSet<DisciplinaArtistica> DisciplinasArtisticas { get; set; } = null!;
    public DbSet<Cargo> Cargos { get; set; } = null!;
    public DbSet<Departamento> Departamentos { get; set; } = null!;
    public DbSet<EstadoUsuario> EstadosUsuarios { get; set; } = null!;
    public DbSet<Carrera> Carreras { get; set; } = null!;
    public DbSet<CategoriaNecesidad> CategoriasNecesidades { get; set; } = null!;

    // =============================================
    // UBICACIÓN GEOGRÁFICA Y SEDES
    // =============================================
    public DbSet<Municipio> Municipios { get; set; } = null!;
    public DbSet<Sede> Sedes { get; set; } = null!;
    public DbSet<Salon> Salones { get; set; } = null!;

    // =============================================
    // PERSONAS
    // =============================================
    public DbSet<Profesor> Profesores { get; set; } = null!;
    public DbSet<Estudiante> Estudiantes { get; set; } = null!;
    public DbSet<Personal> Personal { get; set; } = null!;

    // =============================================
    // EVENTOS Y NECESIDADES
    // =============================================
    public DbSet<Evento> Eventos { get; set; } = null!;
    public DbSet<SubcategoriaNecesidad> SubcategoriasNecesidades { get; set; } = null!;
    public DbSet<NecesidadEvento> NecesidadesEventos { get; set; } = null!;
    public DbSet<ParticipanteEvento> ParticipantesEventos { get; set; } = null!;
    public DbSet<ResponsableEvento> ResponsablesEventos { get; set; } = null!;

    // =============================================
    // USUARIOS
    // =============================================
    public DbSet<UsuarioPersonal> UsuariosPersonal { get; set; } = null!;
    public DbSet<UsuarioEstudiante> UsuariosEstudiantes { get; set; } = null!;

    // =============================================
    // PLANTILLAS Y CARTAS
    // =============================================
    public DbSet<PlantillaCarta> PlantillasCartas { get; set; } = null!;
    public DbSet<CartaGenerada> CartasGeneradas { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =============================================
        // CONFIGURACIÓN DE RELACIONES MANY-TO-MANY
        // =============================================

        // Relación Many-to-Many: Profesores <-> Estudiantes
        modelBuilder.Entity<Profesor>()
            .HasMany(p => p.Estudiantes)
            .WithMany(e => e.Profesores)
            .UsingEntity<Dictionary<string, object>>(
                "ProfesoresEstudiantes",
                right => right.HasOne<Estudiante>().WithMany().HasForeignKey("EstudianteID").OnDelete(DeleteBehavior.Restrict),
                left => left.HasOne<Profesor>().WithMany().HasForeignKey("ProfesorID").OnDelete(DeleteBehavior.Restrict),
                j => j.ToTable("ProfesoresEstudiantes"));

        // Relación Many-to-Many: Eventos <-> Estudiantes (Participantes)
        modelBuilder.Entity<Evento>()
            .HasMany(e => e.Participantes)
            .WithMany(est => est.Eventos)
            .UsingEntity<ParticipanteEvento>(
                right => right.HasOne(pe => pe.Estudiante).WithMany().HasForeignKey(pe => pe.EstudianteID).OnDelete(DeleteBehavior.Restrict),
                left => left.HasOne(pe => pe.Evento).WithMany().HasForeignKey(pe => pe.EventoID).OnDelete(DeleteBehavior.Restrict),
                j => 
                {
                    j.ToTable("ParticipantesEventos");
                    j.HasKey(t => new { t.EventoID, t.EstudianteID });
                });

        // Relación Many-to-Many: Eventos <-> Personal (Responsables)
        modelBuilder.Entity<Evento>()
            .HasMany(e => e.Responsables)
            .WithMany(p => p.Eventos)
            .UsingEntity<ResponsableEvento>(
                right => right.HasOne(re => re.Personal).WithMany().HasForeignKey(re => re.PersonalID).OnDelete(DeleteBehavior.Restrict),
                left => left.HasOne(re => re.Evento).WithMany().HasForeignKey(re => re.EventoID).OnDelete(DeleteBehavior.Restrict),
                j => 
                {
                    j.ToTable("ResponsablesEventos");
                    j.HasKey(t => new { t.EventoID, t.PersonalID });
                });

        // Configuración adicional para evitar ciclos en Usuarios
        modelBuilder.Entity<UsuarioPersonal>()
            .HasOne(u => u.PersonalNavigation)
            .WithMany()
            .HasForeignKey(u => u.Personal)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UsuarioPersonal>()
            .HasOne(u => u.EstadoNavigation)
            .WithMany()
            .HasForeignKey(u => u.Estado)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UsuarioEstudiante>()
            .HasOne(u => u.EstudianteNavigation)
            .WithMany()
            .HasForeignKey(u => u.Estudiante)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UsuarioEstudiante>()
            .HasOne(u => u.EstadoNavigation)
            .WithMany()
            .HasForeignKey(u => u.Estado)
            .OnDelete(DeleteBehavior.Restrict);

        // =============================================
        // CONFIGURACIÓN DE ÍNDICES ÚNICOS
        // =============================================

        // DisciplinasArtisticas - Nombre único
        modelBuilder.Entity<DisciplinaArtistica>()
            .HasIndex(d => d.Nombre)
            .IsUnique();

        // Cargos - Nombre único
        modelBuilder.Entity<Cargo>()
            .HasIndex(c => c.Nombre)
            .IsUnique();

        // EstadosUsuarios - Nombre único
        modelBuilder.Entity<EstadoUsuario>()
            .HasIndex(e => e.Nombre)
            .IsUnique();

        // Carreras - Nombre único
        modelBuilder.Entity<Carrera>()
            .HasIndex(c => c.Nombre)
            .IsUnique();

        // CategoriasNecesidades - Nombre único
        modelBuilder.Entity<CategoriaNecesidad>()
            .HasIndex(c => c.Nombre)
            .IsUnique();

        // Estudiantes - Cedula, Carnet y Celular únicos
        modelBuilder.Entity<Estudiante>()
            .HasIndex(e => e.Cedula)
            .IsUnique();

        modelBuilder.Entity<Estudiante>()
            .HasIndex(e => e.Carnet)
            .IsUnique();

        modelBuilder.Entity<Estudiante>()
            .HasIndex(e => e.Celular)
            .IsUnique();

        // Personal - Cedula, NumeroEmpleado y Celular únicos
        modelBuilder.Entity<Personal>()
            .HasIndex(p => p.Cedula)
            .IsUnique();

        modelBuilder.Entity<Personal>()
            .HasIndex(p => p.NumeroEmpleado)
            .IsUnique();

        modelBuilder.Entity<Personal>()
            .HasIndex(p => p.Celular)
            .IsUnique();

        // UsuariosPersonal - Usuario único
        modelBuilder.Entity<UsuarioPersonal>()
            .HasIndex(u => u.Usuario)
            .IsUnique();

        // UsuariosEstudiantes - Usuario único
        modelBuilder.Entity<UsuarioEstudiante>()
            .HasIndex(u => u.Usuario)
            .IsUnique();

        // PlantillasCartas - Nombre único
        modelBuilder.Entity<PlantillaCarta>()
            .HasIndex(p => p.Nombre)
            .IsUnique();

        // =============================================
        // CONFIGURACIÓN DE RESTRICCIONES CHECK
        // =============================================

        // Salones - Capacidad > 0
        modelBuilder.Entity<Salon>()
            .ToTable(t => t.HasCheckConstraint("CK_Salon_Capacidad", "Capacidad > 0"));

        // Estudiantes - Sexo IN ('M','F')
        modelBuilder.Entity<Estudiante>()
            .ToTable(t => t.HasCheckConstraint("CK_Estudiante_Sexo", "Sexo IN ('M','F')"));

        // Personal - Sexo IN ('M','F')
        modelBuilder.Entity<Personal>()
            .ToTable(t => t.HasCheckConstraint("CK_Personal_Sexo", "Sexo IN ('M','F')"));

        // NecesidadesEvento - Cantidad > 0
        modelBuilder.Entity<NecesidadEvento>()
            .ToTable(t => t.HasCheckConstraint("CK_NecesidadEvento_Cantidad", "Cantidad > 0"));

        // CartasGeneradas - Validación JSON en Datos (SQL Server)
        modelBuilder.Entity<CartaGenerada>()
            .ToTable(t => t.HasCheckConstraint("CK_CartaGenerada_JSON", "ISJSON(Datos) = 1"));
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    CargoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.CargoID);
                });

            migrationBuilder.CreateTable(
                name: "Carreras",
                columns: table => new
                {
                    CarreraID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carreras", x => x.CarreraID);
                });

            migrationBuilder.CreateTable(
                name: "CategoriasNecesidades",
                columns: table => new
                {
                    CategoriaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasNecesidades", x => x.CategoriaID);
                });

            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    DepartamentoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.DepartamentoID);
                });

            migrationBuilder.CreateTable(
                name: "DisciplinasArtisticas",
                columns: table => new
                {
                    DisciplinaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplinasArtisticas", x => x.DisciplinaID);
                });

            migrationBuilder.CreateTable(
                name: "EstadosUsuarios",
                columns: table => new
                {
                    EstadoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosUsuarios", x => x.EstadoID);
                });

            migrationBuilder.CreateTable(
                name: "PlantillasCartas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantillasCartas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profesores",
                columns: table => new
                {
                    ProfesorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradoAcademico = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    PrimerNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SegundoNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrimerApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Asignatura = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesores", x => x.ProfesorID);
                });

            migrationBuilder.CreateTable(
                name: "SubcategoriasNecesidades",
                columns: table => new
                {
                    SubcategoriaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoriaID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubcategoriasNecesidades", x => x.SubcategoriaID);
                    table.ForeignKey(
                        name: "FK_SubcategoriasNecesidades_CategoriasNecesidades_CategoriaID",
                        column: x => x.CategoriaID,
                        principalTable: "CategoriasNecesidades",
                        principalColumn: "CategoriaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Municipios",
                columns: table => new
                {
                    MunicipioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DepartamentoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipios", x => x.MunicipioID);
                    table.ForeignKey(
                        name: "FK_Municipios_Departamentos_DepartamentoID",
                        column: x => x.DepartamentoID,
                        principalTable: "Departamentos",
                        principalColumn: "DepartamentoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    EstudianteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimerNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SegundoNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrimerApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cedula = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Carnet = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Municipio = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Carrera = table.Column<int>(type: "int", nullable: false),
                    Grupo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Año = table.Column<int>(type: "int", nullable: false),
                    Disciplina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.EstudianteID);
                    table.CheckConstraint("CK_Estudiante_Sexo", "Sexo IN ('M','F')");
                    table.ForeignKey(
                        name: "FK_Estudiantes_Carreras_Carrera",
                        column: x => x.Carrera,
                        principalTable: "Carreras",
                        principalColumn: "CarreraID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Estudiantes_DisciplinasArtisticas_Disciplina",
                        column: x => x.Disciplina,
                        principalTable: "DisciplinasArtisticas",
                        principalColumn: "DisciplinaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Estudiantes_Municipios_Municipio",
                        column: x => x.Municipio,
                        principalTable: "Municipios",
                        principalColumn: "MunicipioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personal",
                columns: table => new
                {
                    PersonalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimerNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SegundoNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrimerApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Cargo = table.Column<int>(type: "int", nullable: false),
                    Disciplina = table.Column<int>(type: "int", nullable: false),
                    Cedula = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NumeroEmpleado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Municipio = table.Column<int>(type: "int", nullable: false),
                    DireccionDomiciliaria = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personal", x => x.PersonalID);
                    table.CheckConstraint("CK_Personal_Sexo", "Sexo IN ('M','F')");
                    table.ForeignKey(
                        name: "FK_Personal_Cargos_Cargo",
                        column: x => x.Cargo,
                        principalTable: "Cargos",
                        principalColumn: "CargoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personal_DisciplinasArtisticas_Disciplina",
                        column: x => x.Disciplina,
                        principalTable: "DisciplinasArtisticas",
                        principalColumn: "DisciplinaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personal_Municipios_Municipio",
                        column: x => x.Municipio,
                        principalTable: "Municipios",
                        principalColumn: "MunicipioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sedes",
                columns: table => new
                {
                    SedeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MunicipioID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sedes", x => x.SedeID);
                    table.ForeignKey(
                        name: "FK_Sedes_Municipios_MunicipioID",
                        column: x => x.MunicipioID,
                        principalTable: "Municipios",
                        principalColumn: "MunicipioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfesoresEstudiantes",
                columns: table => new
                {
                    EstudianteID = table.Column<int>(type: "int", nullable: false),
                    ProfesorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfesoresEstudiantes", x => new { x.EstudianteID, x.ProfesorID });
                    table.ForeignKey(
                        name: "FK_ProfesoresEstudiantes_Estudiantes_EstudianteID",
                        column: x => x.EstudianteID,
                        principalTable: "Estudiantes",
                        principalColumn: "EstudianteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfesoresEstudiantes_Profesores_ProfesorID",
                        column: x => x.ProfesorID,
                        principalTable: "Profesores",
                        principalColumn: "ProfesorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosEstudiantes",
                columns: table => new
                {
                    UsuarioEID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Contraseña = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Estudiante = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    EstadoUsuarioEstadoID = table.Column<int>(type: "int", nullable: true),
                    EstudianteID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosEstudiantes", x => x.UsuarioEID);
                    table.ForeignKey(
                        name: "FK_UsuariosEstudiantes_EstadosUsuarios_Estado",
                        column: x => x.Estado,
                        principalTable: "EstadosUsuarios",
                        principalColumn: "EstadoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuariosEstudiantes_EstadosUsuarios_EstadoUsuarioEstadoID",
                        column: x => x.EstadoUsuarioEstadoID,
                        principalTable: "EstadosUsuarios",
                        principalColumn: "EstadoID");
                    table.ForeignKey(
                        name: "FK_UsuariosEstudiantes_Estudiantes_Estudiante",
                        column: x => x.Estudiante,
                        principalTable: "Estudiantes",
                        principalColumn: "EstudianteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuariosEstudiantes_Estudiantes_EstudianteID",
                        column: x => x.EstudianteID,
                        principalTable: "Estudiantes",
                        principalColumn: "EstudianteID");
                });

            migrationBuilder.CreateTable(
                name: "UsuariosPersonal",
                columns: table => new
                {
                    UsuarioPID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Contraseña = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Personal = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    EstadoUsuarioEstadoID = table.Column<int>(type: "int", nullable: true),
                    PersonalID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosPersonal", x => x.UsuarioPID);
                    table.ForeignKey(
                        name: "FK_UsuariosPersonal_EstadosUsuarios_Estado",
                        column: x => x.Estado,
                        principalTable: "EstadosUsuarios",
                        principalColumn: "EstadoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuariosPersonal_EstadosUsuarios_EstadoUsuarioEstadoID",
                        column: x => x.EstadoUsuarioEstadoID,
                        principalTable: "EstadosUsuarios",
                        principalColumn: "EstadoID");
                    table.ForeignKey(
                        name: "FK_UsuariosPersonal_Personal_Personal",
                        column: x => x.Personal,
                        principalTable: "Personal",
                        principalColumn: "PersonalID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuariosPersonal_Personal_PersonalID",
                        column: x => x.PersonalID,
                        principalTable: "Personal",
                        principalColumn: "PersonalID");
                });

            migrationBuilder.CreateTable(
                name: "Salones",
                columns: table => new
                {
                    SalonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Capacidad = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SedeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salones", x => x.SalonID);
                    table.CheckConstraint("CK_Salon_Capacidad", "Capacidad > 0");
                    table.ForeignKey(
                        name: "FK_Salones_Sedes_SedeID",
                        column: x => x.SedeID,
                        principalTable: "Sedes",
                        principalColumn: "SedeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartasGeneradas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantillaId = table.Column<int>(type: "int", nullable: false),
                    NombreCarta = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Datos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RutaPDF = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UsuarioPersonalID = table.Column<int>(type: "int", nullable: false),
                    FechaGeneracion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartasGeneradas", x => x.Id);
                    table.CheckConstraint("CK_CartaGenerada_JSON", "ISJSON(Datos) = 1");
                    table.ForeignKey(
                        name: "FK_CartasGeneradas_PlantillasCartas_PlantillaId",
                        column: x => x.PlantillaId,
                        principalTable: "PlantillasCartas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartasGeneradas_UsuariosPersonal_UsuarioPersonalID",
                        column: x => x.UsuarioPersonalID,
                        principalTable: "UsuariosPersonal",
                        principalColumn: "UsuarioPID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    EventoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoraFinal = table.Column<TimeSpan>(type: "time", nullable: false),
                    Salon = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.EventoID);
                    table.ForeignKey(
                        name: "FK_Eventos_Salones_Salon",
                        column: x => x.Salon,
                        principalTable: "Salones",
                        principalColumn: "SalonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NecesidadesEventos",
                columns: table => new
                {
                    NecesidadID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventoID = table.Column<int>(type: "int", nullable: false),
                    SubcategoriaID = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NecesidadesEventos", x => x.NecesidadID);
                    table.CheckConstraint("CK_NecesidadEvento_Cantidad", "Cantidad > 0");
                    table.ForeignKey(
                        name: "FK_NecesidadesEventos_Eventos_EventoID",
                        column: x => x.EventoID,
                        principalTable: "Eventos",
                        principalColumn: "EventoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NecesidadesEventos_SubcategoriasNecesidades_SubcategoriaID",
                        column: x => x.SubcategoriaID,
                        principalTable: "SubcategoriasNecesidades",
                        principalColumn: "SubcategoriaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantesEventos",
                columns: table => new
                {
                    EstudianteID = table.Column<int>(type: "int", nullable: false),
                    EventoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantesEventos", x => new { x.EstudianteID, x.EventoID });
                    table.ForeignKey(
                        name: "FK_ParticipantesEventos_Estudiantes_EstudianteID",
                        column: x => x.EstudianteID,
                        principalTable: "Estudiantes",
                        principalColumn: "EstudianteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParticipantesEventos_Eventos_EventoID",
                        column: x => x.EventoID,
                        principalTable: "Eventos",
                        principalColumn: "EventoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResponsablesEventos",
                columns: table => new
                {
                    EventoID = table.Column<int>(type: "int", nullable: false),
                    PersonalID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponsablesEventos", x => new { x.EventoID, x.PersonalID });
                    table.ForeignKey(
                        name: "FK_ResponsablesEventos_Eventos_EventoID",
                        column: x => x.EventoID,
                        principalTable: "Eventos",
                        principalColumn: "EventoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResponsablesEventos_Personal_PersonalID",
                        column: x => x.PersonalID,
                        principalTable: "Personal",
                        principalColumn: "PersonalID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargos_Nombre",
                table: "Cargos",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carreras_Nombre",
                table: "Carreras",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartasGeneradas_PlantillaId",
                table: "CartasGeneradas",
                column: "PlantillaId");

            migrationBuilder.CreateIndex(
                name: "IX_CartasGeneradas_UsuarioPersonalID",
                table: "CartasGeneradas",
                column: "UsuarioPersonalID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasNecesidades_Nombre",
                table: "CategoriasNecesidades",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinasArtisticas_Nombre",
                table: "DisciplinasArtisticas",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EstadosUsuarios_Nombre",
                table: "EstadosUsuarios",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_Carnet",
                table: "Estudiantes",
                column: "Carnet",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_Carrera",
                table: "Estudiantes",
                column: "Carrera");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_Cedula",
                table: "Estudiantes",
                column: "Cedula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_Celular",
                table: "Estudiantes",
                column: "Celular",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_Disciplina",
                table: "Estudiantes",
                column: "Disciplina");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_Municipio",
                table: "Estudiantes",
                column: "Municipio");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_Salon",
                table: "Eventos",
                column: "Salon");

            migrationBuilder.CreateIndex(
                name: "IX_Municipios_DepartamentoID",
                table: "Municipios",
                column: "DepartamentoID");

            migrationBuilder.CreateIndex(
                name: "IX_NecesidadesEventos_EventoID",
                table: "NecesidadesEventos",
                column: "EventoID");

            migrationBuilder.CreateIndex(
                name: "IX_NecesidadesEventos_SubcategoriaID",
                table: "NecesidadesEventos",
                column: "SubcategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantesEventos_EventoID",
                table: "ParticipantesEventos",
                column: "EventoID");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_Cargo",
                table: "Personal",
                column: "Cargo");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_Cedula",
                table: "Personal",
                column: "Cedula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personal_Celular",
                table: "Personal",
                column: "Celular",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personal_Disciplina",
                table: "Personal",
                column: "Disciplina");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_Municipio",
                table: "Personal",
                column: "Municipio");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_NumeroEmpleado",
                table: "Personal",
                column: "NumeroEmpleado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlantillasCartas_Nombre",
                table: "PlantillasCartas",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfesoresEstudiantes_ProfesorID",
                table: "ProfesoresEstudiantes",
                column: "ProfesorID");

            migrationBuilder.CreateIndex(
                name: "IX_ResponsablesEventos_PersonalID",
                table: "ResponsablesEventos",
                column: "PersonalID");

            migrationBuilder.CreateIndex(
                name: "IX_Salones_SedeID",
                table: "Salones",
                column: "SedeID");

            migrationBuilder.CreateIndex(
                name: "IX_Sedes_MunicipioID",
                table: "Sedes",
                column: "MunicipioID");

            migrationBuilder.CreateIndex(
                name: "IX_SubcategoriasNecesidades_CategoriaID",
                table: "SubcategoriasNecesidades",
                column: "CategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosEstudiantes_Estado",
                table: "UsuariosEstudiantes",
                column: "Estado");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosEstudiantes_EstadoUsuarioEstadoID",
                table: "UsuariosEstudiantes",
                column: "EstadoUsuarioEstadoID");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosEstudiantes_Estudiante",
                table: "UsuariosEstudiantes",
                column: "Estudiante");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosEstudiantes_EstudianteID",
                table: "UsuariosEstudiantes",
                column: "EstudianteID");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosEstudiantes_Usuario",
                table: "UsuariosEstudiantes",
                column: "Usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosPersonal_Estado",
                table: "UsuariosPersonal",
                column: "Estado");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosPersonal_EstadoUsuarioEstadoID",
                table: "UsuariosPersonal",
                column: "EstadoUsuarioEstadoID");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosPersonal_Personal",
                table: "UsuariosPersonal",
                column: "Personal");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosPersonal_PersonalID",
                table: "UsuariosPersonal",
                column: "PersonalID");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosPersonal_Usuario",
                table: "UsuariosPersonal",
                column: "Usuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartasGeneradas");

            migrationBuilder.DropTable(
                name: "NecesidadesEventos");

            migrationBuilder.DropTable(
                name: "ParticipantesEventos");

            migrationBuilder.DropTable(
                name: "ProfesoresEstudiantes");

            migrationBuilder.DropTable(
                name: "ResponsablesEventos");

            migrationBuilder.DropTable(
                name: "UsuariosEstudiantes");

            migrationBuilder.DropTable(
                name: "PlantillasCartas");

            migrationBuilder.DropTable(
                name: "UsuariosPersonal");

            migrationBuilder.DropTable(
                name: "SubcategoriasNecesidades");

            migrationBuilder.DropTable(
                name: "Profesores");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "EstadosUsuarios");

            migrationBuilder.DropTable(
                name: "Personal");

            migrationBuilder.DropTable(
                name: "CategoriasNecesidades");

            migrationBuilder.DropTable(
                name: "Salones");

            migrationBuilder.DropTable(
                name: "Carreras");

            migrationBuilder.DropTable(
                name: "Cargos");

            migrationBuilder.DropTable(
                name: "DisciplinasArtisticas");

            migrationBuilder.DropTable(
                name: "Sedes");

            migrationBuilder.DropTable(
                name: "Municipios");

            migrationBuilder.DropTable(
                name: "Departamentos");
        }
    }
}

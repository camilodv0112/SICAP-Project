USE [db27572]
GO

-- =============================================
-- 1. CATÁLOGOS BÁSICOS
-- =============================================

CREATE TABLE DisciplinasArtisticas (
    DisciplinaID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre       NVARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE Cargos (
    CargoID     INT IDENTITY(1,1) PRIMARY KEY,
    Nombre      NVARCHAR(100) NOT NULL UNIQUE,
    Descripcion NVARCHAR(255) NOT NULL
);

CREATE TABLE Departamentos (
    DepartamentoID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre         NVARCHAR(100) NOT NULL
);

CREATE TABLE EstadosUsuarios (
    EstadoID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre   VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Carreras (
    CarreraID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre    NVARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE CategoriasNecesidades (
    CategoriaID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre      NVARCHAR(100) NOT NULL UNIQUE,
    Descripcion NVARCHAR(255) NULL
);

-- =============================================
-- 2. UBICACIÓN GEOGRÁFICA Y SEDES
-- =============================================

CREATE TABLE Municipios (
    MunicipioID    INT IDENTITY(1,1) PRIMARY KEY,
    Nombre         NVARCHAR(100) NOT NULL,
    DepartamentoID INT NOT NULL
);

CREATE TABLE Sedes (
    SedeID      INT IDENTITY(1,1) PRIMARY KEY,
    Nombre      NVARCHAR(150) NOT NULL,
    Descripcion NVARCHAR(255) NULL,
    MunicipioID INT NOT NULL
);

CREATE TABLE Salones (
    SalonID     INT IDENTITY(1,1) PRIMARY KEY,
    Nombre      NVARCHAR(100) NOT NULL,
    Capacidad   INT NOT NULL CHECK (Capacidad > 0),
    Descripcion NVARCHAR(255) NULL,
    SedeID      INT NOT NULL
);

-- =============================================
-- 3. PERSONAS
-- =============================================

CREATE TABLE Profesores (
    ProfesorID      INT IDENTITY(1,1) PRIMARY KEY,
    GradoAcademico  NVARCHAR(5)  NULL DEFAULT 'Ing.',
    PrimerNombre    NVARCHAR(50) NOT NULL,
    SegundoNombre   NVARCHAR(50) NOT NULL,
    PrimerApellido  NVARCHAR(50) NOT NULL,
    SegundoApellido NVARCHAR(50) NOT NULL,
    Asignatura      NVARCHAR(50) NOT NULL
);

CREATE TABLE Estudiantes (
    EstudianteID      INT IDENTITY(1,1) PRIMARY KEY,
    PrimerNombre      NVARCHAR(50) NOT NULL,
    SegundoNombre     NVARCHAR(50) NOT NULL,
    PrimerApellido    NVARCHAR(50) NOT NULL,
    SegundoApellido   NVARCHAR(50) NOT NULL,
    Cedula            NVARCHAR(30) NOT NULL UNIQUE,
    Sexo              CHAR(1) NULL CHECK (Sexo IN ('M','F')),
    Carnet            NVARCHAR(20) NOT NULL UNIQUE,
    Celular           NVARCHAR(15) NOT NULL UNIQUE,
    CorreoElectronico NVARCHAR(100) NULL,
    Municipio         INT NOT NULL,
    Direccion         NVARCHAR(250) NOT NULL,
    Carrera           INT NOT NULL,
    Grupo             NVARCHAR(10) NOT NULL,
    Año               INT NOT NULL,
    Disciplina        INT NOT NULL
);

CREATE TABLE Personal (
    PersonalID          INT IDENTITY(1,1) PRIMARY KEY,
    PrimerNombre        NVARCHAR(50) NOT NULL,
    SegundoNombre       NVARCHAR(50) NOT NULL,
    PrimerApellido      NVARCHAR(50) NOT NULL,
    SegundoApellido     NVARCHAR(50) NOT NULL,
    Sexo                CHAR(1) NULL CHECK (Sexo IN ('M','F')),
    Cargo               INT NOT NULL,
    Disciplina          INT NOT NULL,
    Cedula              NVARCHAR(30) NOT NULL UNIQUE,
    NumeroEmpleado      NVARCHAR(20) NULL UNIQUE,
    Celular             NVARCHAR(15) NOT NULL UNIQUE,
    CorreoElectronico   NVARCHAR(100) NULL,
    Municipio           INT NOT NULL,
    DireccionDomiciliaria NVARCHAR(250) NOT NULL
);

-- =============================================
-- 4. EVENTOS Y RELACIONES
-- =============================================

CREATE TABLE Eventos (
    EventoID    INT IDENTITY(1,1) PRIMARY KEY,
    Nombre      NVARCHAR(150) NOT NULL,
    Descripcion NVARCHAR(300) NULL,
    Fecha       DATE NOT NULL,
    HoraInicio  TIME(7) NOT NULL,
    HoraFinal   TIME(7) NOT NULL,
    Salon       INT NOT NULL
);

CREATE TABLE SubcategoriasNecesidades (
    SubcategoriaID INT IDENTITY(1,1) PRIMARY KEY,
    CategoriaID    INT NOT NULL,
    Nombre         NVARCHAR(100) NOT NULL,
    Descripcion    NVARCHAR(255) NULL
);

CREATE TABLE NecesidadesEvento (
    NecesidadID    INT IDENTITY(1,1) PRIMARY KEY,
    EventoID       INT NOT NULL,
    SubcategoriaID INT NOT NULL,
    Cantidad       INT NOT NULL CHECK (Cantidad > 0),
    Observaciones  NVARCHAR(255) NULL
);

CREATE TABLE ParticipantesEventos (
    EventoID     INT NOT NULL,
    EstudianteID INT NOT NULL,
    PRIMARY KEY (EventoID, EstudianteID)
);

CREATE TABLE ResponsablesEventos (
    EventoID   INT NOT NULL,
    PersonalID INT NOT NULL,
    PRIMARY KEY (EventoID, PersonalID)
);

CREATE TABLE ProfesoresEstudiantes (
    ProfesorID   INT NOT NULL,
    EstudianteID INT NOT NULL,
    PRIMARY KEY (ProfesorID, EstudianteID)
);

-- =============================================
-- 5. USUARIOS
-- =============================================

CREATE TABLE UsuariosPersonal (
    UsuarioPID INT IDENTITY(1,1) PRIMARY KEY,
    Usuario    NVARCHAR(50) NOT NULL UNIQUE,
    Contraseña VARBINARY(MAX) NOT NULL,
    Personal   INT NOT NULL,
    Estado     INT NOT NULL
);

CREATE TABLE UsuariosEstudiantes (
    UsuarioEID  INT IDENTITY(1,1) PRIMARY KEY,
    Usuario     NVARCHAR(50) NOT NULL UNIQUE,
    Contraseña  VARBINARY(MAX) NOT NULL,
    Estudiante  INT NOT NULL,
    Estado      INT NOT NULL
);

-- =============================================
-- 6. CARTAS Y PLANTILLAS
-- =============================================

CREATE TABLE PlantillasCarta (
    Id        INT IDENTITY(1,1) PRIMARY KEY,
    Nombre    NVARCHAR(150) NOT NULL UNIQUE,
    Contenido NVARCHAR(MAX) NOT NULL
);

CREATE TABLE CartasGeneradas (
    Id                INT IDENTITY(1,1) PRIMARY KEY,
    PlantillaId       INT NOT NULL,
    NombreCarta       NVARCHAR(150) NOT NULL,
    Datos             NVARCHAR(MAX) NULL CHECK (ISJSON(Datos) = 1),
    RutaPDF           NVARCHAR(255) NULL,
    UsuarioPersonalID INT NOT NULL,
    FechaGeneracion   DATETIME NULL DEFAULT SYSDATETIME()
);

-- =============================================
-- FOREIGN KEYS (agrupados y comentados por tabla)
-- =============================================

-- >>> Municipios
ALTER TABLE Municipios ADD CONSTRAINT FK_Municipios_Departamento FOREIGN KEY (DepartamentoID) REFERENCES Departamentos(DepartamentoID) ON DELETE CASCADE;

-- >>> Sedes
ALTER TABLE Sedes ADD CONSTRAINT FK_Sedes_Municipio FOREIGN KEY (MunicipioID) REFERENCES Municipios(MunicipioID) ON DELETE CASCADE;

-- >>> Salones
ALTER TABLE Salones ADD CONSTRAINT FK_Salones_Sede FOREIGN KEY (SedeID) REFERENCES Sedes(SedeID) ON DELETE CASCADE;

-- >>> Estudiantes
ALTER TABLE Estudiantes ADD CONSTRAINT FK_Estudiantes_Municipio FOREIGN KEY (Municipio) REFERENCES Municipios(MunicipioID) ON DELETE CASCADE;
ALTER TABLE Estudiantes ADD CONSTRAINT FK_Estudiantes_Carrera FOREIGN KEY (Carrera) REFERENCES Carreras(CarreraID) ON DELETE CASCADE;
ALTER TABLE Estudiantes ADD CONSTRAINT FK_Estudiantes_Disciplina FOREIGN KEY (Disciplina) REFERENCES DisciplinasArtisticas(DisciplinaID) ON DELETE CASCADE;

-- >>> Personal
ALTER TABLE Personal ADD CONSTRAINT FK_Personal_Municipio FOREIGN KEY (Municipio) REFERENCES Municipios(MunicipioID) ON DELETE CASCADE;
ALTER TABLE Personal ADD CONSTRAINT FK_Personal_Cargo FOREIGN KEY (Cargo) REFERENCES Cargos(CargoID) ON DELETE CASCADE;
ALTER TABLE Personal ADD CONSTRAINT FK_Personal_Disciplina FOREIGN KEY (Disciplina) REFERENCES DisciplinasArtisticas(DisciplinaID) ON DELETE CASCADE;

-- >>> Eventos
ALTER TABLE Eventos ADD CONSTRAINT FK_Eventos_Salon FOREIGN KEY (Salon) REFERENCES Salones(SalonID) ON DELETE CASCADE;

-- >>> NecesidadesEvento
ALTER TABLE NecesidadesEvento ADD CONSTRAINT FK_Necesidades_Evento FOREIGN KEY (EventoID) REFERENCES Eventos(EventoID) ON DELETE CASCADE;
ALTER TABLE NecesidadesEvento ADD CONSTRAINT FK_Necesidades_Subcategoria FOREIGN KEY (SubcategoriaID) REFERENCES SubcategoriasNecesidades(SubcategoriaID) ON DELETE CASCADE;

-- >>> SubcategoriasNecesidades
ALTER TABLE SubcategoriasNecesidades ADD CONSTRAINT FK_Subcategorias_Categoria FOREIGN KEY (CategoriaID) REFERENCES CategoriasNecesidades(CategoriaID) ON DELETE CASCADE;

-- >>> ParticipantesEventos
ALTER TABLE ParticipantesEventos     ADD CONSTRAINT FK_Participantes_Evento           FOREIGN KEY (EventoID)         REFERENCES Eventos(EventoID);
ALTER TABLE ParticipantesEventos     ADD CONSTRAINT FK_Participantes_Estudiante       FOREIGN KEY (EstudianteID)     REFERENCES Estudiantes(EstudianteID);

-- >>> ResponsablesEventos
ALTER TABLE ResponsablesEventos      ADD CONSTRAINT FK_Responsables_Evento            FOREIGN KEY (EventoID)         REFERENCES Eventos(EventoID);
ALTER TABLE ResponsablesEventos      ADD CONSTRAINT FK_Responsables_Personal          FOREIGN KEY (PersonalID)       REFERENCES Personal(PersonalID);

-- >>> ProfesoresEstudiantes
ALTER TABLE ProfesoresEstudiantes    ADD CONSTRAINT FK_ProfEst_Profesor               FOREIGN KEY (ProfesorID)       REFERENCES Profesores(ProfesorID);
ALTER TABLE ProfesoresEstudiantes    ADD CONSTRAINT FK_ProfEst_Estudiante             FOREIGN KEY (EstudianteID)     REFERENCES Estudiantes(EstudianteID);

-- >>> UsuariosPersonal
ALTER TABLE UsuariosPersonal         ADD CONSTRAINT FK_UsrPer_Personal                FOREIGN KEY (Personal)         REFERENCES Personal(PersonalID);
ALTER TABLE UsuariosPersonal         ADD CONSTRAINT FK_UsrPer_Estado                  FOREIGN KEY (Estado)           REFERENCES EstadosUsuarios(EstadoID);

-- >>> UsuariosEstudiantes
CREATE TABLE Salones (
    SalonID     INT IDENTITY(1,1) PRIMARY KEY,
    Nombre      NVARCHAR(100) NOT NULL,
    Capacidad   INT NOT NULL CHECK (Capacidad > 0),
    Descripcion NVARCHAR(255) NULL,
    SedeID      INT NOT NULL
);

-- =============================================
-- 3. PERSONAS
-- =============================================

CREATE TABLE Profesores (
    ProfesorID      INT IDENTITY(1,1) PRIMARY KEY,
    GradoAcademico  NVARCHAR(5)  NULL DEFAULT 'Ing.',
    PrimerNombre    NVARCHAR(50) NOT NULL,
    SegundoNombre   NVARCHAR(50) NOT NULL,
    PrimerApellido  NVARCHAR(50) NOT NULL,
    SegundoApellido NVARCHAR(50) NOT NULL,
    Asignatura      NVARCHAR(50) NOT NULL
);

CREATE TABLE Estudiantes (
    EstudianteID      INT IDENTITY(1,1) PRIMARY KEY,
    PrimerNombre      NVARCHAR(50) NOT NULL,
    SegundoNombre     NVARCHAR(50) NOT NULL,
    PrimerApellido    NVARCHAR(50) NOT NULL,
    SegundoApellido   NVARCHAR(50) NOT NULL,
    Cedula            NVARCHAR(30) NOT NULL UNIQUE,
    Sexo              CHAR(1) NULL CHECK (Sexo IN ('M','F')),
    Carnet            NVARCHAR(20) NOT NULL UNIQUE,
    Celular           NVARCHAR(15) NOT NULL UNIQUE,
    CorreoElectronico NVARCHAR(100) NULL,
    Municipio         INT NOT NULL,
    Direccion         NVARCHAR(250) NOT NULL,
    Carrera           INT NOT NULL,
    Grupo             NVARCHAR(10) NOT NULL,
    Año               INT NOT NULL,
    Disciplina        INT NOT NULL
);

CREATE TABLE Personal (
    PersonalID          INT IDENTITY(1,1) PRIMARY KEY,
    PrimerNombre        NVARCHAR(50) NOT NULL,
    SegundoNombre       NVARCHAR(50) NOT NULL,
    PrimerApellido      NVARCHAR(50) NOT NULL,
    SegundoApellido     NVARCHAR(50) NOT NULL,
    Sexo                CHAR(1) NULL CHECK (Sexo IN ('M','F')),
    Cargo               INT NOT NULL,
    Disciplina          INT NOT NULL,
    Cedula              NVARCHAR(30) NOT NULL UNIQUE,
    NumeroEmpleado      NVARCHAR(20) NULL UNIQUE,
    Celular             NVARCHAR(15) NOT NULL UNIQUE,
    CorreoElectronico   NVARCHAR(100) NULL,
    Municipio           INT NOT NULL,
    DireccionDomiciliaria NVARCHAR(250) NOT NULL
);

-- =============================================
-- 4. EVENTOS Y RELACIONES
-- =============================================

CREATE TABLE Eventos (
    EventoID    INT IDENTITY(1,1) PRIMARY KEY,
    Nombre      NVARCHAR(150) NOT NULL,
    Descripcion NVARCHAR(300) NULL,
    Fecha       DATE NOT NULL,
    HoraInicio  TIME(7) NOT NULL,
    HoraFinal   TIME(7) NOT NULL,
    Salon       INT NOT NULL
);

CREATE TABLE SubcategoriasNecesidades (
    SubcategoriaID INT IDENTITY(1,1) PRIMARY KEY,
    CategoriaID    INT NOT NULL,
    Nombre         NVARCHAR(100) NOT NULL,
    Descripcion    NVARCHAR(255) NULL
);

CREATE TABLE NecesidadesEvento (
    NecesidadID    INT IDENTITY(1,1) PRIMARY KEY,
    EventoID       INT NOT NULL,
    SubcategoriaID INT NOT NULL,
    Cantidad       INT NOT NULL CHECK (Cantidad > 0),
    Observaciones  NVARCHAR(255) NULL
);

CREATE TABLE ParticipantesEventos (
    EventoID     INT NOT NULL,
    EstudianteID INT NOT NULL,
    PRIMARY KEY (EventoID, EstudianteID)
);

CREATE TABLE ResponsablesEventos (
    EventoID   INT NOT NULL,
    PersonalID INT NOT NULL,
    PRIMARY KEY (EventoID, PersonalID)
);

CREATE TABLE ProfesoresEstudiantes (
    ProfesorID   INT NOT NULL,
    EstudianteID INT NOT NULL,
    PRIMARY KEY (ProfesorID, EstudianteID)
);

-- =============================================
-- 5. USUARIOS
-- =============================================

CREATE TABLE UsuariosPersonal (
    UsuarioPID INT IDENTITY(1,1) PRIMARY KEY,
    Usuario    NVARCHAR(50) NOT NULL UNIQUE,
    Contraseña VARBINARY(MAX) NOT NULL,
    Personal   INT NOT NULL,
    Estado     INT NOT NULL
);

CREATE TABLE UsuariosEstudiantes (
    UsuarioEID  INT IDENTITY(1,1) PRIMARY KEY,
    Usuario     NVARCHAR(50) NOT NULL UNIQUE,
    Contraseña  VARBINARY(MAX) NOT NULL,
    Estudiante  INT NOT NULL,
    Estado      INT NOT NULL
);

-- =============================================
-- 6. CARTAS Y PLANTILLAS
-- =============================================

CREATE TABLE PlantillasCarta (
    Id        INT IDENTITY(1,1) PRIMARY KEY,
    Nombre    NVARCHAR(150) NOT NULL UNIQUE,
    Contenido NVARCHAR(MAX) NOT NULL
);

CREATE TABLE CartasGeneradas (
    Id                INT IDENTITY(1,1) PRIMARY KEY,
    PlantillaId       INT NOT NULL,
    NombreCarta       NVARCHAR(150) NOT NULL,
    Datos             NVARCHAR(MAX) NULL CHECK (ISJSON(Datos) = 1),
    RutaPDF           NVARCHAR(255) NULL,
    UsuarioPersonalID INT NOT NULL,
    FechaGeneracion   DATETIME NULL DEFAULT SYSDATETIME()
);

-- =============================================
-- FOREIGN KEYS (agrupados y comentados por tabla)
-- =============================================

-- >>> Municipios
ALTER TABLE Municipios ADD CONSTRAINT FK_Municipios_Departamento FOREIGN KEY (DepartamentoID) REFERENCES Departamentos(DepartamentoID) ON DELETE CASCADE;

-- >>> Sedes
ALTER TABLE Sedes ADD CONSTRAINT FK_Sedes_Municipio FOREIGN KEY (MunicipioID) REFERENCES Municipios(MunicipioID) ON DELETE CASCADE;

-- >>> Salones
ALTER TABLE Salones ADD CONSTRAINT FK_Salones_Sede FOREIGN KEY (SedeID) REFERENCES Sedes(SedeID) ON DELETE CASCADE;

-- >>> Estudiantes
ALTER TABLE Estudiantes ADD CONSTRAINT FK_Estudiantes_Municipio FOREIGN KEY (Municipio) REFERENCES Municipios(MunicipioID) ON DELETE CASCADE;
ALTER TABLE Estudiantes ADD CONSTRAINT FK_Estudiantes_Carrera FOREIGN KEY (Carrera) REFERENCES Carreras(CarreraID) ON DELETE CASCADE;
ALTER TABLE Estudiantes ADD CONSTRAINT FK_Estudiantes_Disciplina FOREIGN KEY (Disciplina) REFERENCES DisciplinasArtisticas(DisciplinaID) ON DELETE CASCADE;

-- >>> Personal
ALTER TABLE Personal ADD CONSTRAINT FK_Personal_Municipio FOREIGN KEY (Municipio) REFERENCES Municipios(MunicipioID) ON DELETE CASCADE;
ALTER TABLE Personal ADD CONSTRAINT FK_Personal_Cargo FOREIGN KEY (Cargo) REFERENCES Cargos(CargoID) ON DELETE CASCADE;
ALTER TABLE Personal ADD CONSTRAINT FK_Personal_Disciplina FOREIGN KEY (Disciplina) REFERENCES DisciplinasArtisticas(DisciplinaID) ON DELETE CASCADE;

-- >>> Eventos
ALTER TABLE Eventos ADD CONSTRAINT FK_Eventos_Salon FOREIGN KEY (Salon) REFERENCES Salones(SalonID) ON DELETE CASCADE;

-- >>> NecesidadesEvento
ALTER TABLE NecesidadesEvento ADD CONSTRAINT FK_Necesidades_Evento FOREIGN KEY (EventoID) REFERENCES Eventos(EventoID) ON DELETE CASCADE;
ALTER TABLE NecesidadesEvento ADD CONSTRAINT FK_Necesidades_Subcategoria FOREIGN KEY (SubcategoriaID) REFERENCES SubcategoriasNecesidades(SubcategoriaID) ON DELETE CASCADE;

-- >>> SubcategoriasNecesidades
ALTER TABLE SubcategoriasNecesidades ADD CONSTRAINT FK_Subcategorias_Categoria FOREIGN KEY (CategoriaID) REFERENCES CategoriasNecesidades(CategoriaID) ON DELETE CASCADE;

-- >>> ParticipantesEventos
ALTER TABLE ParticipantesEventos     ADD CONSTRAINT FK_Participantes_Evento           FOREIGN KEY (EventoID)         REFERENCES Eventos(EventoID);
ALTER TABLE ParticipantesEventos     ADD CONSTRAINT FK_Participantes_Estudiante       FOREIGN KEY (EstudianteID)     REFERENCES Estudiantes(EstudianteID);

-- >>> ResponsablesEventos
ALTER TABLE ResponsablesEventos      ADD CONSTRAINT FK_Responsables_Evento            FOREIGN KEY (EventoID)         REFERENCES Eventos(EventoID);
ALTER TABLE ResponsablesEventos      ADD CONSTRAINT FK_Responsables_Personal          FOREIGN KEY (PersonalID)       REFERENCES Personal(PersonalID);

-- >>> ProfesoresEstudiantes
ALTER TABLE ProfesoresEstudiantes    ADD CONSTRAINT FK_ProfEst_Profesor               FOREIGN KEY (ProfesorID)       REFERENCES Profesores(ProfesorID);
ALTER TABLE ProfesoresEstudiantes    ADD CONSTRAINT FK_ProfEst_Estudiante             FOREIGN KEY (EstudianteID)     REFERENCES Estudiantes(EstudianteID);

-- >>> UsuariosPersonal
ALTER TABLE UsuariosPersonal         ADD CONSTRAINT FK_UsrPer_Personal                FOREIGN KEY (Personal)         REFERENCES Personal(PersonalID);
ALTER TABLE UsuariosPersonal         ADD CONSTRAINT FK_UsrPer_Estado                  FOREIGN KEY (Estado)           REFERENCES EstadosUsuarios(EstadoID);

-- >>> UsuariosEstudiantes
ALTER TABLE UsuariosEstudiantes      ADD CONSTRAINT FK_UsrEst_Estudiante              FOREIGN KEY (Estudiante)       REFERENCES Estudiantes(EstudianteID);
ALTER TABLE UsuariosEstudiantes      ADD CONSTRAINT FK_UsrEst_Estado                  FOREIGN KEY (Estado)           REFERENCES EstadosUsuarios(EstadoID);

-- >>> CartasGeneradas
ALTER TABLE CartasGeneradas ADD CONSTRAINT FK_Cartas_Plantilla FOREIGN KEY (PlantillaId) REFERENCES PlantillasCarta(Id) ON DELETE CASCADE;
ALTER TABLE CartasGeneradas ADD CONSTRAINT FK_Cartas_UsuarioPersonal FOREIGN KEY (UsuarioPersonalID) REFERENCES UsuariosPersonal(UsuarioPID) ON DELETE CASCADE;
GO

/***********************************************************************
 * Script: Inserción de Categorías y Subcategorías Necesarias para SICAP
 * Descripción: Este script llena las tablas CategoriasNecesidades y 
 *              SubcategoriasNecesidades según el modelo simplificado
 * Autor: Camilo
 * Fecha: 30/11/2025
 ***********************************************************************/

-- *******************************************************
-- 1. Insertar Categorías Necesarias
-- *******************************************************

-- Se insertan las 7 categorías simplificadas:
-- 1 = Equipo
-- 2 = Utileria
-- 3 = Vestuario
-- 4 = Alimentos
-- 5 = Transporte
-- 6 = Instrumentos
-- 7 = Logistica

INSERT INTO CategoriasNecesidades (Nombre) VALUES
('Equipo'),
('Utileria'),
('Vestuario'),
('Alimentos'),
('Transporte'),
('Instrumentos'),
('Logistica');

-- *******************************************************
-- 2. Insertar Subcategorías Necesarias
-- *******************************************************

-- Subcategorías de Equipo (CategoriaID = 1)
INSERT INTO SubcategoriasNecesidades (Nombre, CategoriaID) VALUES
('Microfonos', 1),
('Consolas', 1),
('Parlantes', 1),
('Monitores', 1),
('Cables', 1),
('Luces', 1),
('Controladores', 1),
('Tripodes', 1),
('Proyectores', 1),
('Pantallas', 1);

-- Subcategorías de Utileria (CategoriaID = 2)
INSERT INTO SubcategoriasNecesidades (Nombre, CategoriaID) VALUES
('Utileria teatral', 2),
('Accesorios de escena', 2),
('Atrezzo', 2),
('Decoracion', 2),
('Material artistico', 2),
('Material de animacion', 2);

-- Subcategorías de Vestuario (CategoriaID = 3)
INSERT INTO SubcategoriasNecesidades (Nombre, CategoriaID) VALUES
('Trajes', 3),
('Botargas', 3),
('Vestuario de danza', 3),
('Accesorios', 3),
('Maquillaje artistico', 3);

-- Subcategorías de Alimentos (CategoriaID = 4)
INSERT INTO SubcategoriasNecesidades (Nombre, CategoriaID) VALUES
('Desayunos', 4),
('Almuerzos', 4),
('Cenas', 4),
('Refrigerios', 4),
('Meriendas', 4),
('Bebidas', 4);

-- Subcategorías de Transporte (CategoriaID = 5)
INSERT INTO SubcategoriasNecesidades (Nombre, CategoriaID) VALUES
('Bus', 5),
('Microbus', 5),
('Carga ligera', 5);

-- Subcategorías de Instrumentos (CategoriaID = 6)
INSERT INTO SubcategoriasNecesidades (Nombre, CategoriaID) VALUES
('Cuerdas', 6),
('Marimba', 6),
('Percusion', 6),
('Viento', 6),
('Accesorios musicales', 6);

-- Subcategorías de Logistica (CategoriaID = 7)
INSERT INTO SubcategoriasNecesidades (Nombre, CategoriaID) VALUES
('Agua', 7),
('Bolsas', 7),
('Cintas', 7),
('Cajas', 7),
('Baterias', 7),
('Miscelaneos', 7);

-- *******************************************************
-- Fin del Script
-- *******************************************************
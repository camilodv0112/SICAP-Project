USE [db27572]
GO

-- =============================================
-- SCRIPT DE POBLADO DE CATÁLOGOS GENERALES
-- =============================================

-- 1. Disciplinas Artísticas
INSERT INTO DisciplinasArtisticas (Nombre) VALUES 
('Danza'),
('Música'),
('Teatro'),
('Artes Plásticas'),
('Literatura'),
('Cine y Audiovisuales');

-- 2. Cargos
INSERT INTO Cargos (Nombre, Descripcion) VALUES 
('Coordinador', 'Encargado de coordinar áreas o proyectos'),
('Docente', 'Profesor encargado de impartir clases'),
('Administrativo', 'Personal de gestión administrativa'),
('Director', 'Director de departamento o área'),
('Apoyo Logístico', 'Personal de soporte en eventos y actividades');

-- 3. Departamentos
INSERT INTO Departamentos (Nombre) VALUES 
('Managua'),
('León'),
('Granada'),
('Masaya'),
('Matagalpa');

-- 4. Municipios (Asumiendo IDs de Departamentos creados arriba: 1=Managua, 2=León, etc.)
INSERT INTO Municipios (Nombre, DepartamentoID) VALUES 
('Managua', 1),
('Ciudad Sandino', 1),
('Tipitapa', 1),
('León', 2),
('Granada', 3),
('Masaya', 4),
('Matagalpa', 5);

-- 5. Sedes (Asumiendo IDs de Municipios: 1=Managua)
INSERT INTO Sedes (Nombre, Descripcion, MunicipioID) VALUES 
('Sede Central', 'Campus principal de la universidad', 1),
('Recinto Ruben Dario', 'Recinto de humanidades y ciencias', 1),
('Sede Regional León', 'Campus en la ciudad de León', 4);

-- 6. Salones (Asumiendo IDs de Sedes: 1=Sede Central)
INSERT INTO Salones (Nombre, Capacidad, Descripcion, SedeID) VALUES 
('Auditorio Mayor', 200, 'Auditorio principal para grandes eventos', 1),
('Sala de Danza 1', 30, 'Sala con espejos y piso de madera', 1),
('Sala de Música', 20, 'Sala insonorizada para ensayos', 1),
('Aula 101', 40, 'Aula teórica estándar', 1),
('Anfiteatro', 100, 'Espacio al aire libre', 2);

-- 7. Carreras
INSERT INTO Carreras (Nombre) VALUES 
('Ingeniería en Sistemas'),
('Licenciatura en Danza'),
('Arquitectura'),
('Administración de Empresas'),
('Marketing'),
('Comunicación Social');

-- 8. Estados de Usuarios
INSERT INTO EstadosUsuarios (Nombre) VALUES 
('Activo'),
('Inactivo'),
('Bloqueado'),
('Pendiente');

GO

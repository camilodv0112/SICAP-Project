using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos.Models;

public class Estudiante
{
    [Key]
    public int EstudianteID { get; set; }

    [Required, MaxLength(50)]
    public string PrimerNombre { get; set; }

    [Required, MaxLength(50)]
    public string SegundoNombre { get; set; }

    [Required, MaxLength(50)]
    public string PrimerApellido { get; set; }

    [Required, MaxLength(50)]
    public string SegundoApellido { get; set; }

    [Required, MaxLength(30)]
    public string Cedula { get; set; }

    [MaxLength(1)]
    public string Sexo { get; set; }

    [Required, MaxLength(20)]
    public string Carnet { get; set; }

    [Required, MaxLength(15)]
    public string Celular { get; set; }

    [MaxLength(100)]
    public string CorreoElectronico { get; set; }

    [ForeignKey("MunicipioNavigation")]
    public int Municipio { get; set; }
    public Municipio MunicipioNavigation { get; set; }

    [Required, MaxLength(250)]
    public string Direccion { get; set; }

    [ForeignKey("CarreraNavigation")]
    public int Carrera { get; set; }
    public Carrera CarreraNavigation { get; set; }

    [Required, MaxLength(10)]
    public string Grupo { get; set; }

    [Required]
    public int Año { get; set; }

    [ForeignKey("DisciplinaNavigation")]
    public int Disciplina { get; set; }
    public DisciplinaArtistica DisciplinaNavigation { get; set; }

    public ICollection<Profesor> Profesores { get; set; }
    public ICollection<Evento> Eventos { get; set; }
    public ICollection<UsuarioEstudiante> Usuarios { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos.Models;

public class Personal
{
    [Key]
    public int PersonalID { get; set; }

    [Required, MaxLength(50)]
    public string PrimerNombre { get; set; }

    [Required, MaxLength(50)]
    public string SegundoNombre { get; set; }

    [Required, MaxLength(50)]
    public string PrimerApellido { get; set; }

    [Required, MaxLength(50)]
    public string SegundoApellido { get; set; }

    [MaxLength(1)]
    public string Sexo { get; set; }

    [ForeignKey("CargoNavigation")]
    public int Cargo { get; set; }
    public Cargo CargoNavigation { get; set; }

    [ForeignKey("DisciplinaNavigation")]
    public int Disciplina { get; set; }
    public DisciplinaArtistica DisciplinaNavigation { get; set; }

    [Required, MaxLength(30)]
    public string Cedula { get; set; }

    [MaxLength(20)]
    public string NumeroEmpleado { get; set; }

    [Required, MaxLength(15)]
    public string Celular { get; set; }

    [MaxLength(100)]
    public string CorreoElectronico { get; set; }

    [ForeignKey("MunicipioNavigation")]
    public int Municipio { get; set; }
    public Municipio MunicipioNavigation { get; set; }

    [Required, MaxLength(250)]
    public string DireccionDomiciliaria { get; set; }

    public ICollection<Evento> Eventos { get; set; }
    public ICollection<UsuarioPersonal> Usuarios { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.Estudiante;

public class EstudianteCreateDTO
{
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

    [EmailAddress, MaxLength(100)]
    public string CorreoElectronico { get; set; }

    [Required]
    public int Municipio { get; set; }

    [Required, MaxLength(250)]
    public string Direccion { get; set; }

    [Required]
    public int Carrera { get; set; }

    [Required, MaxLength(10)]
    public string Grupo { get; set; }

    [Required]
    public int Año { get; set; }

    [Required]
    public int Disciplina { get; set; }
}

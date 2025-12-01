using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.Profesor;

public class ProfesorUpdateDTO
{
    [Required]
    public int ProfesorID { get; set; }

    [MaxLength(5)]
    public string GradoAcademico { get; set; }

    [Required, MaxLength(50)]
    public string PrimerNombre { get; set; }

    [Required, MaxLength(50)]
    public string SegundoNombre { get; set; }

    [Required, MaxLength(50)]
    public string PrimerApellido { get; set; }

    [Required, MaxLength(50)]
    public string SegundoApellido { get; set; }

    [Required, MaxLength(50)]
    public string Asignatura { get; set; }
}

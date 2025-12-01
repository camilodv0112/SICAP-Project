using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.UsuarioEstudiante;

public class UsuarioEstudianteCreateDTO
{
    [Required, MaxLength(50)]
    public string Usuario { get; set; }

    [Required, MinLength(6), MaxLength(50)]
    public string Contraseña { get; set; }

    [Required]
    public int Estudiante { get; set; }

    [Required]
    public int Estado { get; set; }
}

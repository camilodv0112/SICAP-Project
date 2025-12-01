using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.UsuarioEstudiante;

public class UsuarioEstudianteUpdateDTO
{
    [Required]
    public int UsuarioEID { get; set; }

    [Required, MaxLength(50)]
    public string Usuario { get; set; }

    [MinLength(6), MaxLength(50)]
    public string Contraseña { get; set; }

    [Required]
    public int Estado { get; set; }
}

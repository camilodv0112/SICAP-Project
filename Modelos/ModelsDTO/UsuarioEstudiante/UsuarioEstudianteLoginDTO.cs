using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.UsuarioEstudiante;

public class UsuarioEstudianteLoginDTO
{
    [Required, MaxLength(50)]
    public string Usuario { get; set; }

    [Required, MaxLength(50)]
    public string Contraseña { get; set; }
}

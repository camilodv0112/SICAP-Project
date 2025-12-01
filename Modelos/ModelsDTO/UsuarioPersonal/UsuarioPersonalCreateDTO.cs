using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.UsuarioPersonal;

public class UsuarioPersonalCreateDTO
{
    [Required, MaxLength(50)]
    public string Usuario { get; set; }

    [Required, MinLength(6), MaxLength(50)]
    public string Contraseña { get; set; }

    [Required]
    public int Personal { get; set; }

    [Required]
    public int Estado { get; set; }
}

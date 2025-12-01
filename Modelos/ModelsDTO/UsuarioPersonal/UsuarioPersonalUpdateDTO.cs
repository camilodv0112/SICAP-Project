using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.UsuarioPersonal;

public class UsuarioPersonalUpdateDTO
{
    [Required]
    public int UsuarioPID { get; set; }

    [Required, MaxLength(50)]
    public string Usuario { get; set; }

    [MinLength(6), MaxLength(50)]
    public string Contraseña { get; set; }

    [Required]
    public int Estado { get; set; }
}

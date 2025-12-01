using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.UsuarioPersonal;

public class UsuarioPersonalLoginDTO
{
    [Required, MaxLength(50)]
    public string Usuario { get; set; }

    [Required, MaxLength(50)]
    public string Contraseña { get; set; }
}

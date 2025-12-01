using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.EstadoUsuario;

public class EstadoUsuarioCreateDTO
{
    [Required, MaxLength(50)]
    public string Nombre { get; set; }
}

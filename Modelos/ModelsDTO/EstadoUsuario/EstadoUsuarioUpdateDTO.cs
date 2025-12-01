using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.EstadoUsuario;

public class EstadoUsuarioUpdateDTO
{
    [Required]
    public int EstadoID { get; set; }

    [Required, MaxLength(50)]
    public string Nombre { get; set; }
}

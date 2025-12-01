using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.CartaGenerada;

public class CartaGeneradaCreateDTO
{
    [Required]
    public int PlantillaId { get; set; }

    [Required, MaxLength(150)]
    public string NombreCarta { get; set; }

    public string Datos { get; set; }

    [MaxLength(255)]
    public string RutaPDF { get; set; }

    [Required]
    public int UsuarioPersonalID { get; set; }
}

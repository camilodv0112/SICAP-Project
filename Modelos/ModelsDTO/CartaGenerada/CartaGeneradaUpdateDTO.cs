using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.CartaGenerada;

public class CartaGeneradaUpdateDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int PlantillaId { get; set; }

    [Required, MaxLength(150)]
    public string NombreCarta { get; set; }

    public string Datos { get; set; }

    [MaxLength(255)]
    public string RutaPDF { get; set; }
}

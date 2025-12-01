using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.NecesidadEvento;

public class NecesidadEventoUpdateDTO
{
    [Required]
    public int NecesidadID { get; set; }

    [Required]
    public int EventoID { get; set; }

    [Required]
    public int SubcategoriaID { get; set; }

    [Required]
    public int Cantidad { get; set; }

    [MaxLength(255)]
    public string Observaciones { get; set; }
}

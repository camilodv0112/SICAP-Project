using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.DisciplinaArtistica;

public class DisciplinaArtisticaUpdateDTO
{
    [Required]
    public int DisciplinaID { get; set; }

    [Required, MaxLength(100)]
    public string Nombre { get; set; }
}

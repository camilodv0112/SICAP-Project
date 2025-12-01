using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.DisciplinaArtistica;

public class DisciplinaArtisticaCreateDTO
{
    [Required, MaxLength(100)]
    public string Nombre { get; set; }
}

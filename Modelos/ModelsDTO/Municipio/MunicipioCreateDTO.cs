using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.Municipio;

public class MunicipioCreateDTO
{
    [Required, MaxLength(100)]
    public string Nombre { get; set; }

    [Required]
    public int DepartamentoID { get; set; }
}

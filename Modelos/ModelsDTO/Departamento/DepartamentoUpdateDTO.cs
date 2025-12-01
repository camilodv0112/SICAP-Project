using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.Departamento;

public class DepartamentoUpdateDTO
{
    [Required]
    public int DepartamentoID { get; set; }

    [Required, MaxLength(100)]
    public string Nombre { get; set; }
}

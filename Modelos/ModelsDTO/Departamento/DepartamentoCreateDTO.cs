using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.Departamento;

public class DepartamentoCreateDTO
{
    [Required, MaxLength(100)]
    public string Nombre { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace Modelos.ModelsDTO.CategoriaNecesidad;

public class CategoriaNecesidadUpdateDTO
{
    [Required]
    public int CategoriaID { get; set; }

    [Required, MaxLength(100)]
    public string Nombre { get; set; }

    [MaxLength(255)]
    public string Descripcion { get; set; }
}

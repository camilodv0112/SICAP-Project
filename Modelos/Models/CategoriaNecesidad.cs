using System.ComponentModel.DataAnnotations;

namespace Modelos.Models;

public class CategoriaNecesidad
{
    [Key]
    public int CategoriaID { get; set; }

    [Required, MaxLength(100)]
    public string Nombre { get; set; }

    [MaxLength(255)]
    public string Descripcion { get; set; }

    public ICollection<SubcategoriaNecesidad> Subcategorias { get; set; }
}

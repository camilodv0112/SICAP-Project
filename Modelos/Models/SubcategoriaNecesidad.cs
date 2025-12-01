using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos.Models;

public class SubcategoriaNecesidad
{
    [Key]
    public int SubcategoriaID { get; set; }

    [ForeignKey("Categoria")]
    public int CategoriaID { get; set; }
    public CategoriaNecesidad Categoria { get; set; }

    [Required, MaxLength(100)]
    public string Nombre { get; set; }

    [MaxLength(255)]
    public string Descripcion { get; set; }

    public ICollection<NecesidadEvento> NecesidadesEventos { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos.Models;

public class NecesidadEvento
{
    [Key]
    public int NecesidadID { get; set; }

    [ForeignKey("Evento")]
    public int EventoID { get; set; }
    public Evento Evento { get; set; }

    [ForeignKey("Subcategoria")]
    public int SubcategoriaID { get; set; }
    public SubcategoriaNecesidad Subcategoria { get; set; }

    [Required]
    public int Cantidad { get; set; }

    [MaxLength(255)]
    public string Observaciones { get; set; }
}

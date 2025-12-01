namespace Modelos.ModelsDTO.NecesidadEvento;

public class NecesidadEventoResponseDTO
{
    public int NecesidadID { get; set; }
    public int EventoID { get; set; }
    public string EventoNombre { get; set; }
    public int SubcategoriaID { get; set; }
    public string SubcategoriaNombre { get; set; }
    public string CategoriaNombre { get; set; }
    public int Cantidad { get; set; }
    public string Observaciones { get; set; }
}

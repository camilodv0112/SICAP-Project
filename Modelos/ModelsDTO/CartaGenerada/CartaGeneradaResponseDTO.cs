namespace Modelos.ModelsDTO.CartaGenerada;

public class CartaGeneradaResponseDTO
{
    public int Id { get; set; }
    public int PlantillaId { get; set; }
    public string PlantillaNombre { get; set; }
    public string NombreCarta { get; set; }
    public string Datos { get; set; }
    public string RutaPDF { get; set; }
    public int UsuarioPersonalID { get; set; }
    public string UsuarioPersonalNombre { get; set; }
    public DateTime? FechaGeneracion { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace Modelos.Models;

public class EstadoUsuario
{
    [Key]
    public int EstadoID { get; set; }

    [Required, MaxLength(50)]
    public string Nombre { get; set; }

    public ICollection<UsuarioPersonal> UsuariosPersonal { get; set; }
    public ICollection<UsuarioEstudiante> UsuariosEstudiantes { get; set; }
}

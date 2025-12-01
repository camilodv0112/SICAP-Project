using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos.Models;

public class UsuarioPersonal
{
    [Key]
    public int UsuarioPID { get; set; }

    [Required, MaxLength(50)]
    public string Usuario { get; set; }

    [Required]
    public byte[] Contraseña { get; set; }

    [ForeignKey("PersonalNavigation")]
    public int Personal { get; set; }
    public Personal PersonalNavigation { get; set; }

    [ForeignKey("EstadoNavigation")]
    public int Estado { get; set; }
    public EstadoUsuario EstadoNavigation { get; set; }

    public ICollection<CartaGenerada> CartasGeneradas { get; set; }
}

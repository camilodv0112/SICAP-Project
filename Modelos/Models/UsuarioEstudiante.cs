using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos.Models;

public class UsuarioEstudiante
{
    [Key]
    public int UsuarioEID { get; set; }

    [Required, MaxLength(50)]
    public string Usuario { get; set; }

    [Required]
    public byte[] Contraseña { get; set; }

    [ForeignKey("EstudianteNavigation")]
    public int Estudiante { get; set; }
    public Estudiante EstudianteNavigation { get; set; }

    [ForeignKey("EstadoNavigation")]
    public int Estado { get; set; }
    public EstadoUsuario EstadoNavigation { get; set; }
}

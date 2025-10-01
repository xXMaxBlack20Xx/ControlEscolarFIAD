/*
 Este seria el buffet de maestros que se pueden asignar a las carreras
 */

using System.ComponentModel.DataAnnotations;

namespace Entidades.Modelos.PlanesDeEstudio.Carreras;

public class E_Docentes
{
    [Key]
    public int? IdDocente { get; set; }

    [Required]
    [MaxLength(10)]
    public string NumeroEmpleado { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string NombreDocente { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string PaternoDocente { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string MaternoDocente { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string EmailAlterno { get; set; } = string.Empty;

    [Required]
    public bool EstadoDocente { get; set; } = true;
    
    // Propiedad de navegacion inversa
    public ICollection<E_Carrera> CarrerasCoordinadas { get; set; } = new List<E_Carrera>();
}
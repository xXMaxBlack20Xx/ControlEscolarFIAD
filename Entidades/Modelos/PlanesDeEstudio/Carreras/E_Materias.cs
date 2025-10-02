using System.ComponentModel.DataAnnotations;

namespace Entidades.Modelos.PlanesDeEstudio.Carreras;

public class E_Materias
{
    [Key]
    public int IdMateria { get; set; }

    [Required]
    [MaxLength(6)]
    public string ClaveMateria { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string NombreMateria { get; set; } = string.Empty;

    // Horas de clase
    [Required]
    public int HC { get; set; }

    // Horas de laboratorio
    [Required]
    public int HL { get; set; }

    // Horas taller
    [Required]
    public int HT { get; set; }

    // Horas por ciclo
    [Required]
    public int HPC { get; set; }

    // Horas clase laboratorio
    [Required]
    public int HCL { get; set; }

    // Hora Estimulos
    [Required]
    public int HE { get; set; }

    // Creditos
    [Required]
    public int CR { get; set; }

    [Required]
    [MaxLength(200)]
    public string PropositoGeneral { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Competencia { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Evidencia { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Metodologia { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Criterios { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string BibliografiaBasica { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string BibliografiaComplementaria { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string PerfilDocente { get; set; } = string.Empty;

    [Required]
    [MaxLength(256)]
    public string? PathPUA { get; set; } = string.Empty;

    [Required]
    public bool EstadoMateria { get; set; } = true;

    // Colección para la relación muchos a muchos a través de la tabla intermedia
    public ICollection<E_PlanEstudioMateria> PlanesEstudioMaterias { get; set; } = [];
}
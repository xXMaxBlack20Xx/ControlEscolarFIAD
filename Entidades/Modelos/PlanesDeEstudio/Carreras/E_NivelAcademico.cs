using System.ComponentModel.DataAnnotations;

namespace Entidades.Modelos.PlanesDeEstudio.Carreras;

public class E_NivelAcademico
{
    [Key]
    public int IdNivelAcademico { get; set; }

    [Required]
    [MaxLength(30)]
    public string NombreNivelAcademico { get; set; } = string.Empty;

    public ICollection<E_PlanEstudio> PlanesEstudio { get; set; } = [];
}
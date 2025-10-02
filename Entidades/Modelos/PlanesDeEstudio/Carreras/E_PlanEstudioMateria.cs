using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades.Modelos.PlanesDeEstudio.Carreras;

public class E_PlanEstudioMateria
{
    [Key]
    public int IdPlanEstudioMateria { get; set; }

    // Foreign key de E_PlanesEstudio
    [Required]
    public int IdPlanEstudio { get; set; }

    // Foreign key de E_Materias
    [Required]
    public int IdMateria { get; set; }

    [Required]
    public int? Semestre { get; set; }

    [Required]
    public bool? Estado { get; set; }

    // PROPIEDADES DE NAVEGACIÓN
    [ForeignKey(nameof(IdPlanEstudio))]
    public E_PlanEstudio PlanEstudio { get; set; } = new E_PlanEstudio();

    [ForeignKey(nameof(IdMateria))]
    public E_Materias Materia { get; set; } = new E_Materias();
}
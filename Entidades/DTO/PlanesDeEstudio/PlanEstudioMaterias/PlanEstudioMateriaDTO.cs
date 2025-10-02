
using System.ComponentModel.DataAnnotations;

namespace Entidades.DTO.PlanesDeEstudio.PlanEstudioMaterias;

public class PlanEstudioMateriaDTO
{
    public int IdPlanEstudioMateria { get; set; }

    [Required(ErrorMessage = "Debe proporcionar el ID del Plan de Estudio.")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del Plan de Estudio no es válido.")]
    public int IdPlanEstudio { get; set; }

    [Required(ErrorMessage = "Debe seleccionar una materia.")]
    [Range(1, int.MaxValue, ErrorMessage = "La materia seleccionada no es válida.")]
    public int IdMateria { get; set; }

    [Required(ErrorMessage = "Debe especificar el semestre.")]
    [Range(1, 12, ErrorMessage = "El semestre debe estar entre 1 y 12.")]
    public int? Semestre { get; set; }

    [Required(ErrorMessage = "Debe especificar el estado de la materia en el plan de estudio.")]
    public bool? Estado { get; set; }

    // Propiedades adicionales para detalles de la materia
    public string? NombrePlanEstudio { get; set; } = string.Empty;

    public string? ClaveMateria { get; set; } = string.Empty;

    public string? NombreMateria { get; set; } = string.Empty;

    public int? CreditosMateria { get; set; }
}
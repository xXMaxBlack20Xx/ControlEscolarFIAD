/*
    Aqui usaremos Data anottacion para poder hacer un Data Transfer Object en el cual nos servira para los formularios 
    y validar reglas adicionales y mapearlas con el AutoMapper
*/

using System.ComponentModel.DataAnnotations;

namespace Entidades.DTO.PlanesDeEstudio.PlanEstudios;

public class PlanEstudioDTO : IValidatableObject
{
    public int IdPlanEstudio { get; set; }

    [Required(ErrorMessage = "Debe capturar el plan de estudios.")]
    [RegularExpression(@"^\d{4}-[124]$", ErrorMessage = "El formato debe ser AAAA-D, donde AAAA es un año y D es 1, 2 o 4.")]
    public string PlanEstudio { get; set; } = string.Empty;

    // Si la BD ya asigna fecha por default, considera quitar Required o incluso remover la propiedad del DTO.
    public DateTime? FechaCreacion { get; set; }

    [Required(ErrorMessage = "Debe capturar el total de créditos.")]
    [Range(1, int.MaxValue, ErrorMessage = "El total de créditos debe ser mayor a 0.")]
    public int TotalCreditos { get; set; }

    [Required(ErrorMessage = "Debe capturar los créditos optativos.")]
    [Range(0, int.MaxValue, ErrorMessage = "Los créditos optativos deben ser 0 o mayores.")]
    public int CreditosOptativos { get; set; }

    [Required(ErrorMessage = "Debe capturar los créditos obligatorios.")]
    [Range(1, int.MaxValue, ErrorMessage = "Los créditos obligatorios deben ser mayores a 0.")]
    public int CreditosObligatorios { get; set; }

    [Required(ErrorMessage = "Debe capturar el perfil de ingreso.")]
    [StringLength(2048, ErrorMessage = "El perfil de ingreso no debe exceder 2048 caracteres.")]
    public string PerfilDeIngreso { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe capturar el perfil de egreso.")]
    [StringLength(2048, ErrorMessage = "El perfil de egreso no debe exceder 2048 caracteres.")]
    public string PerfilDeEgreso { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe capturar el campo ocupacional.")]
    [StringLength(2048, ErrorMessage = "El campo ocupacional no debe exceder 2048 caracteres.")]
    public string CampoOcupacional { get; set; } = string.Empty;

    // Comentarios normalmente es opcional; si lo quieres obligatorio, deja Required.
    [StringLength(2048, ErrorMessage = "Los comentarios no deben exceder 2048 caracteres.")]
    public string? Comentarios { get; set; }

    [Required(ErrorMessage = "Debe indicar el estado del plan de estudios.")]
    public bool EstadoPlanEstudio { get; set; } = true;

    // FK (sin [ForeignKey] en DTO)
    [Required(ErrorMessage = "Seleccione una carrera.")]
    [Range(1, int.MaxValue, ErrorMessage = "Seleccione una carrera válida.")]
    public int IdCarrera { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (CreditosOptativos + CreditosObligatorios != TotalCreditos)
        {
            yield return new ValidationResult(
                "La suma de créditos optativos y obligatorios debe ser igual al total de créditos.",
                new[] { nameof(CreditosOptativos), nameof(CreditosObligatorios), nameof(TotalCreditos) }
            );
        }
    }
}
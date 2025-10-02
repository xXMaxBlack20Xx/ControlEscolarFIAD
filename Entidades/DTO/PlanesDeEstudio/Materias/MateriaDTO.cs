using System.ComponentModel.DataAnnotations;

namespace Entidades.DTO.PlanesDeEstudio.Materias;

public class MateriaDTO
{
    [Required(ErrorMessage = "La clave de la materia es obligatoria.")]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "La clave debe tener exactamente 6 caracteres.")]
    public string ClaveMateria { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre de la materia es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres.")]
    public string NombreMateria { get; set; } = string.Empty;

    [Required(ErrorMessage = "Las Horas de Clase (HC) son obligatorias.")]
    [Range(0, 99, ErrorMessage = "El valor de HC debe estar entre 0 y 99.")]
    public int HC { get; set; }

    [Required(ErrorMessage = "Las Horas de Laboratorio (HL) son obligatorias.")]
    [Range(0, 99, ErrorMessage = "El valor de HL debe estar entre 0 y 99.")]
    public int HL { get; set; }

    [Required(ErrorMessage = "Las Horas de Taller (HT) son obligatorias.")]
    [Range(0, 99, ErrorMessage = "El valor de HT debe estar entre 0 y 99.")]
    public int HT { get; set; }

    [Required(ErrorMessage = "Las Horas por Ciclo (HPC) son obligatorias.")]
    [Range(0, 999, ErrorMessage = "El valor de HPC debe ser 0 o mayor.")]
    public int HPC { get; set; }

    [Required(ErrorMessage = "Las Horas Clase-Laboratorio (HCL) son obligatorias.")]
    [Range(0, 99, ErrorMessage = "El valor de HCL debe ser 0 o mayor.")]
    public int HCL { get; set; }

    [Required(ErrorMessage = "Las Horas de Estímulos (HE) son obligatorias.")]
    [Range(0, 99, ErrorMessage = "El valor de HE debe ser 0 o mayor.")]
    public int HE { get; set; }

    [Required(ErrorMessage = "Los Créditos (CR) son obligatorios.")]
    [Range(1, 99, ErrorMessage = "Los créditos deben ser mayores a 0.")]
    public int CR { get; set; }

    [Required(ErrorMessage = "El propósito general es obligatorio.")]
    [StringLength(200, ErrorMessage = "El propósito general no debe exceder los 200 caracteres.")]
    public string PropositoGeneral { get; set; } = string.Empty;

    [Required(ErrorMessage = "La competencia es obligatoria.")]
    [StringLength(200, ErrorMessage = "La competencia no debe exceder los 200 caracteres.")]
    public string Competencia { get; set; } = string.Empty;

    [Required(ErrorMessage = "La evidencia es obligatoria.")]
    [StringLength(200, ErrorMessage = "La evidencia no debe exceder los 200 caracteres.")]
    public string Evidencia { get; set; } = string.Empty;

    [Required(ErrorMessage = "La metodología es obligatoria.")]
    [StringLength(200, ErrorMessage = "La metodología no debe exceder los 200 caracteres.")]
    public string Metodologia { get; set; } = string.Empty;

    [Required(ErrorMessage = "Los criterios son obligatorios.")]
    [StringLength(200, ErrorMessage = "Los criterios no deben exceder los 200 caracteres.")]
    public string Criterios { get; set; } = string.Empty;

    [Required(ErrorMessage = "La bibliografía básica es obligatoria.")]
    [StringLength(200, ErrorMessage = "La bibliografía básica no debe exceder los 200 caracteres.")]
    public string BibliografiaBasica { get; set; } = string.Empty;

    [Required(ErrorMessage = "La bibliografía complementaria es obligatoria.")]
    [StringLength(200, ErrorMessage = "La bibliografía complementaria no debe exceder los 200 caracteres.")]
    public string BibliografiaComplementaria { get; set; } = string.Empty;

    [Required(ErrorMessage = "El perfil docente es obligatorio.")]
    [StringLength(200, ErrorMessage = "El perfil docente no debe exceder los 200 caracteres.")]
    public string PerfilDocente { get; set; } = string.Empty;

    [StringLength(256, ErrorMessage = "La ruta del archivo PUA no debe exceder los 256 caracteres.")]
    public string? PathPUA { get; set; }

    public bool EstadoMateria { get; set; } = true;

    /// <summary>
    /// Realiza validaciones complejas que involucran múltiples propiedades.
    /// </summary>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // Regla de negocio: La suma de las horas debe coincidir con el total por ciclo.
        if (HC + HL + HT != HPC)
        {
            yield return new ValidationResult(
                "La suma de Horas Clase (HC), Laboratorio (HL) y Taller (HT) debe ser igual a las Horas Por Ciclo (HPC).",
                [nameof(HC), nameof(HL), nameof(HT), nameof(HPC)]
            );
        }
    }
}

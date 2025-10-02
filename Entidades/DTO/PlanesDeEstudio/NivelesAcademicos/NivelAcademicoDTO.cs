using System.ComponentModel.DataAnnotations;

namespace Entidades.DTO.PlanesDeEstudio.NivelesAcademicos;

public class NivelAcademicoDTO
{
    [Key]
    public int IdNivelAcademico { get; set; }

    [Required(ErrorMessage = "Debe capturar el nombre del nivel académico.")]
    [StringLength(30, ErrorMessage = "El nombre no debe exceder 30 caracteres.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
    public string NombreNivelAcademico { get; set; } = string.Empty;
}
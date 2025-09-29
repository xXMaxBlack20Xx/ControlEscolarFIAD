using System.ComponentModel.DataAnnotations;

namespace Entidades.DTO.PlanesDeEstudio.Carreras;

public class CarreraDTO
{
    public int IdCarrera { get; set; }

    [Required(ErrorMessage = "Debe capturar la clave de la carrera.")]
    [RegularExpression(@"^[A-Z]{3}$", ErrorMessage = "La clave debe tener exactamente 3 letras mayúsculas (A–Z).")]
    public string ClaveCarrera { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe capturar el nombre de la carrera.")]
    [StringLength(50, ErrorMessage = "El nombre de la carrera no debe exceder 50 caracteres.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s\-,.']+$", ErrorMessage = "El nombre solo puede contener letras, espacios y los caracteres: - , ' .")]
    public string NombreCarrera { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe capturar el alias de la carrera.")]
    [StringLength(50, ErrorMessage = "El alias de la carrera no debe exceder 50 caracteres.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s\-,.']+$", ErrorMessage = "El alias solo puede contener letras, espacios y los caracteres: - , ' .")]
    public string AliasCarrera { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe especificar el coordinador.")]
    [Range(1, int.MaxValue, ErrorMessage = "El Id de coordinador debe ser un entero positivo.")]
    public int? IdCoordinador { get; set; }

    public bool EstadoCarrera { get; set; } = true;
}
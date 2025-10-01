using System.ComponentModel.DataAnnotations;

namespace Entidades.DTO.PlanesDeEstudio.Docentes;

public class DocenteDTO
{
    public int? IdDocente { get; set; }

    [Required(ErrorMessage = "Debe capturar el número de empleado.")]
    [StringLength(10, ErrorMessage = "El número de empleado no debe exceder 10 caracteres.")]
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "El número de empleado solo puede contener letras y números.")]
    public string NumeroEmpleado { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe capturar el nombre del docente.")]
    [StringLength(100, ErrorMessage = "El nombre del docente no debe exceder 100 caracteres.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s\-,.']+$", ErrorMessage = "El nombre solo puede contener letras, espacios y los caracteres: - , ' .")]
    public string NombreDocente { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe capturar el apellido paterno.")]
    [StringLength(100, ErrorMessage = "El apellido paterno no debe exceder 100 caracteres.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s\-,.']+$", ErrorMessage = "El apellido paterno solo puede contener letras y los caracteres: - , ' .")]
    public string PaternoDocente { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe capturar el apellido materno.")]
    [StringLength(100, ErrorMessage = "El apellido materno no debe exceder 100 caracteres.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s\-,.']+$", ErrorMessage = "El apellido materno solo puede contener letras y los caracteres: - , ' .")]
    public string MaternoDocente { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe capturar un correo electrónico.")]
    [StringLength(150, ErrorMessage = "El correo electrónico no debe exceder 150 caracteres.")]
    [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
    public string EmailAlterno { get; set; } = string.Empty;

    public bool EstadoDocente { get; set; } = true;
}
/* 
    Aqui vamos a estar definiendo la tabla de Carreras y PlanesEstudio 
    es una propiedad de navegacion que establese una relacion de 
    1:N (Osea de uno a muchos)
*/

using System.ComponentModel.DataAnnotations;

namespace Entidades.Modelos.PlanesDeEstudio.Carreras;

public class E_Carrera
{
    [Key]
    public int IdCarrera { get; set; }

    [Required]
    [MaxLength(3)]
    public string ClaveCarrera { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string NombreCarrera { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string AliasCarrera { get; set; } = string.Empty;

    [Required]
    public int? IdCoordinador { get; set; }

    [Required]
    public bool EstadoCarrera { get; set; } = true;

    // Navigation
    public ICollection<E_PlanEstudio> PlanEstudios { get; set; } = new List<E_PlanEstudio>();
}
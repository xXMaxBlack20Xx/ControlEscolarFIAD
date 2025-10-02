/* 
    Aqui vamos a estar definiendo la tabla de Carreras y PlanesEstudio 
    es una propiedad de navegacion que establese una relacion de 
    1:N (Osea de uno a muchos)
*/

using Entidades.Modelos.PlanesDeEstudio.Docentes;
using Entidades.Modelos.PlanesDeEstudio.PlanEstudios;

namespace Entidades.Modelos.PlanesDeEstudio.Carreras;

public class E_Carrera
{
    public int IdCarrera { get; set; }

    public string ClaveCarrera { get; set; } = string.Empty;

    public string NombreCarrera { get; set; } = string.Empty;

    public string AliasCarrera { get; set; } = string.Empty;

    public int? IdCoordinador { get; set; }

    public bool EstadoCarrera { get; set; } = true;

    public E_Docentes? Coordinador { get; set; }

    // Propiedad de navegación para la relación 1:N con PlanEstudio
    public ICollection<E_PlanEstudio> PlanEstudios { get; set; } = [];
}
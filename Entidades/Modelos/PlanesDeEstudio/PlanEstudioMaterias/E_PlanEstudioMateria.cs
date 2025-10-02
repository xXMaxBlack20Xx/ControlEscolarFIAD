using Entidades.Modelos.PlanesDeEstudio.Materias;
using Entidades.Modelos.PlanesDeEstudio.PlanEstudios;

namespace Entidades.Modelos.PlanesDeEstudio.PlanEstudioMaterias;

public class E_PlanEstudioMateria
{
    public int IdPlanEstudioMateria { get; set; }

    // Foreign key de E_PlanesEstudio
    public int IdPlanEstudio { get; set; }

    // Foreign key de E_Materias
    public int IdMateria { get; set; }

    public int? Semestre { get; set; }

    public bool? Estado { get; set; }

    // PROPIEDADES DE NAVEGACIÓN
    // IdPlanEstudio
    public E_PlanEstudio PlanEstudio { get; set; } = new E_PlanEstudio();

    // IdMateria
    public E_Materias Materia { get; set; } = new E_Materias();
}
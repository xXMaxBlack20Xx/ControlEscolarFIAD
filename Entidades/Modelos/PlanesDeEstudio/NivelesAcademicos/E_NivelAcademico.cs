using Entidades.Modelos.PlanesDeEstudio.PlanEstudios;
using System.ComponentModel.DataAnnotations;

namespace Entidades.Modelos.PlanesDeEstudio.NivelesAcademicos;

public class E_NivelAcademico
{
    public int IdNivelAcademico { get; set; }

    public string NombreNivelAcademico { get; set; } = string.Empty;

    public ICollection<E_PlanEstudio> PlanesEstudio { get; set; } = [];
}
using Entidades.Modelos.PlanesDeEstudio.PlanEstudioMaterias;
using System.ComponentModel.DataAnnotations;

namespace Entidades.Modelos.PlanesDeEstudio.Materias;

public class E_Materias
{
    public int IdMateria { get; set; }

    public string ClaveMateria { get; set; } = string.Empty;

    public string NombreMateria { get; set; } = string.Empty;

    // Horas de clase
    public int HC { get; set; }

    // Horas de laboratorio
    public int HL { get; set; }

    // Horas taller
    public int HT { get; set; }

    // Horas por ciclo
    public int HPC { get; set; }

    // Horas clase laboratorio
    public int HCL { get; set; }

    // Hora Estimulos
    public int HE { get; set; }

    // Creditos
    public int CR { get; set; }

    public string PropositoGeneral { get; set; } = string.Empty;

    public string Competencia { get; set; } = string.Empty;

    public string Evidencia { get; set; } = string.Empty;

    public string Metodologia { get; set; } = string.Empty;

    public string Criterios { get; set; } = string.Empty;

    public string BibliografiaBasica { get; set; } = string.Empty;

    public string BibliografiaComplementaria { get; set; } = string.Empty;

    public string PerfilDocente { get; set; } = string.Empty;

    public string? PathPUA { get; set; } = string.Empty;

    public bool EstadoMateria { get; set; } = true;

    // Colección para la relación muchos a muchos a través de la tabla intermedia
    public ICollection<E_PlanEstudioMateria> PlanesEstudioMaterias { get; set; } = [];
}
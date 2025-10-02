/*
    Aqui lo que estaremos realizando sera la representacion de la tabla PlanEstudio declarando
    la navegacion de propiedades con E_Carrera Carrera para permitir navegar a los obbjetos relacionados.
*/

using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Entidades.Modelos.PlanesDeEstudio.NivelesAcademicos;
using Entidades.Modelos.PlanesDeEstudio.PlanEstudioMaterias;

namespace Entidades.Modelos.PlanesDeEstudio.PlanEstudios;

public class E_PlanEstudio
{
    public int IdPlanEstudio { get; set; }

    public int IdNivelAcademico { get; set; }

    // Foreign Key
    public int IdCarrera { get; set; }

    public string PlanEstudio { get; set; } = string.Empty;

    public DateTime FechaCreacion { get; set; }

    public int TotalCreditos { get; set; }

    public int CreditosOptativos { get; set; }

    public int CreditosObligatorios { get; set; }

    public string PerfilDeIngreso { get; set; } = string.Empty;

    public string PerfilDeEgreso { get; set; } = string.Empty;

    public string CampoOcupacional { get; set; } = string.Empty;

    public string Comentarios { get; set; } = string.Empty;

    public bool EstadoPlanEstudio { get; set; } = false;

    // Navegacion properties
    public E_Carrera Carrera { get; set; } = new E_Carrera();

    public E_NivelAcademico NivelAcademico { get; set; } = new E_NivelAcademico();

    // Colección para la relación muchos a muchos a través de la tabla intermedia
    public ICollection<E_PlanEstudioMateria> PlanEstudioMaterias { get; set; } = [];

}
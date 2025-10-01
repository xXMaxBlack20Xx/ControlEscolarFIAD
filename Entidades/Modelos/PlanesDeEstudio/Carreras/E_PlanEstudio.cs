/*
    Aqui lo que estaremos realizando sera la representacion de la tabla PlanEstudio declarando
    la navegacion de propiedades con E_Carrera Carrera para permitir navegar a los obbjetos relacionados.
*/

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades.Modelos.PlanesDeEstudio.Carreras;

public class E_PlanEstudio
{
    [Key]
    public int IdPlanEstudio { get; set; }

    [Required]
    public int IdNivelAcademico { get; set; }

    // Foreign Key
    [Required]
    public int IdCarrera { get; set; }

    [Required]
    [MaxLength(50)]
    public string PlanEstudio { get; set; } = string.Empty;

    [Required]
    public DateTime FechaCreacion { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int TotalCreditos { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int CreditosOptativos { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int CreditosObligatorios { get; set; }

    [Required]
    [MaxLength(50)]
    public string PerfilDeIngreso { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string PerfilDeEgreso { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string CampoOcupacional { get; set; } = string.Empty;

    [MaxLength(250)]
    public string Comentarios { get; set; } = string.Empty;

    [Required]
    public bool EstadoPlanEstudio { get; set; } = false;

    // Navegacion properties
    [ForeignKey(nameof(IdCarrera))]
    public E_Carrera Carrera { get; set; } = new E_Carrera();

    [ForeignKey(nameof(IdNivelAcademico))]
    public E_NivelAcademico NivelAcademico { get; set; } = new E_NivelAcademico();

    // Futura implementacion
    //public E_NivelAcademico NivelAcademico { get; set; }
    //public ICollection<E_PlanEstudioMateria>? PlanEstudioMaterias { get; set; }
}
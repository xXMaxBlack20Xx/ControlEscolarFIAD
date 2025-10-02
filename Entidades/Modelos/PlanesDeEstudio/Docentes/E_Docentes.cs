﻿/*
 Este seria el buffet de maestros que se pueden asignar a las carreras
 */

using Entidades.Modelos.PlanesDeEstudio.Carreras;
using System.ComponentModel.DataAnnotations;

namespace Entidades.Modelos.PlanesDeEstudio.Docentes;

public class E_Docentes
{
    public int? IdDocente { get; set; }

    public string NumeroEmpleado { get; set; } = string.Empty;

    public string NombreDocente { get; set; } = string.Empty;

    public string PaternoDocente { get; set; } = string.Empty;

    public string MaternoDocente { get; set; } = string.Empty;

    public string EmailAlterno { get; set; } = string.Empty;

    public bool EstadoDocente { get; set; } = true;
    
    // Propiedad de navegacion inversa
    public ICollection<E_Carrera> CarrerasCoordinadas { get; set; } = new List<E_Carrera>();
}
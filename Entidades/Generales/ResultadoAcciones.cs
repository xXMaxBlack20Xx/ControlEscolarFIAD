using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Generales;

public class ResultadoAcciones
{
    public List<string> Mensajes { get; set; } = [];

    public bool Resultado { get; set; } = true;

    public static ResultadoAcciones Exitoso(string mensaje)
    {
        return new ResultadoAcciones
        {
            Resultado = true,
            Mensajes = new List<string> { mensaje }
        };
    }

    public static ResultadoAcciones Fallido(string mensaje)
    {
        return new ResultadoAcciones
        {
            Resultado = false,
            Mensajes = new List<string> { mensaje }
        };
    }
}

public class ResultadoAccion<T> : ResultadoAcciones
{
    public T? Data { get; set; }

    public required object? Entidad { get; set; }
}
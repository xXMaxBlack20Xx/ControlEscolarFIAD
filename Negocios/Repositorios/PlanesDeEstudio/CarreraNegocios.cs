using Datos.IRepositorios.PlanesDeEstudio;
using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.Carreras;

namespace Negocios.Repositorios.PlanesDeEstudio;

public class CarreraNegocios
{
    private readonly ICarreraRepositorios _carreraRepositorio;

    public CarreraNegocios(ICarreraRepositorios carreraRepositorio)
    {
        _carreraRepositorio = carreraRepositorio;
    }

    // ================== CASOS DE USO (Sin cambios) ==================

    public async Task<ResultadoAcciones> InsertarCarrera(E_Carrera carrera)
    {
        if (carrera == null)
        {
            return new ResultadoAcciones { Resultado = false, Mensajes = { "La carrera no tiene los datos necesarios para agregarla al sistema." } };
        }
        var resultadoValidacion = await ValidarCarrera(carrera, esModificacion: false);
        if (!resultadoValidacion.Resultado) return resultadoValidacion;
        try
        {
            return await _carreraRepositorio.InsertarCarrera(carrera);
        }
        catch (Exception ex)
        {
            return new ResultadoAcciones { Resultado = false, Mensajes = { "Ocurrió un error inesperado al insertar la carrera.", ex.Message } };
        }
    }

    public async Task<ResultadoAcciones> ModificarCarrera(E_Carrera carrera)
    {
        if (carrera == null || carrera.IdCarrera <= 0)
        {
            return new ResultadoAcciones { Resultado = false, Mensajes = { "Los datos de la carrera son inválidos." } };
        }
        var resultadoValidacion = await ValidarCarrera(carrera, esModificacion: true);
        if (!resultadoValidacion.Resultado) return resultadoValidacion;
        try
        {
            return await _carreraRepositorio.ModificarCarrera(carrera);
        }
        catch (Exception ex)
        {
            return new ResultadoAcciones { Resultado = false, Mensajes = { "Ocurrió un error inesperado al modificar la carrera.", ex.Message } };
        }
    }

    // ... (Borrar, ObtenerPorId, Listar, etc. no cambian)
    public async Task<ResultadoAcciones> BorrarCarrera(int idCarrera)
    {
        if (idCarrera <= 0)
        {
            return new ResultadoAcciones { Resultado = false, Mensajes = { "El identificador de la carrera no es válido." } };
        }
        try
        {
            return await _carreraRepositorio.BorrarCarrera(idCarrera);
        }
        catch (Exception ex)
        {
            return new ResultadoAcciones { Resultado = false, Mensajes = { "Ocurrió un error inesperado al eliminar la carrera.", ex.Message } };
        }
    }
    public async Task<ResultadoAccion<E_Carrera>> ObtenerCarreraPorId(int idCarrera)
    {
        if (idCarrera <= 0)
        {
            return new ResultadoAccion<E_Carrera> { Resultado = false, Entidad = null, Mensajes = { "El identificador de la carrera no es válido." } };
        }
        try
        {
            var entidad = await _carreraRepositorio.BuscarCarrera(idCarrera);
            if (entidad is null)
            {
                return new ResultadoAccion<E_Carrera> { Resultado = false, Entidad = null, Mensajes = { $"No se encontró la carrera con ID {idCarrera}." } };
            }
            return new ResultadoAccion<E_Carrera> { Resultado = true, Entidad = entidad };
        }
        catch (Exception ex)
        {
            return new ResultadoAccion<E_Carrera> { Resultado = false, Entidad = null, Mensajes = { "Ocurrió un error inesperado al obtener la carrera.", ex.Message } };
        }
    }
    public async Task<IEnumerable<E_Carrera>> ListarCarreras(string? criterioBusqueda = null)
    {
        try
        {
            var criterio = string.IsNullOrWhiteSpace(criterioBusqueda) ? null : criterioBusqueda.Trim();
            return await _carreraRepositorio.ListarCarreras(criterio);
        }
        catch { return Array.Empty<E_Carrera>(); }
    }
    public async Task<IEnumerable<E_Carrera>> ListarCarrerasPorPlanEstudio(int? idPlanEstudio = null)
    {
        try
        {
            if (idPlanEstudio is null || idPlanEstudio <= 0)
                return await _carreraRepositorio.ListarCarreras();
            return await _carreraRepositorio.ListarCarrerasPorPlanEstudio(idPlanEstudio.Value);
        }
        catch { return Array.Empty<E_Carrera>(); }
    }

    // ============== VALIDACIONES DE NEGOCIO (CORREGIDAS) ==============

    public async Task<ResultadoAcciones> ValidarCarrera(E_Carrera carrera, bool esModificacion = false)
    {
        var resultado = new ResultadoAcciones { Resultado = true };

        ValidarClaveCarrera(resultado, carrera.ClaveCarrera);
        ValidarNombreCarrera(resultado, carrera.NombreCarrera);
        ValidarAliasCarrera(resultado, carrera.AliasCarrera);
        if (!resultado.Resultado) return resultado;

        if (esModificacion)
        {
            await ValidarUnicidadNombreCarrera(resultado, carrera.NombreCarrera, carrera.IdCarrera);
            await ValidarUnicidadAliasCarrera(resultado, carrera.AliasCarrera, carrera.IdCarrera);
        }
        else
        {
            await ValidarUnicidadNombreCarrera(resultado, carrera.NombreCarrera);
            await ValidarUnicidadAliasCarrera(resultado, carrera.AliasCarrera);
        }
        resultado.Resultado = resultado.Mensajes.Count == 0;
        return resultado;
    }

    // --------- reglas básicas (sin cambios) ---------
    private static void ValidarClaveCarrera(ResultadoAcciones res, string? claveCarrera)
    {
        if (string.IsNullOrWhiteSpace(claveCarrera))
        {
            res.Resultado = false;
            res.Mensajes.Add("La clave de la carrera es obligatoria.");
            return;
        }
        var clave = claveCarrera.Trim();
        if (clave.Length != 3 || !clave.All(char.IsLetter))
        {
            res.Resultado = false;
            res.Mensajes.Add("La clave de la carrera debe tener exactamente 3 letras (A–Z).");
        }
    }
    private static void ValidarNombreCarrera(ResultadoAcciones res, string? nombreCarrera)
    {
        if (string.IsNullOrWhiteSpace(nombreCarrera))
        {
            res.Resultado = false;
            res.Mensajes.Add("El nombre de la carrera es obligatorio.");
            return;
        }
        if (nombreCarrera.Trim().Length > 50)
        {
            res.Resultado = false;
            res.Mensajes.Add("El nombre de la carrera no debe exceder 50 caracteres.");
        }
    }
    private static void ValidarAliasCarrera(ResultadoAcciones res, string? aliasCarrera)
    {
        if (string.IsNullOrWhiteSpace(aliasCarrera))
        {
            res.Resultado = false;
            res.Mensajes.Add("El alias de la carrera es obligatorio.");
            return;
        }
        if (aliasCarrera.Trim().Length > 50)
        {
            res.Resultado = false;
            res.Mensajes.Add("El alias de la carrera no debe exceder 50 caracteres.");
        }
    }

    // --------- reglas de unicidad (CORREGIDAS) ---------

    private async Task ValidarUnicidadNombreCarrera(ResultadoAcciones res, string? nombreCarrera, int? idExcluido = null)
    {
        if (string.IsNullOrWhiteSpace(nombreCarrera)) return;

        var nombre = nombreCarrera.Trim();
        bool existe = await _carreraRepositorio.ExisteNombreCarrera(nombre); // Devuelve bool
        if (!existe) return; // Si no existe, no hay nada más que validar.

        // Si existe, debemos verificar si es el mismo registro que estamos editando.
        if (idExcluido.HasValue)
        {
            var actual = await _carreraRepositorio.BuscarCarrera(idExcluido.Value);
            // Si el nombre encontrado pertenece al registro actual, no es un error.
            if (actual != null && string.Equals(actual.NombreCarrera?.Trim(), nombre, StringComparison.OrdinalIgnoreCase))
                return;
        }

        // Si llegamos aquí, significa que el nombre existe y pertenece a OTRO registro.
        res.Resultado = false;
        res.Mensajes.Add("El nombre de la carrera ya existe.");
    }

    private async Task ValidarUnicidadAliasCarrera(ResultadoAcciones res, string? aliasCarrera, int? idExcluido = null)
    {
        if (string.IsNullOrWhiteSpace(aliasCarrera)) return;

        var alias = aliasCarrera.Trim();
        bool existe = await _carreraRepositorio.ExisteAliasCarrera(alias); // Devuelve bool
        if (!existe) return;

        if (idExcluido.HasValue)
        {
            var actual = await _carreraRepositorio.BuscarCarrera(idExcluido.Value);
            if (actual != null && string.Equals(actual.AliasCarrera?.Trim(), alias, StringComparison.OrdinalIgnoreCase))
                return;
        }

        res.Resultado = false;
        res.Mensajes.Add("El alias de la carrera ya existe.");
    }
}
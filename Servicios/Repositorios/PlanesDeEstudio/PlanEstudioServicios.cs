using AutoMapper;
using Entidades.DTO.PlanesDeEstudio.PlanEstudios;
using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Negocios.Repositorios.PlanesDeEstudio;
using Servicios.IRepositorios.PlanesDeEstudio;

namespace Servicios.Repositorios.PlanesDeEstudio;
public class PlanEstudioServicios : IPlanEstudioServicios
{
    private readonly PlanEstudioNegocios _negocios;
    private readonly IMapper _mapper;

    // Inyectamos las dependencias (capa de negocio y automapper)
    public PlanEstudioServicios(PlanEstudioNegocios negocios, IMapper mapper)
    {
        _negocios = negocios;
        _mapper = mapper;
    }

    public async Task<ResultadoAcciones> InsertarPlanEstudio(PlanEstudioDTO dto)
    {
        if (dto is null)
            return new ResultadoAcciones { Resultado = false, Mensajes = { "Los datos del plan de estudio son requeridos." } };

        Normalizar(dto);
        var entidad = _mapper.Map<E_PlanEstudio>(dto);
        return await _negocios.InsertarPlanEstudio(entidad);
    }

    public async Task<ResultadoAcciones> ModificarPlanEstudio(PlanEstudioDTO dto)
    {
        // La validación del ID debe ser en el DTO, ya que es un dato de entrada.
        if (dto is null || dto.IdPlanEstudio <= 0)
            return new ResultadoAcciones { Resultado = false, Mensajes = { "Los datos del plan de estudio son inválidos." } };

        Normalizar(dto);
        var entidad = _mapper.Map<E_PlanEstudio>(dto);
        return await _negocios.ModificarPlanEstudio(entidad);
    }

    public Task<ResultadoAcciones> BorrarPlanEstudio(int idPlanEstudio)
    {
        if (idPlanEstudio <= 0)
            return Task.FromResult(new ResultadoAcciones { Resultado = false, Mensajes = { "El ID del plan de estudio es inválido." } });

        return _negocios.BorrarPlanEstudio(idPlanEstudio);
    }

    public async Task<ResultadoAccion<PlanEstudioDTO>> ObtenerPlanEstudio(int idPlanEstudio)
    {
        var r = await _negocios.ObtenerPlanEstudioPorId(idPlanEstudio);
        if (!r.Resultado || r.Entidad is null)
            return new ResultadoAccion<PlanEstudioDTO> { Resultado = false, Mensajes = r.Mensajes, Entidad = null };

        var dto = _mapper.Map<PlanEstudioDTO>(r.Entidad);
        return new ResultadoAccion<PlanEstudioDTO> { Resultado = true, Mensajes = r.Mensajes, Entidad = dto };
    }

    public async Task<ResultadoAccion<T>> ObtenerPlanEstudio<T>(int idPlanEstudio) where T : class
    {
        var r = await _negocios.ObtenerPlanEstudioPorId(idPlanEstudio);
        if (!r.Resultado || r.Entidad is null)
            return new ResultadoAccion<T> { Resultado = false, Mensajes = r.Mensajes, Entidad = default };

        return new ResultadoAccion<T>
        {
            Resultado = true,
            Mensajes = r.Mensajes,
            Entidad = _mapper.Map<T>(r.Entidad)
        };
    }

    public async Task<IEnumerable<PlanEstudioDTO>> ListarPlanesEstudio(string? criterioBusqueda = null)
    {
        var entidades = await _negocios.ListarPlanesEstudio(criterioBusqueda);
        return _mapper.Map<IEnumerable<PlanEstudioDTO>>(entidades);
    }

    public async Task<List<PlanEstudioDTO>> ListarPlanesPorCarreraAsync(int idCarrera)
    {
        if (idCarrera <= 0) return new List<PlanEstudioDTO>();
        var entidades = await _negocios.ListarPlanesPorCarrera(idCarrera);
        return _mapper.Map<List<PlanEstudioDTO>>(entidades);
    }

    // --- helpers ---
    // El DTO para PlanEstudio no parece tener un IdPlanEstudio de tipo string,
    // así que he ajustado la normalización.
    private static void Normalizar(PlanEstudioDTO d)
    {
        d.PlanEstudio = d.PlanEstudio?.Trim() ?? string.Empty;
        d.PerfilDeEgreso = d.PerfilDeEgreso?.Trim() ?? string.Empty;
        d.PerfilDeIngreso = d.PerfilDeIngreso?.Trim() ?? string.Empty;
        d.CampoOcupacional = d.CampoOcupacional?.Trim() ?? string.Empty;
    }
}
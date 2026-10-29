using Abstracciones.Agentes;
using Abstracciones.Excepciones;

namespace Flujo.Agentes;

public class AgenteFlujo : IAgenteFlujo
{
    private readonly IAgenteDa _da;
    private readonly TimeProvider _reloj;
    
    public AgenteFlujo(IAgenteDa da, TimeProvider reloj)
    {
        _da = da;
        _reloj = reloj;
    }

    public async Task<AgenteDto> Crear(AgenteCrearDto dto, CancellationToken ct = default)
    {
        var agente = new Agente
        {
            Nombre = dto.Nombre.Trim(),
            Correo = dto.Correo.Trim().ToLowerInvariant(),
            Telefono = dto.Telefono.Trim(),
            CantidadTickets = dto.CantidadTickets,
            Activo = dto.Activo,
            FechaCreacion = dto.FechaCreacion ?? _reloj.GetUtcNow().UtcDateTime
        };
        return await Crear(agente, ct);
    }

    public async Task<AgenteDto> Crear(Agente agente, CancellationToken ct = default)
    {
        var correo = agente.Correo.Trim().ToLowerInvariant();
        if (await _da.ObtenerPorCorreoAsync(correo, ct) is not null)
            throw new ConflictoException($"Ya existe un agente con el correo '{correo}'.");        
        
        var nuevo = new Agente
        {
            Nombre = agente.Nombre.Trim(),
            Correo = correo,
            Telefono = agente.Telefono.Trim(),
            CantidadTickets = agente.CantidadTickets,
            Activo = agente.Activo,
            FechaCreacion = agente.FechaCreacion != default ? agente.FechaCreacion : _reloj.GetUtcNow().UtcDateTime,
        };
        
        await _da.Crear(nuevo, ct);
        return AgenteMapeador.ADto(nuevo);
    }

    public async Task<bool> Actualizar(int id, AgenteActualizarDto dto, CancellationToken ct = default)
    {
        var agenteDto = new AgenteDto(
            id,
            dto.Nombre.Trim(),
            dto.Correo.Trim().ToLowerInvariant(),
            dto.Telefono.Trim(),
            dto.CantidadTickets,
            dto.Activo,
            dto.FechaCreacion ?? _reloj.GetUtcNow().UtcDateTime
        );
        return await Actualizar(id, agenteDto, ct);
    }

    public async Task<bool> Actualizar(int id, AgenteDto agente, CancellationToken ct = default)
    {
        var correo = agente.Correo.Trim().ToLowerInvariant();

        _ = await _da.ObtenerPorId(id, ct) ?? throw new NoEncontradoException("Agente", id);

        var otro = await _da.ObtenerPorCorreoAsync(correo, ct);
        
        if (otro is not null && otro.AgenteId != id)
            throw new ConflictoException($"Ya existe un agente con el correo '{correo}'.");

        var actualizado = await _da.Actualizar(id, new Agente
        {
            AgenteId = id,
            Nombre = agente.Nombre.Trim(),
            Correo = correo,
            Telefono = agente.Telefono.Trim(),
            CantidadTickets = agente.CantidadTickets,
            Activo = agente.Activo
        }, ct);
        if (!actualizado) throw new NoEncontradoException("Agente", id);
        return true;
    }

    public async Task<AgenteDto> ObtenerPorId(int id, CancellationToken ct = default)
    {
        var agente = await _da.ObtenerPorId(id, ct)
                     ?? throw new NoEncontradoException("Agente", id);
        return AgenteMapeador.ADto(agente);
    }

    public async Task<List<AgenteDto>> ObtenerTodos(CancellationToken ct = default)
    {
        var agentes = await _da.ObtenerTodos(ct);
        return agentes.Select(AgenteMapeador.ADto).ToList();
    }

    public async Task<bool> Eliminar(int id, CancellationToken ct = default)
    {
        _ = await _da.ObtenerPorId(id, ct) ?? throw new NoEncontradoException("Agente", id);
        var eliminado = await _da.Eliminar(id, ct);
        if (!eliminado) throw new NoEncontradoException("Agente", id);
        return true;
    }
}

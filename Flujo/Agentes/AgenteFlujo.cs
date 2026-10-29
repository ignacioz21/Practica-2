using Abstracciones.Agentes;
using Abstracciones.Excepciones;
using Flujo.Clientes;

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

    public async Task<AgenteDto> Crear(Agente agente, CancellationToken ct)
    {
        var correo = agente.Correo.Trim().ToLowerInvariant();
        if (await _da.ObtenerPorCorreoAsync(correo, ct) is not null)
            throw new ConflictoException($"Ya existe un cliente con el correo '{correo}'.");        
        
        agente = new Agente
        {
            Nombre = agente.Nombre,
            Correo = correo,
            Telefono = agente.Telefono,
            CantidadTickets = agente.CantidadTickets,
            Activo = agente.Activo,
            FechaCreacion = _reloj.GetUtcNow().UtcDateTime,
        };
        
        await _da.Crear(agente, ct);
        return AgenteMapeador.ADto(agente);
    }

    public async Task<bool> Actualizar(int id, AgenteDto agente, CancellationToken ct)
    {
        var correo = agente.Correo.Trim().ToLowerInvariant();

        _ = await _da.ObtenerPorId(id, ct) ?? throw new NoEncontradoException($"No se encontró el agente con id '{id}'.", id);

        var otro = await _da.ObtenerPorCorreoAsync(correo, ct);
        
        if (otro is not null && otro.AgenteId != id)
            throw new ConflictoException($"Ya existe un agente con el correo '{correo}'.");

        var actualizado = await _da.Actualizar(id, new Agente
        {
            AgenteId = id,
            Nombre = agente.Nombre,
            Correo = correo,
            Telefono = agente.Telefono,
            CantidadTickets = agente.CantidadTickets,
            Activo = agente.Activo
        }, ct);
        if (!actualizado) throw new NoEncontradoException("Cliente", id);
        return true;
    }

    public async Task<AgenteDto> ObtenerPorId(int id, CancellationToken ct)
    {
        var agente = await _da.ObtenerPorId(id, ct)
                     ?? throw new NoEncontradoException("Cliente", id);
        return AgenteMapeador.ADto(agente);
    }
        

    public Task<List<Agente>> ObtenerTodos(CancellationToken ct)
    {
        return _da.ObtenerTodos(ct);
    }

    public async Task<bool> Eliminar(int id, CancellationToken ct)
    {
        _ = await _da.ObtenerPorId(id, ct) ?? throw new NoEncontradoException("Cliente", id);
        return true;
    }
}
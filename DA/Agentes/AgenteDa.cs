using Abstracciones.Agentes;
using Abstracciones.Excepciones;
using DA.Comunes;
using DA.Contexto;
using Microsoft.EntityFrameworkCore;

namespace DA.Agentes;

public class AgenteDa : IAgenteDa
{
    private readonly SistemaSoporteContexto _contexto;
    
    public AgenteDa(SistemaSoporteContexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<Agente> Crear(Agente agente, CancellationToken ct)
    {
        _contexto.Agentes.Add(agente);
        try
        {
            await _contexto.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex) when (ErroresSql.EsDuplicado(ex))
        {
            throw new ConflictoException("Ya existe un agente con el mismo nombre");
        }
        return agente;
    }

    public async Task<bool> Actualizar(int id, Agente agente, CancellationToken ct)
    {
        var agenteBuscar = await _contexto.Agentes.FirstOrDefaultAsync(a => a.AgenteId == id, ct);
        if (agenteBuscar is null) return false;
        
        agenteBuscar.Nombre = agente.Nombre;
        agenteBuscar.Correo = agente.Correo;
        agenteBuscar.CantidadTickets = agente.CantidadTickets;
        agenteBuscar.Activo = agente.Activo;

        try
        {
            await _contexto.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex) when (ErroresSql.EsDuplicado(ex))
        {
            throw new ConflictoException("Ya existe un agente con el mismo nombre");
        }
        return true;
    }

    public async Task<Agente?> ObtenerPorId(int id, CancellationToken ct)
    {
        try
        {
            return await _contexto.Agentes.AsNoTracking().FirstOrDefaultAsync(a => a.AgenteId == id, ct);
        }
        catch
        {
            throw new ConflictoException("Ya existe un agente con el mismo nombre");
            
        }
    }

    public async Task<List<Agente>> ObtenerTodos(CancellationToken ct)
    {
        return await _contexto.Agentes.AsNoTracking().ToListAsync(ct);
    }

    public async Task<bool> Eliminar(int id, CancellationToken ct)
    {
        var agenteBuscar = await _contexto.Agentes.FirstOrDefaultAsync(a => a.AgenteId == id, ct);
        if (agenteBuscar is null) return false;

        _contexto.Agentes.Remove(agenteBuscar);
        try
        {
            await _contexto.SaveChangesAsync(ct);
        }
        catch
        {
            throw new ConflictoException("Error al eliminar el agente");
        }
        return true;
    }

    public Task<Agente?> ObtenerPorCorreoAsync(string correo, CancellationToken ct) =>
        _contexto.Agentes.AsNoTracking().FirstOrDefaultAsync(c => c.Correo == correo, ct);
}
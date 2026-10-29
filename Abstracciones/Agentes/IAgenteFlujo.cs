namespace Abstracciones.Agentes;

public interface IAgenteFlujo
{
    Task<AgenteDto> Crear(Agente agente, CancellationToken ct);
    Task<bool> Actualizar(int id, AgenteDto agente, CancellationToken ct);
    Task<AgenteDto> ObtenerPorId(int id, CancellationToken ct);
    Task<List<Agente>> ObtenerTodos(CancellationToken ct);
    Task<bool> Eliminar(int id, CancellationToken ct);
}
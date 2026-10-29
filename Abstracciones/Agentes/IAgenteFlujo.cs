namespace Abstracciones.Agentes;

public interface IAgenteFlujo
{
    Task<AgenteDto> Crear(AgenteCrearDto dto, CancellationToken ct = default);
    Task<AgenteDto> Crear(Agente agente, CancellationToken ct = default);
    Task<bool> Actualizar(int id, AgenteActualizarDto dto, CancellationToken ct = default);
    Task<bool> Actualizar(int id, AgenteDto agente, CancellationToken ct = default);
    Task<AgenteDto> ObtenerPorId(int id, CancellationToken ct = default);
    Task<List<AgenteDto>> ObtenerTodos(CancellationToken ct = default);
    Task<bool> Eliminar(int id, CancellationToken ct = default);
}

namespace Abstracciones.Agentes;

public interface IAgenteDa
{
    Task<Agente> Crear(Agente agente, CancellationToken ct);
    Task<bool> Actualizar(int id, Agente agente, CancellationToken ct);
    Task<Agente?> ObtenerPorId(int id, CancellationToken ct);
    Task<List<Agente>> ObtenerTodos(CancellationToken ct);
    Task<bool> Eliminar(int id, CancellationToken ct);
    Task<Agente?> ObtenerPorCorreoAsync(string correo, CancellationToken ct);
     
}
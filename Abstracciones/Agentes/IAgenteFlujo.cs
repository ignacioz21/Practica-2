namespace Abstracciones.Agentes;

public interface IAgenteFlujo
{
    Task<AgenteDto> Crear(AgenteCrearDto agente);
    Task<bool> Actualizar(int id, AgenteActualizarDto agente);
    Task<AgenteDto> ObtenerPorId(int id);
    Task<List<AgenteDto>> ObtenerTodos();
    Task<bool> Eliminar(int id);
}
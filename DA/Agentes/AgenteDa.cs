using Abstracciones.Agentes;

namespace DA.Agentes;

public class AgenteDa : IAgenteDa
{
    public Task<AgenteDto> Crear(AgenteCrearDto agente)
    {
        
    }

    public Task<bool> Actualizar(int id, AgenteActualizarDto agente)
    {
        throw new NotImplementedException();
    }

    public Task<AgenteDto> ObtenerPorId(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<AgenteDto>> ObtenerTodos()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Eliminar(int id)
    {
        throw new NotImplementedException();
    }
}
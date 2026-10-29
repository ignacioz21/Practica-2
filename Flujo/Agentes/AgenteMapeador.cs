using Abstracciones.Agentes;

namespace Flujo.Agentes;

public class AgenteMapeador
{
    public static AgenteDto ADto(Agente a) =>
        new(a.Nombre, a.Correo, a.Telefono, a.CantidadTickets, a.Activo, a.FechaCreacion);
}
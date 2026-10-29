namespace Abstracciones.Agentes;

public class Agente
{
    public int AgenteId { get; set; }
    public required string Nombre { get; set; }
    public string Correo { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public int CantidadTickets { get; set; } = 0;
    public bool Activo { get; set; } = true;
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
}
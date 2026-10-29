using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Agentes;

public record AgenteDto(
    string Nombre,
    string Correo,
    string Telefono,
    int CantidadTickets
);

public class AgenteCrearDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
    [StringLength(150)]
    public string Correo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [RegularExpression(@"^\d{8}$", ErrorMessage = "El teléfono debe tener exactamente 8 dígitos.")]
    public string Telefono { get; set; } = string.Empty;

    [Range(0, 50, ErrorMessage = "La cantidad de tickets debe estar entre 0 y 50")]
    public int CantidadTickets { get; set; } = 0;
}

public class AgenteActualizarDto : AgenteCrearDto { }
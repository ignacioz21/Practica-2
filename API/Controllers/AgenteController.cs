using Abstracciones.Agentes;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/agentes")]
public class AgenteController : ControllerBase
{
    private readonly IAgenteFlujo _flujo;

    public AgenteController(IAgenteFlujo flujo)
    {
        _flujo = flujo;
    }

    [HttpGet]
    [ProducesResponseType<List<AgenteDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<AgenteDto>>> ObtenerTodos(CancellationToken ct)
    {
        var agentes = await _flujo.ObtenerTodos(ct);
        return Ok(agentes);
    }

    [HttpGet("{id:int:min(1)}")]
    [ProducesResponseType<AgenteDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AgenteDto>> ObtenerPorId(int id, CancellationToken ct) =>
        Ok(await _flujo.ObtenerPorId(id, ct));

    [HttpPost]
    [ProducesResponseType<AgenteDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<AgenteDto>> Crear(AgenteCrearDto dto, CancellationToken ct)
    {
        var creado = await _flujo.Crear(dto, ct);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
    }

    [HttpPut("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Actualizar(int id, AgenteActualizarDto dto, CancellationToken ct)
    {
        await _flujo.Actualizar(id, dto, ct);
        return NoContent();
    }

    [HttpDelete("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Eliminar(int id, CancellationToken ct)
    {
        await _flujo.Eliminar(id, ct);
        return NoContent();
    }
}

using Api.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Models;
using Modelos.ModelsDTO.NecesidadEvento;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NecesidadEventoController : ControllerBase
{
    private readonly INecesidadEventoRepository _repository;
    private readonly IMapper _mapper;

    public NecesidadEventoController(INecesidadEventoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var necesidad = await _repository.GetByIdAsync(id);
        if (necesidad == null)
            return NotFound(new { Message = "Necesidad de evento no encontrada." });

        var dto = _mapper.Map<NecesidadEventoResponseDTO>(necesidad);
        return Ok(dto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var necesidades = await _repository.GetAllAsync();
        var dtos = _mapper.Map<List<NecesidadEventoResponseDTO>>(necesidades);
        return Ok(dtos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] NecesidadEventoCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var necesidad = _mapper.Map<NecesidadEvento>(dto);
        await _repository.CreateAsync(necesidad);

        var response = _mapper.Map<NecesidadEventoResponseDTO>(necesidad);
        return CreatedAtAction(nameof(Get), new { id = necesidad.NecesidadID }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] NecesidadEventoUpdateDTO dto)
    {
        if (id != dto.NecesidadID)
            return BadRequest(new { Message = "El ID no coincide." });

        var necesidad = await _repository.GetByIdAsync(id);
        if (necesidad == null)
            return NotFound(new { Message = "Necesidad de evento no encontrada." });

        _mapper.Map(dto, necesidad);
        await _repository.UpdateAsync(necesidad);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var necesidad = await _repository.GetByIdAsync(id);
        if (necesidad == null)
            return NotFound(new { Message = "Necesidad de evento no encontrada." });

        await _repository.DeleteAsync(necesidad);
        return NoContent();
    }

    [HttpGet("by-evento/{eventoId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByEvento(int eventoId)
    {
        var necesidades = await _repository.GetByEventoAsync(eventoId);
        var dtos = _mapper.Map<List<NecesidadEventoResponseDTO>>(necesidades);
        return Ok(dtos);
    }

    [HttpGet("by-subcategoria/{subcategoriaId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBySubcategoria(int subcategoriaId)
    {
        var necesidades = await _repository.GetBySubcategoriaAsync(subcategoriaId);
        var dtos = _mapper.Map<List<NecesidadEventoResponseDTO>>(necesidades);
        return Ok(dtos);
    }
}

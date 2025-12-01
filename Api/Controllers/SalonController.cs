using Api.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Models;
using Modelos.ModelsDTO.Salon;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SalonController : ControllerBase
{
    private readonly ISalonRepository _repository;
    private readonly IMapper _mapper;

    public SalonController(ISalonRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var salon = await _repository.GetByIdAsync(id);
        if (salon == null)
            return NotFound(new { Message = "Salón no encontrado." });

        var dto = _mapper.Map<SalonResponseDTO>(salon);
        return Ok(dto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var salones = await _repository.GetAllAsync();
        var dtos = _mapper.Map<List<SalonResponseDTO>>(salones);
        return Ok(dtos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] SalonCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var salon = _mapper.Map<Salon>(dto);
        await _repository.CreateAsync(salon);

        var response = _mapper.Map<SalonResponseDTO>(salon);
        return CreatedAtAction(nameof(Get), new { id = salon.SalonID }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] SalonUpdateDTO dto)
    {
        if (id != dto.SalonID)
            return BadRequest(new { Message = "El ID no coincide." });

        var salon = await _repository.GetByIdAsync(id);
        if (salon == null)
            return NotFound(new { Message = "Salón no encontrado." });

        _mapper.Map(dto, salon);
        await _repository.UpdateAsync(salon);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var salon = await _repository.GetByIdAsync(id);
        if (salon == null)
            return NotFound(new { Message = "Salón no encontrado." });

        await _repository.DeleteAsync(salon);
        return NoContent();
    }

    [HttpGet("by-sede/{sedeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBySede(int sedeId)
    {
        var salones = await _repository.GetBySedeAsync(sedeId);
        var dtos = _mapper.Map<List<SalonResponseDTO>>(salones);
        return Ok(dtos);
    }

    [HttpGet("by-capacidad-minima/{capacidad}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByCapacidadMinima(int capacidad)
    {
        var salones = await _repository.GetByCapacidadMinimaAsync(capacidad);
        var dtos = _mapper.Map<List<SalonResponseDTO>>(salones);
        return Ok(dtos);
    }
}

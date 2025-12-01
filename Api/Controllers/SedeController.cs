using Api.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Models;
using Modelos.ModelsDTO.Sede;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SedeController : ControllerBase
{
    private readonly ISedeRepository _repository;
    private readonly IMapper _mapper;

    public SedeController(ISedeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var sede = await _repository.GetByIdAsync(id);
        if (sede == null)
            return NotFound(new { Message = "Sede no encontrada." });

        var dto = _mapper.Map<SedeResponseDTO>(sede);
        return Ok(dto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var sedes = await _repository.GetAllAsync();
        var dtos = _mapper.Map<List<SedeResponseDTO>>(sedes);
        return Ok(dtos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] SedeCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var sede = _mapper.Map<Sede>(dto);
        await _repository.CreateAsync(sede);

        var response = _mapper.Map<SedeResponseDTO>(sede);
        return CreatedAtAction(nameof(Get), new { id = sede.SedeID }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] SedeUpdateDTO dto)
    {
        if (id != dto.SedeID)
            return BadRequest(new { Message = "El ID no coincide." });

        var sede = await _repository.GetByIdAsync(id);
        if (sede == null)
            return NotFound(new { Message = "Sede no encontrada." });

        _mapper.Map(dto, sede);
        await _repository.UpdateAsync(sede);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var sede = await _repository.GetByIdAsync(id);
        if (sede == null)
            return NotFound(new { Message = "Sede no encontrada." });

        await _repository.DeleteAsync(sede);
        return NoContent();
    }

    [HttpGet("by-municipio/{municipioId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByMunicipio(int municipioId)
    {
        var sedes = await _repository.GetByMunicipioAsync(municipioId);
        var dtos = _mapper.Map<List<SedeResponseDTO>>(sedes);
        return Ok(dtos);
    }

    [HttpGet("with-salones")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllWithSalones()
    {
        var sedes = await _repository.GetAllWithSalonesAsync();
        var dtos = _mapper.Map<List<SedeResponseDTO>>(sedes);
        return Ok(dtos);
    }
}

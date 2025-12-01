using Api.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Models;
using Modelos.ModelsDTO.Municipio;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MunicipioController : ControllerBase
{
    private readonly IMunicipioRepository _repository;
    private readonly IMapper _mapper;

    public MunicipioController(IMunicipioRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var municipio = await _repository.GetByIdAsync(id);
        if (municipio == null)
            return NotFound(new { Message = "Municipio no encontrado." });

        var dto = _mapper.Map<MunicipioResponseDTO>(municipio);
        return Ok(dto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var municipios = await _repository.GetAllAsync();
        var dtos = _mapper.Map<List<MunicipioResponseDTO>>(municipios);
        return Ok(dtos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] MunicipioCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var municipio = _mapper.Map<Municipio>(dto);
        await _repository.CreateAsync(municipio);

        var response = _mapper.Map<MunicipioResponseDTO>(municipio);
        return CreatedAtAction(nameof(Get), new { id = municipio.MunicipioID }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] MunicipioUpdateDTO dto)
    {
        if (id != dto.MunicipioID)
            return BadRequest(new { Message = "El ID no coincide." });

        var municipio = await _repository.GetByIdAsync(id);
        if (municipio == null)
            return NotFound(new { Message = "Municipio no encontrado." });

        _mapper.Map(dto, municipio);
        await _repository.UpdateAsync(municipio);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var municipio = await _repository.GetByIdAsync(id);
        if (municipio == null)
            return NotFound(new { Message = "Municipio no encontrado." });

        await _repository.DeleteAsync(municipio);
        return NoContent();
    }

    [HttpGet("by-departamento/{departamentoId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByDepartamento(int departamentoId)
    {
        var municipios = await _repository.GetByDepartamentoAsync(departamentoId);
        var dtos = _mapper.Map<List<MunicipioResponseDTO>>(municipios);
        return Ok(dtos);
    }

    [HttpGet("by-nombre/{nombre}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByNombre(string nombre)
    {
        var municipio = await _repository.GetByNombreAsync(nombre);
        if (municipio == null)
            return NotFound(new { Message = "Municipio no encontrado." });

        var dto = _mapper.Map<MunicipioResponseDTO>(municipio);
        return Ok(dto);
    }
}

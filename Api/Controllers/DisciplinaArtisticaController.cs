using Api.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Models;
using Modelos.ModelsDTO.DisciplinaArtistica;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DisciplinaArtisticaController : ControllerBase
{
    private readonly IDisciplinaArtisticaRepository _repository;
    private readonly IMapper _mapper;

    public DisciplinaArtisticaController(IDisciplinaArtisticaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var disciplina = await _repository.GetByIdAsync(id);
        if (disciplina == null)
            return NotFound(new { Message = "Disciplina artística no encontrada." });

        var dto = _mapper.Map<DisciplinaArtisticaResponseDTO>(disciplina);
        return Ok(dto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var disciplinas = await _repository.GetAllAsync();
        var dtos = _mapper.Map<List<DisciplinaArtisticaResponseDTO>>(disciplinas);
        return Ok(dtos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] DisciplinaArtisticaCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existe = await _repository.ExistsAsync(d => d.Nombre == dto.Nombre);
        if (existe)
            return Conflict(new { Message = "Ya existe una disciplina artística con ese nombre." });

        var disciplina = _mapper.Map<DisciplinaArtistica>(dto);
        await _repository.CreateAsync(disciplina);

        var response = _mapper.Map<DisciplinaArtisticaResponseDTO>(disciplina);
        return CreatedAtAction(nameof(Get), new { id = disciplina.DisciplinaID }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] DisciplinaArtisticaUpdateDTO dto)
    {
        if (id != dto.DisciplinaID)
            return BadRequest(new { Message = "El ID no coincide." });

        var disciplina = await _repository.GetByIdAsync(id);
        if (disciplina == null)
            return NotFound(new { Message = "Disciplina artística no encontrada." });

        _mapper.Map(dto, disciplina);
        await _repository.UpdateAsync(disciplina);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var disciplina = await _repository.GetByIdAsync(id);
        if (disciplina == null)
            return NotFound(new { Message = "Disciplina artística no encontrada." });

        await _repository.DeleteAsync(disciplina);
        return NoContent();
    }

    [HttpGet("by-nombre/{nombre}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByNombre(string nombre)
    {
        var disciplina = await _repository.GetByNombreAsync(nombre);
        if (disciplina == null)
            return NotFound(new { Message = "Disciplina artística no encontrada." });

        var dto = _mapper.Map<DisciplinaArtisticaResponseDTO>(disciplina);
        return Ok(dto);
    }
}

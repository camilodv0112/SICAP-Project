using Api.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Models;
using Modelos.ModelsDTO.Profesor;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProfesorController : ControllerBase
{
    private readonly IProfesorRepository _repository;
    private readonly IMapper _mapper;

    public ProfesorController(IProfesorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var profesor = await _repository.GetByIdAsync(id);
        if (profesor == null)
            return NotFound(new { Message = "Profesor no encontrado." });

        var dto = _mapper.Map<ProfesorResponseDTO>(profesor);
        return Ok(dto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var profesores = await _repository.GetAllAsync();
        var dtos = _mapper.Map<List<ProfesorResponseDTO>>(profesores);
        return Ok(dtos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] ProfesorCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var profesor = _mapper.Map<Profesor>(dto);
        await _repository.CreateAsync(profesor);

        var response = _mapper.Map<ProfesorResponseDTO>(profesor);
        return CreatedAtAction(nameof(Get), new { id = profesor.ProfesorID }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] ProfesorUpdateDTO dto)
    {
        if (id != dto.ProfesorID)
            return BadRequest(new { Message = "El ID no coincide." });

        var profesor = await _repository.GetByIdAsync(id);
        if (profesor == null)
            return NotFound(new { Message = "Profesor no encontrado." });

        _mapper.Map(dto, profesor);
        await _repository.UpdateAsync(profesor);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var profesor = await _repository.GetByIdAsync(id);
        if (profesor == null)
            return NotFound(new { Message = "Profesor no encontrado." });

        await _repository.DeleteAsync(profesor);
        return NoContent();
    }

    [HttpGet("by-asignatura/{asignatura}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByAsignatura(string asignatura)
    {
        var profesores = await _repository.GetByAsignaturaAsync(asignatura);
        var dtos = _mapper.Map<List<ProfesorResponseDTO>>(profesores);
        return Ok(dtos);
    }

    [HttpGet("{id}/with-estudiantes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWithEstudiantes(int id)
    {
        var profesor = await _repository.GetWithEstudiantesAsync(id);
        if (profesor == null)
            return NotFound(new { Message = "Profesor no encontrado." });

        var dto = _mapper.Map<ProfesorResponseDTO>(profesor);
        return Ok(dto);
    }
}

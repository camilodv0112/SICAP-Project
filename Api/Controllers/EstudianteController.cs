using Api.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Models;
using Modelos.ModelsDTO.Estudiante;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EstudianteController : ControllerBase
{
    private readonly IEstudianteRepository _repository;
    private readonly IMapper _mapper;

    public EstudianteController(IEstudianteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var estudiante = await _repository.GetByIdAsync(id);
        if (estudiante == null)
            return NotFound(new { Message = "Estudiante no encontrado." });

        var dto = _mapper.Map<EstudianteResponseDTO>(estudiante);
        return Ok(dto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var estudiantes = await _repository.GetAllAsync();
        var dtos = _mapper.Map<List<EstudianteResponseDTO>>(estudiantes);
        return Ok(dtos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] EstudianteCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existeCedula = await _repository.ExistsAsync(e => e.Cedula == dto.Cedula);
        if (existeCedula)
            return Conflict(new { Message = "Ya existe un estudiante con esa cédula." });

        var existeCarnet = await _repository.ExistsAsync(e => e.Carnet == dto.Carnet);
        if (existeCarnet)
            return Conflict(new { Message = "Ya existe un estudiante con ese carnet." });

        var estudiante = _mapper.Map<Estudiante>(dto);
        await _repository.CreateAsync(estudiante);

        var response = _mapper.Map<EstudianteResponseDTO>(estudiante);
        return CreatedAtAction(nameof(Get), new { id = estudiante.EstudianteID }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] EstudianteUpdateDTO dto)
    {
        if (id != dto.EstudianteID)
            return BadRequest(new { Message = "El ID no coincide." });

        var estudiante = await _repository.GetByIdAsync(id);
        if (estudiante == null)
            return NotFound(new { Message = "Estudiante no encontrado." });

        _mapper.Map(dto, estudiante);
        await _repository.UpdateAsync(estudiante);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var estudiante = await _repository.GetByIdAsync(id);
        if (estudiante == null)
            return NotFound(new { Message = "Estudiante no encontrado." });

        await _repository.DeleteAsync(estudiante);
        return NoContent();
    }

    [HttpGet("by-cedula/{cedula}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByCedula(string cedula)
    {
        var estudiante = await _repository.GetByCedulaAsync(cedula);
        if (estudiante == null)
            return NotFound(new { Message = "Estudiante no encontrado." });

        var dto = _mapper.Map<EstudianteResponseDTO>(estudiante);
        return Ok(dto);
    }

    [HttpGet("by-carnet/{carnet}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByCarnet(string carnet)
    {
        var estudiante = await _repository.GetByCarnetAsync(carnet);
        if (estudiante == null)
            return NotFound(new { Message = "Estudiante no encontrado." });

        var dto = _mapper.Map<EstudianteResponseDTO>(estudiante);
        return Ok(dto);
    }

    [HttpGet("by-carrera/{carreraId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByCarrera(int carreraId)
    {
        var estudiantes = await _repository.GetByCarreraAsync(carreraId);
        var dtos = _mapper.Map<List<EstudianteResponseDTO>>(estudiantes);
        return Ok(dtos);
    }

    [HttpGet("by-disciplina/{disciplinaId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByDisciplina(int disciplinaId)
    {
        var estudiantes = await _repository.GetByDisciplinaAsync(disciplinaId);
        var dtos = _mapper.Map<List<EstudianteResponseDTO>>(estudiantes);
        return Ok(dtos);
    }

    [HttpGet("{id}/with-profesores")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWithProfesores(int id)
    {
        var estudiante = await _repository.GetWithProfesoresAsync(id);
        if (estudiante == null)
            return NotFound(new { Message = "Estudiante no encontrado." });

        var dto = _mapper.Map<EstudianteResponseDTO>(estudiante);
        return Ok(dto);
    }
}

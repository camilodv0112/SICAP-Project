using Api.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Models;
using Modelos.ModelsDTO.Carrera;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CarreraController : ControllerBase
{
    private readonly ICarreraRepository _repository;
    private readonly IMapper _mapper;

    public CarreraController(ICarreraRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var carrera = await _repository.GetByIdAsync(id);
        if (carrera == null)
            return NotFound(new { Message = "Carrera no encontrada." });

        var dto = _mapper.Map<CarreraResponseDTO>(carrera);
        return Ok(dto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var carreras = await _repository.GetAllAsync();
        var dtos = _mapper.Map<List<CarreraResponseDTO>>(carreras);
        return Ok(dtos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CarreraCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existe = await _repository.ExistsAsync(c => c.Nombre == dto.Nombre);
        if (existe)
            return Conflict(new { Message = "Ya existe una carrera con ese nombre." });

        var carrera = _mapper.Map<Carrera>(dto);
        await _repository.CreateAsync(carrera);

        var response = _mapper.Map<CarreraResponseDTO>(carrera);
        return CreatedAtAction(nameof(Get), new { id = carrera.CarreraID }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] CarreraUpdateDTO dto)
    {
        if (id != dto.CarreraID)
            return BadRequest(new { Message = "El ID no coincide." });

        var carrera = await _repository.GetByIdAsync(id);
        if (carrera == null)
            return NotFound(new { Message = "Carrera no encontrada." });

        _mapper.Map(dto, carrera);
        await _repository.UpdateAsync(carrera);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var carrera = await _repository.GetByIdAsync(id);
        if (carrera == null)
            return NotFound(new { Message = "Carrera no encontrada." });

        await _repository.DeleteAsync(carrera);
        return NoContent();
    }

    [HttpGet("by-nombre/{nombre}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByNombre(string nombre)
    {
        var carrera = await _repository.GetByNombreAsync(nombre);
        if (carrera == null)
            return NotFound(new { Message = "Carrera no encontrada." });

        var dto = _mapper.Map<CarreraResponseDTO>(carrera);
        return Ok(dto);
    }

    [HttpGet("with-estudiantes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllWithEstudiantes()
    {
        var carreras = await _repository.GetAllWithEstudiantesAsync();
        var dtos = _mapper.Map<List<CarreraResponseDTO>>(carreras);
        return Ok(dtos);
    }
}

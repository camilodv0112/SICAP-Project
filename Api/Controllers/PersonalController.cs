using Api.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Models;
using Modelos.ModelsDTO.Personal;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PersonalController : ControllerBase
{
    private readonly IPersonalRepository _repository;
    private readonly IMapper _mapper;

    public PersonalController(IPersonalRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var personal = await _repository.GetByIdAsync(id);
        if (personal == null)
            return NotFound(new { Message = "Personal no encontrado." });

        var dto = _mapper.Map<PersonalResponseDTO>(personal);
        return Ok(dto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var personal = await _repository.GetAllAsync();
        var dtos = _mapper.Map<List<PersonalResponseDTO>>(personal);
        return Ok(dtos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] PersonalCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existeCedula = await _repository.ExistsAsync(p => p.Cedula == dto.Cedula);
        if (existeCedula)
            return Conflict(new { Message = "Ya existe personal con esa cédula." });

        if (!string.IsNullOrEmpty(dto.NumeroEmpleado))
        {
            var existeNumero = await _repository.ExistsAsync(p => p.NumeroEmpleado == dto.NumeroEmpleado);
            if (existeNumero)
                return Conflict(new { Message = "Ya existe personal con ese número de empleado." });
        }

        var personal = _mapper.Map<Personal>(dto);
        await _repository.CreateAsync(personal);

        var response = _mapper.Map<PersonalResponseDTO>(personal);
        return CreatedAtAction(nameof(Get), new { id = personal.PersonalID }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] PersonalUpdateDTO dto)
    {
        if (id != dto.PersonalID)
            return BadRequest(new { Message = "El ID no coincide." });

        var personal = await _repository.GetByIdAsync(id);
        if (personal == null)
            return NotFound(new { Message = "Personal no encontrado." });

        _mapper.Map(dto, personal);
        await _repository.UpdateAsync(personal);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var personal = await _repository.GetByIdAsync(id);
        if (personal == null)
            return NotFound(new { Message = "Personal no encontrado." });

        await _repository.DeleteAsync(personal);
        return NoContent();
    }

    [HttpGet("by-cedula/{cedula}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByCedula(string cedula)
    {
        var personal = await _repository.GetByCedulaAsync(cedula);
        if (personal == null)
            return NotFound(new { Message = "Personal no encontrado." });

        var dto = _mapper.Map<PersonalResponseDTO>(personal);
        return Ok(dto);
    }

    [HttpGet("by-numero-empleado/{numeroEmpleado}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByNumeroEmpleado(string numeroEmpleado)
    {
        var personal = await _repository.GetByNumeroEmpleadoAsync(numeroEmpleado);
        if (personal == null)
            return NotFound(new { Message = "Personal no encontrado." });

        var dto = _mapper.Map<PersonalResponseDTO>(personal);
        return Ok(dto);
    }

    [HttpGet("by-cargo/{cargoId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByCargo(int cargoId)
    {
        var personal = await _repository.GetByCargoAsync(cargoId);
        var dtos = _mapper.Map<List<PersonalResponseDTO>>(personal);
        return Ok(dtos);
    }

    [HttpGet("by-disciplina/{disciplinaId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByDisciplina(int disciplinaId)
    {
        var personal = await _repository.GetByDisciplinaAsync(disciplinaId);
        var dtos = _mapper.Map<List<PersonalResponseDTO>>(personal);
        return Ok(dtos);
    }
}

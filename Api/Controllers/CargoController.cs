using Api.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Models;
using Modelos.ModelsDTO.Cargo;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CargoController : ControllerBase
{
    private readonly ICargoRepository _repository;
    private readonly IMapper _mapper;

    public CargoController(ICargoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var cargo = await _repository.GetByIdAsync(id);
        if (cargo == null)
            return NotFound(new { Message = "Cargo no encontrado." });

        var dto = _mapper.Map<CargoResponseDTO>(cargo);
        return Ok(dto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var cargos = await _repository.GetAllAsync();
        var dtos = _mapper.Map<List<CargoResponseDTO>>(cargos);
        return Ok(dtos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CargoCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existe = await _repository.ExistsAsync(c => c.Nombre == dto.Nombre);
        if (existe)
            return Conflict(new { Message = "Ya existe un cargo con ese nombre." });

        var cargo = _mapper.Map<Cargo>(dto);
        await _repository.CreateAsync(cargo);

        var response = _mapper.Map<CargoResponseDTO>(cargo);
        return CreatedAtAction(nameof(Get), new { id = cargo.CargoID }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] CargoUpdateDTO dto)
    {
        if (id != dto.CargoID)
            return BadRequest(new { Message = "El ID no coincide." });

        var cargo = await _repository.GetByIdAsync(id);
        if (cargo == null)
            return NotFound(new { Message = "Cargo no encontrado." });

        _mapper.Map(dto, cargo);
        await _repository.UpdateAsync(cargo);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var cargo = await _repository.GetByIdAsync(id);
        if (cargo == null)
            return NotFound(new { Message = "Cargo no encontrado." });

        await _repository.DeleteAsync(cargo);
        return NoContent();
    }

    [HttpGet("by-nombre/{nombre}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByNombre(string nombre)
    {
        var cargo = await _repository.GetByNombreAsync(nombre);
        if (cargo == null)
            return NotFound(new { Message = "Cargo no encontrado." });

        var dto = _mapper.Map<CargoResponseDTO>(cargo);
        return Ok(dto);
    }
}

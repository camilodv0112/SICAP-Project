using Api.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Models;
using Modelos.ModelsDTO.EstadoUsuario;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EstadoUsuarioController : ControllerBase
{
    private readonly IEstadoUsuarioRepository _repository;
    private readonly IMapper _mapper;

    public EstadoUsuarioController(IEstadoUsuarioRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var estado = await _repository.GetByIdAsync(id);
        if (estado == null)
            return NotFound(new { Message = "Estado de usuario no encontrado." });

        var dto = _mapper.Map<EstadoUsuarioResponseDTO>(estado);
        return Ok(dto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var estados = await _repository.GetAllAsync();
        var dtos = _mapper.Map<List<EstadoUsuarioResponseDTO>>(estados);
        return Ok(dtos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] EstadoUsuarioCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existe = await _repository.ExistsAsync(e => e.Nombre == dto.Nombre);
        if (existe)
            return Conflict(new { Message = "Ya existe un estado de usuario con ese nombre." });

        var estado = _mapper.Map<EstadoUsuario>(dto);
        await _repository.CreateAsync(estado);

        var response = _mapper.Map<EstadoUsuarioResponseDTO>(estado);
        return CreatedAtAction(nameof(Get), new { id = estado.EstadoID }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] EstadoUsuarioUpdateDTO dto)
    {
        if (id != dto.EstadoID)
            return BadRequest(new { Message = "El ID no coincide." });

        var estado = await _repository.GetByIdAsync(id);
        if (estado == null)
            return NotFound(new { Message = "Estado de usuario no encontrado." });

        _mapper.Map(dto, estado);
        await _repository.UpdateAsync(estado);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var estado = await _repository.GetByIdAsync(id);
        if (estado == null)
            return NotFound(new { Message = "Estado de usuario no encontrado." });

        await _repository.DeleteAsync(estado);
        return NoContent();
    }

    [HttpGet("by-nombre/{nombre}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByNombre(string nombre)
    {
        var estado = await _repository.GetByNombreAsync(nombre);
        if (estado == null)
            return NotFound(new { Message = "Estado de usuario no encontrado." });

        var dto = _mapper.Map<EstadoUsuarioResponseDTO>(estado);
        return Ok(dto);
    }
}

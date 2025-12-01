using Api.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Models;
using Modelos.ModelsDTO.CartaGenerada;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CartaGeneradaController : ControllerBase
{
    private readonly ICartaGeneradaRepository _repository;
    private readonly IMapper _mapper;

    public CartaGeneradaController(ICartaGeneradaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var carta = await _repository.GetByIdAsync(id);
        if (carta == null)
            return NotFound(new { Message = "Carta generada no encontrada." });

        var dto = _mapper.Map<CartaGeneradaResponseDTO>(carta);
        return Ok(dto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var cartas = await _repository.GetAllAsync();
        var dtos = _mapper.Map<List<CartaGeneradaResponseDTO>>(cartas);
        return Ok(dtos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CartaGeneradaCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var carta = _mapper.Map<CartaGenerada>(dto);
        await _repository.CreateAsync(carta);

        var response = _mapper.Map<CartaGeneradaResponseDTO>(carta);
        return CreatedAtAction(nameof(Get), new { id = carta.Id }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] CartaGeneradaUpdateDTO dto)
    {
        if (id != dto.Id)
            return BadRequest(new { Message = "El ID no coincide." });

        var carta = await _repository.GetByIdAsync(id);
        if (carta == null)
            return NotFound(new { Message = "Carta generada no encontrada." });

        _mapper.Map(dto, carta);
        await _repository.UpdateAsync(carta);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var carta = await _repository.GetByIdAsync(id);
        if (carta == null)
            return NotFound(new { Message = "Carta generada no encontrada." });

        await _repository.DeleteAsync(carta);
        return NoContent();
    }

    [HttpGet("by-plantilla/{plantillaId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByPlantilla(int plantillaId)
    {
        var cartas = await _repository.GetByPlantillaAsync(plantillaId);
        var dtos = _mapper.Map<List<CartaGeneradaResponseDTO>>(cartas);
        return Ok(dtos);
    }

    [HttpGet("by-usuario/{usuarioPersonalId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUsuario(int usuarioPersonalId)
    {
        var cartas = await _repository.GetByUsuarioAsync(usuarioPersonalId);
        var dtos = _mapper.Map<List<CartaGeneradaResponseDTO>>(cartas);
        return Ok(dtos);
    }

    [HttpGet("by-fecha/{fecha}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByFecha(DateTime fecha)
    {
        var cartas = await _repository.GetByFechaAsync(fecha);
        var dtos = _mapper.Map<List<CartaGeneradaResponseDTO>>(cartas);
        return Ok(dtos);
    }
}

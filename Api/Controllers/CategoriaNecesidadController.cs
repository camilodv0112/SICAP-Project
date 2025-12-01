using Api.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Models;
using Modelos.ModelsDTO.CategoriaNecesidad;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoriaNecesidadController : ControllerBase
{
    private readonly ICategoriaNecesidadRepository _repository;
    private readonly IMapper _mapper;

    public CategoriaNecesidadController(ICategoriaNecesidadRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var categoria = await _repository.GetByIdAsync(id);
        if (categoria == null)
            return NotFound(new { Message = "Categoría de necesidad no encontrada." });

        var dto = _mapper.Map<CategoriaNecesidadResponseDTO>(categoria);
        return Ok(dto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var categorias = await _repository.GetAllAsync();
        var dtos = _mapper.Map<List<CategoriaNecesidadResponseDTO>>(categorias);
        return Ok(dtos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CategoriaNecesidadCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existe = await _repository.ExistsAsync(c => c.Nombre == dto.Nombre);
        if (existe)
            return Conflict(new { Message = "Ya existe una categoría de necesidad con ese nombre." });

        var categoria = _mapper.Map<CategoriaNecesidad>(dto);
        await _repository.CreateAsync(categoria);

        var response = _mapper.Map<CategoriaNecesidadResponseDTO>(categoria);
        return CreatedAtAction(nameof(Get), new { id = categoria.CategoriaID }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] CategoriaNecesidadUpdateDTO dto)
    {
        if (id != dto.CategoriaID)
            return BadRequest(new { Message = "El ID no coincide." });

        var categoria = await _repository.GetByIdAsync(id);
        if (categoria == null)
            return NotFound(new { Message = "Categoría de necesidad no encontrada." });

        _mapper.Map(dto, categoria);
        await _repository.UpdateAsync(categoria);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var categoria = await _repository.GetByIdAsync(id);
        if (categoria == null)
            return NotFound(new { Message = "Categoría de necesidad no encontrada." });

        await _repository.DeleteAsync(categoria);
        return NoContent();
    }

    [HttpGet("by-nombre/{nombre}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByNombre(string nombre)
    {
        var categoria = await _repository.GetByNombreAsync(nombre);
        if (categoria == null)
            return NotFound(new { Message = "Categoría de necesidad no encontrada." });

        var dto = _mapper.Map<CategoriaNecesidadResponseDTO>(categoria);
        return Ok(dto);
    }

    [HttpGet("with-subcategorias")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllWithSubcategorias()
    {
        var categorias = await _repository.GetAllWithSubcategoriasAsync();
        var dtos = _mapper.Map<List<CategoriaNecesidadResponseDTO>>(categorias);
        return Ok(dtos);
    }
}

using Api.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Models;
using Modelos.ModelsDTO.SubcategoriaNecesidad;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SubcategoriaNecesidadController : ControllerBase
{
    private readonly ISubcategoriaNecesidadRepository _repository;
    private readonly IMapper _mapper;

    public SubcategoriaNecesidadController(ISubcategoriaNecesidadRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var subcategoria = await _repository.GetByIdAsync(id);
        if (subcategoria == null)
            return NotFound(new { Message = "Subcategoría de necesidad no encontrada." });

        var dto = _mapper.Map<SubcategoriaNecesidadResponseDTO>(subcategoria);
        return Ok(dto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var subcategorias = await _repository.GetAllAsync();
        var dtos = _mapper.Map<List<SubcategoriaNecesidadResponseDTO>>(subcategorias);
        return Ok(dtos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] SubcategoriaNecesidadCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var subcategoria = _mapper.Map<SubcategoriaNecesidad>(dto);
        await _repository.CreateAsync(subcategoria);

        var response = _mapper.Map<SubcategoriaNecesidadResponseDTO>(subcategoria);
        return CreatedAtAction(nameof(Get), new { id = subcategoria.SubcategoriaID }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] SubcategoriaNecesidadUpdateDTO dto)
    {
        if (id != dto.SubcategoriaID)
            return BadRequest(new { Message = "El ID no coincide." });

        var subcategoria = await _repository.GetByIdAsync(id);
        if (subcategoria == null)
            return NotFound(new { Message = "Subcategoría de necesidad no encontrada." });

        _mapper.Map(dto, subcategoria);
        await _repository.UpdateAsync(subcategoria);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var subcategoria = await _repository.GetByIdAsync(id);
        if (subcategoria == null)
            return NotFound(new { Message = "Subcategoría de necesidad no encontrada." });

        await _repository.DeleteAsync(subcategoria);
        return NoContent();
    }

    [HttpGet("by-categoria/{categoriaId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByCategoria(int categoriaId)
    {
        var subcategorias = await _repository.GetByCategoriaAsync(categoriaId);
        var dtos = _mapper.Map<List<SubcategoriaNecesidadResponseDTO>>(subcategorias);
        return Ok(dtos);
    }
}

using Api.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Models;
using Modelos.ModelsDTO.Departamento;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DepartamentoController : ControllerBase
{
    private readonly IDepartamentoRepository _repository;
    private readonly IMapper _mapper;

    public DepartamentoController(IDepartamentoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var departamento = await _repository.GetByIdAsync(id);
        if (departamento == null)
            return NotFound(new { Message = "Departamento no encontrado." });

        var dto = _mapper.Map<DepartamentoResponseDTO>(departamento);
        return Ok(dto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var departamentos = await _repository.GetAllAsync();
        var dtos = _mapper.Map<List<DepartamentoResponseDTO>>(departamentos);
        return Ok(dtos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] DepartamentoCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existe = await _repository.ExistsAsync(d => d.Nombre == dto.Nombre);
        if (existe)
            return Conflict(new { Message = "Ya existe un departamento con ese nombre." });

        var departamento = _mapper.Map<Departamento>(dto);
        await _repository.CreateAsync(departamento);

        var response = _mapper.Map<DepartamentoResponseDTO>(departamento);
        return CreatedAtAction(nameof(Get), new { id = departamento.DepartamentoID }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] DepartamentoUpdateDTO dto)
    {
        if (id != dto.DepartamentoID)
            return BadRequest(new { Message = "El ID no coincide." });

        var departamento = await _repository.GetByIdAsync(id);
        if (departamento == null)
            return NotFound(new { Message = "Departamento no encontrado." });

        _mapper.Map(dto, departamento);
        await _repository.UpdateAsync(departamento);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var departamento = await _repository.GetByIdAsync(id);
        if (departamento == null)
            return NotFound(new { Message = "Departamento no encontrado." });

        await _repository.DeleteAsync(departamento);
        return NoContent();
    }

    [HttpGet("by-nombre/{nombre}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByNombre(string nombre)
    {
        var departamento = await _repository.GetByNombreAsync(nombre);
        if (departamento == null)
            return NotFound(new { Message = "Departamento no encontrado." });

        var dto = _mapper.Map<DepartamentoResponseDTO>(departamento);
        return Ok(dto);
    }

    [HttpGet("with-municipios")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllWithMunicipios()
    {
        var departamentos = await _repository.GetAllWithMunicipiosAsync();
        var dtos = _mapper.Map<List<DepartamentoResponseDTO>>(departamentos);
        return Ok(dtos);
    }
}

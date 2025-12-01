using Api.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Models;
using Modelos.ModelsDTO.PlantillaCarta;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PlantillaCartaController : ControllerBase
{
    private readonly IPlantillaCartaRepository _repository;
    private readonly IMapper _mapper;

    public PlantillaCartaController(IPlantillaCartaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var plantilla = await _repository.GetByIdAsync(id);
        if (plantilla == null)
            return NotFound(new { Message = "Plantilla de carta no encontrada." });

        var dto = _mapper.Map<PlantillaCartaResponseDTO>(plantilla);
        return Ok(dto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var plantillas = await _repository.GetAllAsync();
        var dtos = _mapper.Map<List<PlantillaCartaResponseDTO>>(plantillas);
        return Ok(dtos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] PlantillaCartaCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existe = await _repository.ExistsAsync(p => p.Nombre == dto.Nombre);
        if (existe)
            return Conflict(new { Message = "Ya existe una plantilla de carta con ese nombre." });

        var plantilla = _mapper.Map<PlantillaCarta>(dto);
        await _repository.CreateAsync(plantilla);

        var response = _mapper.Map<PlantillaCartaResponseDTO>(plantilla);
        return CreatedAtAction(nameof(Get), new { id = plantilla.Id }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] PlantillaCartaUpdateDTO dto)
    {
        if (id != dto.Id)
            return BadRequest(new { Message = "El ID no coincide." });

        var plantilla = await _repository.GetByIdAsync(id);
        if (plantilla == null)
            return NotFound(new { Message = "Plantilla de carta no encontrada." });

        _mapper.Map(dto, plantilla);
        await _repository.UpdateAsync(plantilla);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var plantilla = await _repository.GetByIdAsync(id);
        if (plantilla == null)
            return NotFound(new { Message = "Plantilla de carta no encontrada." });

        await _repository.DeleteAsync(plantilla);
        return NoContent();
    }

    [HttpGet("by-nombre/{nombre}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByNombre(string nombre)
    {
        var plantilla = await _repository.GetByNombreAsync(nombre);
        if (plantilla == null)
            return NotFound(new { Message = "Plantilla de carta no encontrada." });

        var dto = _mapper.Map<PlantillaCartaResponseDTO>(plantilla);
        return Ok(dto);
    }
}

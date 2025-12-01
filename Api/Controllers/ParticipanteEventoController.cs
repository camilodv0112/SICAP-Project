using Api.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Models;
using Modelos.ModelsDTO.ParticipanteEvento;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ParticipanteEventoController : ControllerBase
{
    private readonly IParticipanteEventoRepository _repository;
    private readonly IMapper _mapper;

    public ParticipanteEventoController(IParticipanteEventoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] ParticipanteEventoCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var participante = _mapper.Map<ParticipanteEvento>(dto);
        await _repository.CreateAsync(participante);

        // Fetch the full entity with navigation properties to avoid NullReferenceException in AutoMapper
        var fullParticipante = await _repository.GetAsync(
            p => p.EventoID == participante.EventoID && p.EstudianteID == participante.EstudianteID,
            includeProperties: "Estudiante"
        );

        var response = _mapper.Map<ParticipanteEventoResponseDTO>(fullParticipante);
        return Ok(response); // Returning OK instead of CreatedAtAction because of composite key complexity for now
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var participantes = await _repository.GetAllAsync();
        var dtos = _mapper.Map<List<ParticipanteEventoResponseDTO>>(participantes);
        return Ok(dtos);
    }
}

using Api.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Models;
using Modelos.ModelsDTO.Evento;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EventoController : ControllerBase
{
    private readonly IEventoRepository _repository;
    private readonly INecesidadEventoRepository _necesidadRepository;
    private readonly IParticipanteEventoRepository _participanteRepository;
    private readonly IResponsableEventoRepository _responsableRepository;
    private readonly IMapper _mapper;

    public EventoController(
        IEventoRepository repository,
        INecesidadEventoRepository necesidadRepository,
        IParticipanteEventoRepository participanteRepository,
        IResponsableEventoRepository responsableRepository,
        IMapper mapper)
    {
        _repository = repository;
        _necesidadRepository = necesidadRepository;
        _participanteRepository = participanteRepository;
        _responsableRepository = responsableRepository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var evento = await _repository.GetByIdAsync(id);
        if (evento == null)
            return NotFound(new { Message = "Evento no encontrado." });

        var dto = _mapper.Map<EventoResponseDTO>(evento);
        return Ok(dto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var eventos = await _repository.GetAllAsync();
        var dtos = _mapper.Map<List<EventoResponseDTO>>(eventos);
        return Ok(dtos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] EventoCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var evento = _mapper.Map<Evento>(dto);
        await _repository.CreateAsync(evento);

        var response = _mapper.Map<EventoResponseDTO>(evento);
        return CreatedAtAction(nameof(Get), new { id = evento.EventoID }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] EventoUpdateDTO dto)
    {
        if (id != dto.EventoID)
            return BadRequest(new { Message = "El ID no coincide." });

        var evento = await _repository.GetByIdAsync(id);
        if (evento == null)
            return NotFound(new { Message = "Evento no encontrado." });

        _mapper.Map(dto, evento);
        await _repository.UpdateAsync(evento);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var evento = await _repository.GetByIdAsync(id);
        if (evento == null)
            return NotFound(new { Message = "Evento no encontrado." });

        // 1. Eliminar Necesidades
        var necesidades = await _necesidadRepository.GetAllAsync(n => n.EventoID == id);
        foreach (var necesidad in necesidades)
        {
            await _necesidadRepository.DeleteAsync(necesidad);
        }

        // 2. Eliminar Participantes
        var participantes = await _participanteRepository.GetAllAsync(p => p.EventoID == id);
        foreach (var participante in participantes)
        {
            await _participanteRepository.DeleteAsync(participante);
        }

        // 3. Eliminar Responsables
        var responsables = await _responsableRepository.GetAllAsync(r => r.EventoID == id);
        foreach (var responsable in responsables)
        {
            await _responsableRepository.DeleteAsync(responsable);
        }

        // 4. Eliminar Evento
        await _repository.DeleteAsync(evento);
        return NoContent();
    }

    [HttpGet("by-salon/{salonId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBySalon(int salonId)
    {
        var eventos = await _repository.GetBySalonAsync(salonId);
        var dtos = _mapper.Map<List<EventoResponseDTO>>(eventos);
        return Ok(dtos);
    }

    [HttpGet("by-fecha/{fecha}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByFecha(DateTime fecha)
    {
        var eventos = await _repository.GetByFechaAsync(fecha);
        var dtos = _mapper.Map<List<EventoResponseDTO>>(eventos);
        return Ok(dtos);
    }

    [HttpGet("{id}/with-participantes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWithParticipantes(int id)
    {
        var evento = await _repository.GetWithParticipantesAsync(id);
        if (evento == null)
            return NotFound(new { Message = "Evento no encontrado." });

        var dto = _mapper.Map<EventoResponseDTO>(evento);
        return Ok(dto);
    }

    [HttpGet("{id}/with-responsables")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWithResponsables(int id)
    {
        var evento = await _repository.GetWithResponsablesAsync(id);
        if (evento == null)
            return NotFound(new { Message = "Evento no encontrado." });

        var dto = _mapper.Map<EventoResponseDTO>(evento);
        return Ok(dto);
    }
}

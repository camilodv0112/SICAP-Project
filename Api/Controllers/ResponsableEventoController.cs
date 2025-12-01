using Api.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Models;
using Modelos.ModelsDTO.ResponsableEvento;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ResponsableEventoController : ControllerBase
{
    private readonly IResponsableEventoRepository _repository;
    private readonly IMapper _mapper;

    public ResponsableEventoController(IResponsableEventoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] ResponsableEventoCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var responsable = _mapper.Map<ResponsableEvento>(dto);
        await _repository.CreateAsync(responsable);

        // Fetch the full entity with navigation properties to avoid NullReferenceException in AutoMapper
        var fullResponsable = await _repository.GetAsync(
            r => r.EventoID == responsable.EventoID && r.PersonalID == responsable.PersonalID,
            includeProperties: "Personal"
        );

        var response = _mapper.Map<ResponsableEventoResponseDTO>(fullResponsable);
        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var responsables = await _repository.GetAllAsync();
        var dtos = _mapper.Map<List<ResponsableEventoResponseDTO>>(responsables);
        return Ok(dtos);
    }
}

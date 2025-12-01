using Api.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Models;
using Modelos.ModelsDTO.UsuarioPersonal;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsuarioPersonalController : ControllerBase
{
    private readonly IUsuarioPersonalRepository _repository;
    private readonly IMapper _mapper;

    public UsuarioPersonalController(IUsuarioPersonalRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario == null)
            return NotFound(new { Message = "Usuario de personal no encontrado." });

        var dto = _mapper.Map<UsuarioPersonalResponseDTO>(usuario);
        return Ok(dto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var usuarios = await _repository.GetAllAsync();
        var dtos = _mapper.Map<List<UsuarioPersonalResponseDTO>>(usuarios);
        return Ok(dtos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] UsuarioPersonalCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existe = await _repository.ExistsAsync(u => u.Usuario == dto.Usuario);
        if (existe)
            return Conflict(new { Message = "Ya existe un usuario con ese nombre de usuario." });

        var usuario = _mapper.Map<UsuarioPersonal>(dto);
        usuario.Contraseña = HashPassword(dto.Contraseña);

        await _repository.CreateAsync(usuario);
        var response = _mapper.Map<UsuarioPersonalResponseDTO>(usuario);
        return CreatedAtAction(nameof(Get), new { id = usuario.UsuarioPID }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UsuarioPersonalUpdateDTO dto)
    {
        if (id != dto.UsuarioPID)
            return BadRequest(new { Message = "El ID no coincide." });

        var usuario = await _repository.GetByIdAsync(id);
        if (usuario == null)
            return NotFound(new { Message = "Usuario de personal no encontrado." });

        _mapper.Map(dto, usuario);

        if (!string.IsNullOrEmpty(dto.Contraseña))
            usuario.Contraseña = HashPassword(dto.Contraseña);

        await _repository.UpdateAsync(usuario);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario == null)
            return NotFound(new { Message = "Usuario de personal no encontrado." });

        await _repository.DeleteAsync(usuario);
        return NoContent();
    }

    private byte[] HashPassword(string password)
    {
        using var sha = System.Security.Cryptography.SHA256.Create();
        return sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    [HttpGet("by-usuario/{usuario}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByUsuario(string usuario)
    {
        var usuarioPersonal = await _repository.GetByUsuarioAsync(usuario);
        if (usuarioPersonal == null)
            return NotFound(new { Message = "Usuario de personal no encontrado." });

        var dto = _mapper.Map<UsuarioPersonalResponseDTO>(usuarioPersonal);
        return Ok(dto);
    }

    [HttpGet("by-personal/{personalId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByPersonal(int personalId)
    {
        var usuarios = await _repository.GetByPersonalIdAsync(personalId);
        var dtos = _mapper.Map<List<UsuarioPersonalResponseDTO>>(usuarios);
        return Ok(dtos);
    }

    [HttpGet("by-estado/{estadoId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByEstado(int estadoId)
    {
        var usuarios = await _repository.GetByEstadoAsync(estadoId);
        var dtos = _mapper.Map<List<UsuarioPersonalResponseDTO>>(usuarios);
        return Ok(dtos);
    }
}

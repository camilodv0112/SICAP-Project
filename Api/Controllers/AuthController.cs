using Api.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Modelos.Models;
using Modelos.ModelsDTO.UsuarioPersonal;
using Modelos.ModelsDTO.UsuarioEstudiante;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUsuarioPersonalRepository _usuarioPersonalRepo;
    private readonly IUsuarioEstudianteRepository _usuarioEstudianteRepo;
    private readonly IConfiguration _configuration;

    public AuthController(
        IUsuarioPersonalRepository usuarioPersonalRepo,
        IUsuarioEstudianteRepository usuarioEstudianteRepo,
        IConfiguration configuration)
    {
        _usuarioPersonalRepo = usuarioPersonalRepo;
        _usuarioEstudianteRepo = usuarioEstudianteRepo;
        _configuration = configuration;
    }

    /// <summary>
    /// Login para usuarios de personal
    /// </summary>
    [AllowAnonymous]
    [HttpPost("login/personal")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> LoginPersonal([FromBody] UsuarioPersonalLoginDTO model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var usuario = await _usuarioPersonalRepo.GetByUsuarioAsync(model.Usuario);
        if (usuario == null)
            return Unauthorized(new { Message = "Credenciales inválidas" });

        var hash = HashPassword(model.Contraseña);
        if (!usuario.Contraseña.SequenceEqual(hash))
            return Unauthorized(new { Message = "Credenciales inválidas" });

        var token = GenerateJwtTokenPersonal(usuario);
        return Ok(new { token, tipoUsuario = "Personal" });
    }

    /// <summary>
    /// Login para usuarios estudiantes
    /// </summary>
    [AllowAnonymous]
    [HttpPost("login/estudiante")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> LoginEstudiante([FromBody] UsuarioEstudianteLoginDTO model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var usuario = await _usuarioEstudianteRepo.GetByUsuarioAsync(model.Usuario);
        if (usuario == null)
            return Unauthorized(new { Message = "Credenciales inválidas" });

        var hash = HashPassword(model.Contraseña);
        if (!usuario.Contraseña.SequenceEqual(hash))
            return Unauthorized(new { Message = "Credenciales inválidas" });

        var token = GenerateJwtTokenEstudiante(usuario);
        return Ok(new { token, tipoUsuario = "Estudiante" });
    }

    private byte[] HashPassword(string password)
    {
        using var sha = System.Security.Cryptography.SHA256.Create();
        return sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    private string GenerateJwtTokenPersonal(UsuarioPersonal usuario)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = Encoding.ASCII.GetBytes(jwtSettings.GetValue<string>("Key"));

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Usuario),
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioPID.ToString()),
                new Claim(ClaimTypes.Name, usuario.Usuario),
                new Claim(ClaimTypes.Role, "Personal"),
                new Claim("PersonalId", usuario.Personal.ToString()),
                new Claim("TipoUsuario", "Personal")
            }),
            Issuer = jwtSettings.GetValue<string>("Issuer"),
            Audience = jwtSettings.GetValue<string>("Audience"),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private string GenerateJwtTokenEstudiante(UsuarioEstudiante usuario)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = Encoding.ASCII.GetBytes(jwtSettings.GetValue<string>("Key"));

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Usuario),
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioEID.ToString()),
                new Claim(ClaimTypes.Name, usuario.Usuario),
                new Claim(ClaimTypes.Role, "Estudiante"),
                new Claim("EstudianteId", usuario.Estudiante.ToString()),
                new Claim("TipoUsuario", "Estudiante")
            }),
            Issuer = jwtSettings.GetValue<string>("Issuer"),
            Audience = jwtSettings.GetValue<string>("Audience"),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

using Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configuración de la base de datos
builder.Services.AddDbContext<APIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuración de AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Registro de Repositorios
builder.Services.AddScoped(typeof(Api.Repositories.IRepositories.IRepository<>), typeof(Api.Repositories.Repository<>));

// Repositorios de Catálogos
builder.Services.AddScoped<Api.Repositories.IRepositories.IDisciplinaArtisticaRepository, Api.Repositories.DisciplinaArtisticaRepository>();
builder.Services.AddScoped<Api.Repositories.IRepositories.ICargoRepository, Api.Repositories.CargoRepository>();
builder.Services.AddScoped<Api.Repositories.IRepositories.IDepartamentoRepository, Api.Repositories.DepartamentoRepository>();
builder.Services.AddScoped<Api.Repositories.IRepositories.IEstadoUsuarioRepository, Api.Repositories.EstadoUsuarioRepository>();
builder.Services.AddScoped<Api.Repositories.IRepositories.ICarreraRepository, Api.Repositories.CarreraRepository>();
builder.Services.AddScoped<Api.Repositories.IRepositories.ICategoriaNecesidadRepository, Api.Repositories.CategoriaNecesidadRepository>();

// Repositorios de Ubicación
builder.Services.AddScoped<Api.Repositories.IRepositories.IMunicipioRepository, Api.Repositories.MunicipioRepository>();
builder.Services.AddScoped<Api.Repositories.IRepositories.ISedeRepository, Api.Repositories.SedeRepository>();
builder.Services.AddScoped<Api.Repositories.IRepositories.ISalonRepository, Api.Repositories.SalonRepository>();

// Repositorios de Personas
builder.Services.AddScoped<Api.Repositories.IRepositories.IProfesorRepository, Api.Repositories.ProfesorRepository>();
builder.Services.AddScoped<Api.Repositories.IRepositories.IEstudianteRepository, Api.Repositories.EstudianteRepository>();
builder.Services.AddScoped<Api.Repositories.IRepositories.IPersonalRepository, Api.Repositories.PersonalRepository>();

// Repositorios de Eventos y Necesidades
builder.Services.AddScoped<Api.Repositories.IRepositories.IEventoRepository, Api.Repositories.EventoRepository>();
builder.Services.AddScoped<Api.Repositories.IRepositories.ISubcategoriaNecesidadRepository, Api.Repositories.SubcategoriaNecesidadRepository>();
builder.Services.AddScoped<Api.Repositories.IRepositories.INecesidadEventoRepository, Api.Repositories.NecesidadEventoRepository>();
builder.Services.AddScoped<Api.Repositories.IRepositories.IParticipanteEventoRepository, Api.Repositories.ParticipanteEventoRepository>();
builder.Services.AddScoped<Api.Repositories.IRepositories.IResponsableEventoRepository, Api.Repositories.ResponsableEventoRepository>();

// Repositorios de Usuarios
builder.Services.AddScoped<Api.Repositories.IRepositories.IUsuarioPersonalRepository, Api.Repositories.UsuarioPersonalRepository>();
builder.Services.AddScoped<Api.Repositories.IRepositories.IUsuarioEstudianteRepository, Api.Repositories.UsuarioEstudianteRepository>();

// Repositorios de Documentos
builder.Services.AddScoped<Api.Repositories.IRepositories.IPlantillaCartaRepository, Api.Repositories.PlantillaCartaRepository>();
builder.Services.AddScoped<Api.Repositories.IRepositories.ICartaGeneradaRepository, Api.Repositories.CartaGeneradaRepository>();

// Configuración de autenticación JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.GetValue<string>("Key"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
        ValidateAudience = true,
        ValidAudience = jwtSettings.GetValue<string>("Audience"),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Reflection;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EventContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Registrar os repositorios (Injecao de Dependencia)
builder.Services.AddScoped<ITipoEventoRepository, TipoEventoRepository>();

builder.Services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>();

builder.Services.AddScoped<IInstituicaoRepository, InstituicaoRepository>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<IEventoRepository, EventoRepository>();

//Adiciona servico de Jwt Bearer (forma de autentificacao)
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})

.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //Valida quem esta solicitando
        ValidateIssuer = true,

        //Valida quem esta recebendo
        ValidateAudience = true,

        //Define se o tempo de expiracao sera validado
        ValidateLifetime = true,

        //Forma de criptografia e valida a chave de autenticacao
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("events-chaves-autenticacao-webapi-dev")),

        //Valida o tempo de expiracao do token
        ClockSkew = TimeSpan.FromMinutes(5),

        //Nome do issue (de onde esta vindo)
        ValidIssuer = "api_events",

        //Nome do audience (para onde ele esta indo)
        ValidAudience = "api_events"

    };
});



//Adiciona o Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
options.SwaggerDoc("v1", new OpenApiInfo
{
    Version = "v1",
    Title = "API de eventos",
    Description = "Aplicacao para gerenciamento de eventos",
    TermsOfService = new Uri("https://example.com/terms"),
    Contact = new OpenApiContact
    {
        Name = "Gabriel Figueira",
        Url = new Uri("https://github.com/gfigueira10")
    },
    License = new OpenApiLicense
    {
        Name = "Exemplo da Licenca",
        Url = new Uri("https://example.com/license")
    }
});

//Usando a autenticacao no swagger
options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Insira o token JWT: "
});

options.AddSecurityRequirement(document => new OpenApiSecurityRequirement {

    [new OpenApiSecuritySchemeReference("Bearer", document)] = Array.Empty<string>().ToList()
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger(options => { });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

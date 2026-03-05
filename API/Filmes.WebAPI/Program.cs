using Filmes.WebAPI.BdContextFilme;
using Filmes.WebAPI.Interfaces;
using Filmes.WebAPI.Models;
using Filmes.WebAPI.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o contexto do banco de dados (exemplo cm SQL Server)
builder.Services.AddDbContext<FilmeContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnetion")));

// Adiciona o repositorio ao container de injeção de dependencia
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();


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
          IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chaves-autenticacao-webapi-dev")),

          //Valida o tempo de expiracao do token
          ClockSkew = TimeSpan.FromMinutes(5),

          //Nome do issue (de onde esta vindo)
          ValidIssuer = "api_filmes",

          //Nome do audience (para onde ele esta indo)
          ValidAudience = "api_filmes"

      };
  });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Filmes API",
        Description = "Uma API com catalogo de filmes",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "gfigueira10",
            Url = new Uri("https://github.com/gfigueira10")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT:"
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = Array.Empty<string>().ToList()
    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});


//Adiciona o servico de Controllers 
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => { });
    app.UseSwaggerUI(options =>  
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");

        options.RoutePrefix = string.Empty;
    });
}

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

//Adiciona o mapeamento de Controllers
app.MapControllers();

app.Run();

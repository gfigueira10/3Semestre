using Filmes.WebAPI.BdContextFilme;
using Filmes.WebAPI.Interfaces;
using Filmes.WebAPI.Models;
using Filmes.WebAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o contexto do banco de dados (exemplo cm SQL Server)
builder.Services.AddDbContext<FilmeContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnetion")));

// Adiciona o repositorio ao container de injeção de dependencia
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();


//Adiciona o servico de Controllers 
builder.Services.AddControllers();

var app = builder.Build();

//Adiciona o mapeamento de Controllers
app.MapControllers();

app.Run();

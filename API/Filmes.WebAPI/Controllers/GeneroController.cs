using Filmes.WebAPI.Interfaces;
using Filmes.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Filmes.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GeneroController : ControllerBase
{
    private readonly IGeneroRepository _generoRepository;
    private Genero novoGenero;

    public GeneroController(IGeneroRepository generoRepository) 
    {
        _generoRepository = generoRepository;
    }

    [HttpPost]
    public IActionResult Post(Genero novogenero) 
    {
        try
        {
            _generoRepository.Cadastrar(novoGenero);
            return StatusCode(201);
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }

    }
}

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

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            return Ok(_generoRepository.Listar());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetByiD(Guid id)
    {
        try
        {
            return Ok(_generoRepository.BuscarPorId(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public IActionResult Post(Genero novogenero)
    {
        try
        {
            _generoRepository.Cadastrar(novogenero);
            return StatusCode(201);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }


    [HttpPut("{id}")]

    public IActionResult Put(Guid id, Genero generoAtualizado)
    {
        try
        {
            _generoRepository.AtualizarIdUrl(id, generoAtualizado);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);

        }
    }

    [HttpPut]
    public IActionResult PutBodey(Genero generoAtualizado)
    {
        try
        {
            _generoRepository.AtualizarIdCorpo(generoAtualizado);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



[HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _generoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}


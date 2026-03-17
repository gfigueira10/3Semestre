using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }


    /// <summary>
    /// EndPoint da API fazendo a chamada para buscar um usuario por ir
    /// </summary>
    /// <param name="id">id do usuario a ser buscado</param>
    /// <returns>status code 200 e o usuario buscado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id) 
    {
        try
        {
            return Ok(_usuarioRepository.BuscarPorId(id));
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }


    /// <summary>
    /// EndPoint da API que faz a chamada para o metodo de cadastrar um usuario
    /// </summary>
    /// <param name="usuario">Usuario a ser cadastrado</param>
    /// <returns>Status code 201 e o usuario cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(Usuario usuario) 
    {
        try
        {
            _usuarioRepository.Cadastrar(usuario);

            return StatusCode(201, usuario);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }
}

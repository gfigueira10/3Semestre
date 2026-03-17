using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoUsuarioController : ControllerBase
{
    private ITipoUsuarioRepository _tipoUsuarioRepository;

    public TipoUsuarioController(ITipoUsuarioRepository tipoUsuarioRepository)
    {
        _tipoUsuarioRepository = tipoUsuarioRepository;
    }

    /// <summary>
    /// EndPoint da api que faz chamada para o repositorio listar os tipos de usuarios cadastrados
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_tipoUsuarioRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// EndPoint da api que faz chamada para o repositorio buscar um tipo de usuario especifico
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_tipoUsuarioRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// EndPoint da api que faz chamada para o repositorio cadastrar um novo tipo de usuario
    /// </summary>
    /// <param name="tipoUsuario"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Cadastrar(TipoUsuarioDTO tipoUsuario)
    {
        try
        {
            var novoTipoUsuario = new TipoUsuario
            {
                Titulo = tipoUsuario.Titulo!
            };

            _tipoUsuarioRepository.Cadastrar(novoTipoUsuario);
            return StatusCode(201,
                tipoUsuario);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// EndPoint faz chamada para o repositorio atualizar um tipo de usuario especifico
    /// </summary>
    /// <param name="id"></param>
    /// <param name="tipoUsuario"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, TipoUsuarioDTO tipoUsuario)
    {
        try
        {
            var tipoUsuarioAtualizado = new TipoUsuario
            {
                Titulo = tipoUsuario.Titulo!
            };
            _tipoUsuarioRepository.Atualizar(id, tipoUsuarioAtualizado);
            return StatusCode(204, tipoUsuario);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }


    /// <summary>
    /// EndPointfaz chamada para o repositorio deletar um tipo de usuario especifico
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _tipoUsuarioRepository.Deletar(id);
            return StatusCode(204);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}

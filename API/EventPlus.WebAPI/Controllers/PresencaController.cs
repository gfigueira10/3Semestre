using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PresencaController : ControllerBase
{
    private IPresencaRepository _presencaRepository;

    public PresencaController(IPresencaRepository presencaRepository)
    {
        _presencaRepository = presencaRepository;
    }


    /// <summary>
    /// EndPoint da API que retorna uma presenca por id
    /// </summary>
    /// <param name="id">id da presenca a ser buscada</param>
    /// <returns>Status Code 200 e presenca buscada</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id) 
    {
        try
        {
            return Ok(_presencaRepository.BuscarPorId(id));
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }



    /// <summary>
    /// EndPoint da API que retorna uma lista de presenca filtrada por usuario
    /// </summary>
    /// <param name="idUsuario">id do usuario para filtragem</param>
    /// <returns>uma lista de presenca filtrada pelo usuario</returns>
    [HttpGet("ListarMinhas/{idUsuario}")]
    public IActionResult BuscarPorUsuario(Guid idUsuario) 
    {
        try
        {
            return Ok(_presencaRepository.ListarMinhas(idUsuario));
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }



    [HttpPost]
    public IActionResult Inscrever(PresencaDTO presencaDTO) 
    {
        try
        {
            var novaPresenca = new Presenca
            {
                Situacao = presencaDTO.Situacao!,
                IdUsuario = presencaDTO.IdUsuario,
                IdEvento = presencaDTO.IdEvento
            };

            _presencaRepository.Inscrever(novaPresenca);
            return StatusCode(201, presencaDTO);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }



    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, PresencaDTO presenca) 
    {
        try
        {
            var presencaAtualizada = new Presenca
            {
                Situacao = presenca.Situacao!,
            };
            _presencaRepository.Atualizar(id, presencaAtualizada);
            return NoContent();
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }


    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _presencaRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }


    [HttpGet]
    public IActionResult Listar() 
    {
        try
        {
            return Ok(_presencaRepository.Listar());
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }
}

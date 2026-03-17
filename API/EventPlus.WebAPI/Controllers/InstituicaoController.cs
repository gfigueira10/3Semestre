using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstituicaoController : ControllerBase
{
    private IInstituicaoRepository _instituicaoRepository;

    public InstituicaoController(IInstituicaoRepository instituicaoRepository)
    {
        _instituicaoRepository = instituicaoRepository;
    }

    /// <summary>
    /// EndPoint da api que faz chamada para o repositorio listar as instituicoes cadastrados
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_instituicaoRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }


    /// <summary>
    /// EndPoint da api que faz chamada para o repositorio buscar uma instituicao especifica
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_instituicaoRepository.BuscarPorId(id));

        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// EndPoint da api que faz chamada para o repositorio cadastrar uma nova instituicao
    /// </summary>
    /// <param name="instituicao"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Cadastrar(InstituicaoDTO instituicao)
    {
        try
        {
            var novaInstituicao = new Instituicao
            {
                NomeFantasia = instituicao.NomeFantasia!,
                Cnpj = instituicao.Cnpj!,
                Endereco = instituicao.Endereco!
            };

            _instituicaoRepository.Cadastrar(novaInstituicao);
            return StatusCode(201, instituicao);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// EndPoint da api que faz chamada para o repositorio atualizar uma instituicao especifica
    /// </summary>
    /// <param name="id"></param>
    /// <param name="instituicao"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, InstituicaoDTO instituicao)
    {
        try
        {
            var instituicaoAtualizada = new Instituicao
            {
                NomeFantasia = instituicao.NomeFantasia!,
                Cnpj = instituicao.Cnpj!,
                Endereco = instituicao.Endereco!
            };
            _instituicaoRepository.Atualizar(id, instituicaoAtualizada);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// EndPoint da api que faz chamada para o repositorio deletar uma instituicao especifica
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _instituicaoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}

using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventoController : ControllerBase
{
    private readonly IEventoRepository _eventoRepository;

    public EventoController(IEventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository;
    }


    /// <summary>
    /// EndPoint da API que faz chamada para o metodo de listar
    /// </summary>
    /// <param name="id_Usuario">Id de usuario para a filtragem</param>
    /// <returns>Status code 200 e uma lista de eventos</returns>
    [HttpGet("Usuario/{id_Usuario}")]
    public IActionResult ListarPorId(Guid id_Usuario) 
    {
        try
        {
            return Ok(_eventoRepository.ListarPorId(id_Usuario));
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }



    /// <summary>
    /// EndPoint da API que faz a chamada do metodo que lista os proximos eventos 
    /// </summary>
    /// <returns>Status code 200 e a lista dos proximos eventos</returns>
    [HttpGet("ListarProximos")]
    public IActionResult BuscarProximosEventos() 
    {
        try
        {
            return Ok(_eventoRepository.ProximoEventos());
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }



    [HttpPost]
    public IActionResult Cadastrar(EventoDTO eventoDTO) 
    {
        try
        {
            var novoTipoEvento = new Evento
            {
                Nome = eventoDTO.Nome,
                DataEvento = eventoDTO.DataEvento,
                Descricao = eventoDTO.Descricao,
                IdTipoEvento = eventoDTO.Id_TipoEvento
            };
            _eventoRepository.Cadastrar(novoTipoEvento);
            return Ok(eventoDTO);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }


    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, EventoDTO eventoDTO) 
    {
        try
        {
            var eventoAtualizado = new Evento
            {
                Nome = eventoDTO.Nome,
                DataEvento = eventoDTO.DataEvento,
                Descricao = eventoDTO.Descricao,
                IdTipoEvento = eventoDTO.Id_TipoEvento
            };
            _eventoRepository.Atualizar(id, eventoAtualizado);
            return Ok(eventoDTO);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }
}

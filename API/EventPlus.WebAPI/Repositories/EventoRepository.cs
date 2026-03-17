using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class EventoRepository : IEventoRepository
{
    private readonly EventContext _context;

    public EventoRepository (EventContext context)
    {
        _context = context;
    }


    /// <summary>
    /// Atualiza um Evento usando rastreamento automatico
    /// </summary>
    /// <param name="id">id do evento a ser atualizado</param>
    /// <param name="evento">Novos dados do evento</param>
    /// /// <exception cref="NotImplementedException"></exception>
    public void Atualizar(Guid id, Evento evento)
    {
        var eventoBuscado = _context.Eventos.Find(id);

        if (eventoBuscado != null)
        {
            eventoBuscado.Nome = String.IsNullOrWhiteSpace(evento.Nome) ? eventoBuscado.Nome : evento.Nome;
            eventoBuscado.Descricao = String.IsNullOrWhiteSpace(evento.Descricao) ? eventoBuscado?.Descricao : evento.Descricao;
            eventoBuscado.DataEvento = evento.DataEvento == default ? eventoBuscado.DataEvento : evento.DataEvento;
            //O SaveChanges detecta a mudanca na propriedade "Titulo" e atualiza o banco de dados
            _context.SaveChanges();
        }
    }


    /// <summary>
    /// Busca um evento por id
    /// </summary>
    /// <param name="id">id do evento a ser buscado</param>
    /// <returns>Objeto do evento com as informacoes do evento buscado</returns>
    public Evento BuscarPorId(Guid id)
    {
        return _context.Eventos.Find(id)!;
    }


    /// <summary>
    /// Cadastra um novo evento
    /// </summary>
    /// <param name="evento">Evento a ser cadastrado</param>
    public void Cadastrar(Evento evento)
    {
        _context.Eventos.Add(evento);
        _context.SaveChanges();
    }



    /// <summary>
    /// Deleta um evento
    /// </summary>
    /// <param name="Id_Evento">id do evento a ser deletado</param>
    public void Deletar(Guid Id_Evento)
    {
        var eventoBuscado = _context.Eventos.Find(Id_Evento);

        if (eventoBuscado != null)
        {
            _context.Eventos.Remove(eventoBuscado);
            _context.SaveChanges();
        }
    }


    /// <summary>
    /// Busca a lista de evetos cadastrados
    /// </summary>
    /// <returns>Uma lista de eventos</returns>
    public List<Evento> Listar()
    {
        return _context.Eventos
            .OrderBy(evento => evento.Nome)
            .ToList();
    }




    /// <summary>
    /// Metodo que busca eventos no qual um usuario confirmou presenca
    /// </summary>
    /// <param name="Id_Usuario">Id do usuario a ser buscado</param>
    /// <returns>Uma lista de eventos</returns>
    public List<Evento> ListarPorId(Guid Id_Usuario)
    {
        return _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInsituicaoNavigation)
            .Where(e => e.Presencas.Any(p => p.IdUsuario ==
            Id_Usuario && p.Situacao == true))
            .ToList();
    }



    /// <summary>
    /// Metodo que faz a lista de proximos eventos
    /// </summary>
    /// <returns>Uma lista de eventos</returns>
    public List<Evento> ProximoEventos()
    {
        return _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInsituicaoNavigation)
            .Where(e => e.DataEvento >= DateTime.Now)
            .OrderBy(e => e.DataEvento)
            .ToList();
    }
}

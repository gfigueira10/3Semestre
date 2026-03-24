using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class PresencaRepository : IPresencaRepository
{
    private readonly EventContext _eventContext;

    public PresencaRepository(EventContext eventContext)
    {
        _eventContext = eventContext;
    }

    public void Atualizar(Guid id, Presenca presenca)
    {
        var presencaBuscada = _eventContext.Presencas.Find(id);
        if (presencaBuscada != null)
        {
            presencaBuscada.Situacao = !presencaBuscada.Situacao;
            //O SaveChanges detecta a mudanca na propriedade "Titulo" e atualiza o banco de dados
            _eventContext.SaveChanges();
        }

    }


    /// <summary>
    /// Busca uma presenca por id
    /// </summary>
    /// <param name="id">id da presenca buscada</param>
    /// <returns>presenca buscada</returns>
    public Presenca BuscarPorId(Guid id)
    {
        return _eventContext.Presencas
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e!.IdInsituicaoNavigation)
            .FirstOrDefault(p => p.IdPresenca == id)!;
    }

    public void Deletar(Guid id)
    {
        var presencaBuscada = _eventContext.Presencas.Find(id);

        if (presencaBuscada != null)
        {
            _eventContext.Presencas.Remove(presencaBuscada);
            _eventContext.SaveChanges();
        }
    }

    public void Inscrever(Presenca inscricao)
    {
        _eventContext.Presencas.Add(inscricao);
        _eventContext.SaveChanges();
    }

    public List<Presenca> Listar()
    {
        return _eventContext.Presencas
            .OrderBy(presenca => presenca.Situacao)
            .ToList();
    }


    /// <summary>
    /// Lista presencas de usuario especifico 
    /// </summary>
    /// <param name="Id_Usuario">id do usuario para filtragem</param>
    /// <returns>uma lista de presenca de um usuario especifico </returns>
    public List<Presenca> ListarMinhas(Guid Id_Usuario)
    {
        return _eventContext.Presencas
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e.IdInsituicaoNavigation)
            .Where(p => p.IdUsuario == Id_Usuario)
            .ToList();
    }
}

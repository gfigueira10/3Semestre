using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class ComentarioRepository : IComentarioEventoRepository
{
    private readonly EventContext _eventContext;

    public ComentarioRepository(EventContext context)
    {
        _eventContext = context;
    }

    public ComentarioEvento BuscarPorIdUsuario(Guid Id_Usuario, Guid Id_Evento)
    {
        throw new NotImplementedException();
    }

    public void Cadastrar(ComentarioEvento comentarioEvento)
    {
        _eventContext.ComentarioEventos.Add(comentarioEvento);
        _eventContext.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        var comentarioBuscado = _eventContext.ComentarioEventos.Find(id);

        if (comentarioBuscado != null)
        {
            _eventContext.ComentarioEventos.Remove(comentarioBuscado);
            _eventContext.SaveChanges();
        }
    }

    public List<ComentarioEvento> Listar(Guid Id_Evento)
    {
        return _eventContext.ComentarioEventos
        .OrderBy(ComentarioEvento => ComentarioEvento.Descricao)
        .ToList();
    }

    public List<ComentarioEvento> ListarSomenteExibe(Guid Id_Evento)
    {
        return _eventContext.ComentarioEventos
            .Where(c => c.IdEveto == Id_Evento && c.Exibe == true)
            .Include(c => c.IdUsuarioNavigation)
            .ToList();
    }
}

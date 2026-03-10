using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories;

public class TipoEventoRepository : ITipoEventoRepository
{
    private readonly EventContext _context;

    //Injecao de dependencia: Recebe o contexto pelo construtor

    public TipoEventoRepository(EventContext context) 
    {
        _context = context;
    }
    /// <summary>
    /// Atualiza um tipo de evento usando rastreamento automatico 
    /// </summary>
    /// <param name="id">id do evento a ser atualizado</param>
    /// <param name="tipoEvento">Novos dados do tipo evento</param>
    /// <exception cref="NotImplementedException"></exception>

    public void Atualizar(Guid id, TipoEvento tipoEvento)
    {
        var tipoEventoBuscado = _context.TipoEventos.Find(id);

        if (tipoEventoBuscado != null)
        {
            tipoEventoBuscado.Titulo = tipoEvento.Titulo;
            //O SaveChanges detecta a mudanca na propriedade "Titulo" e atualiza o banco de dados
            _context.SaveChanges();
        }
    }


    /// <summary>
    /// Busca um tipo de evento por id
    /// </summary>
    /// <param name="id">id do tipo evento a ser buscado</param>
    /// <returns>Objeto do tipoEvento com as informacoes do tipo de evento buscado</returns>
    public TipoEvento BuscarPorId(Guid id)
    {
        return _context.TipoEventos.Find(id)!;
    }
    



    /// <summary>
    /// Cadastra um novo tipo de evento
    /// </summary>
    /// <param name="tipoEvento">tipo de evento a ser cadastrado</param>

    public void Cadastrar(TipoEvento tipoEvento)
    {
        _context.TipoEventos.Add(tipoEvento);
        _context.SaveChanges();
    }




    /// <summary>
    /// Deleta um tipo de evento
    /// </summary>
    /// <param name="id">id do tipo evento a ser deletado</param>
    public void Deletar(Guid id)
    {
        var tipoEventoBuscado = _context.TipoEventos.Find(id);

        if (tipoEventoBuscado != null)
        {
            _context.TipoEventos.Remove(tipoEventoBuscado);
            _context.SaveChanges();
        }
    }



    /// <summary>
    /// Busca a lista de tipo de eventos cadastrado
    /// </summary>
    /// <returns>uma lista de tipo eventos</returns>
    public List<TipoEvento> Listar()
    {
        return _context.TipoEventos
            .OrderBy(tipoEvento => tipoEvento.Titulo)
            .ToList();
    }
}

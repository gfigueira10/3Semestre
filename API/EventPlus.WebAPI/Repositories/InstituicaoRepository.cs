using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories;

public class InstituicaoRepository : IInstituicaoRepository
{
    private readonly EventContext _context;

    //Injecao de dependencia: Recebe o contexto pelo construtor

    public InstituicaoRepository(EventContext context)
    {
        _context = context;
    }




    /// <summary>
    /// Atualiza uma Instituicao usando rastreamento automatico 
    /// </summary>
    /// <param name="id">id da instituicao a ser atualizado</param>
    /// <param name="tipoEvento">Novos dados da instituicao</param>
    /// <exception cref="NotImplementedException"></exception>
    public void Atualizar(Guid id, Instituicao instituicao)
    {
        var instituicaoBuscado = _context.Instituicaos.Find(id);

        if (instituicaoBuscado != null)
        {
            instituicaoBuscado.NomeFantasia = instituicaoBuscado.NomeFantasia;
            //O SaveChanges detecta a mudanca na propriedade "Titulo" e atualiza o banco de dados
            _context.SaveChanges();
        }
    }




    /// <summary>
    /// Busca uma instituicao por id
    /// </summary>
    /// <param name="id">id da instituicao a ser buscado</param>
    /// <returns>Objeto da instituicao com as informacoes da instituicao buscado</returns>
    public Instituicao BuscarPorId(Guid id)
    {
        return _context.Instituicaos.Find(id)!;
    }



    /// <summary>
    /// Cadastra uma nova instituicao
    /// </summary>
    /// <param name="tipoEvento">instituicao a ser cadastrada</param>
    public void Cadastrar(Instituicao instituicao)
    {
        _context.Instituicaos.Add(instituicao);
        _context.SaveChanges();
    }




    /// <summary>
    /// Deleta uma instituicao
    /// </summary>
    /// <param name="id">id da instituicao a ser deletado</param>
    public void Deletar(Guid id)
    {
        var instituicaoBuscado = _context.Instituicaos.Find(id);

        if (instituicaoBuscado != null)
        {
            _context.Instituicaos.Remove(instituicaoBuscado);
            _context.SaveChanges();
        }
    }



    /// <summary>
    /// Busca a lista de instituicoes cadastradas
    /// </summary>
    /// <returns>uma lista de instituicoes</returns>
    public List<Instituicao> Listar()
    {
        return _context.Instituicaos
            .OrderBy(instituicao => instituicao.NomeFantasia)
            .ToList();
    }
}

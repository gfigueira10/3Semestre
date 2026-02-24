using Filmes.WebAPI.Models;

namespace Filmes.WebAPI.Interfaces;

public interface IFilmeRepository
{
    void Cadastrar(Filme novoFilme);
    void AtualizarIdCorpo(Filme filmeAtualizado);
    void AtualizarIdUrl(Guid id, Filme filmeAtualizado);

    List<Filme> Listar();
    void Delete(Guid id);

    Filme BuscarPorId(Guid id);
}

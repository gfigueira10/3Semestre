using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IEventoRepository
{
    void Cadastrar(Evento evento);
    List<Evento> Listar();
    void Deletar(Guid Id_Evento);
    void Atualizar(Guid id, Evento evento);
    List<Evento> ListarPorId(Guid Id_Usuario);
    List<Evento> ProximoEventos();
    Evento BuscarPorId(Guid id);
}

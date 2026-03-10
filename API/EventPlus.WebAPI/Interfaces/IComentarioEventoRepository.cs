using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IComentarioEventoRepository
{
    void Cadastrar(ComentarioEvento comentarioEvento);
    void Deletar(Guid id);

    List<ComentarioEvento> Listar(Guid Id_Evento);
    ComentarioEvento BuscarPorIdUsuario(Guid Id_Usuario, Guid Id_Evento);

    List<ComentarioEvento> ListarSomenteExibe(Guid Id_Evento);
}

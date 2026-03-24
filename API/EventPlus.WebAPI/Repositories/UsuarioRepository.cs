using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly EventContext _context;

    //metodo construtor que aplica a injeção de dependência
    public UsuarioRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Busca o usua´rio pelo e-mail e valida o hash da senha
    /// </summary>
    /// <param name="email">Email do usuaário a ser buscado</param>
    /// <param name="Senha">Senha para validar o usuário</param>
    /// <returns>ele retorna o usuario buscado</returns>
    public Usuario BuscarPorEmail(string email, string Senha)
    {
        //Primeiro, buscamos o usuario pelo e-mail
        var usuarioBuscado = _context.Usuarios
            .Include(usuario => usuario.IdTipoUsuarioNavigation) // Inclui os dados do tipo de usuário
            .FirstOrDefault(usuario => usuario.Email == email);

        //Se o usuário não for encontrado
        if (usuarioBuscado != null)
        {
            //Comparamos o hash da senha digitada com o que esta no banco
            bool confere = Criptografia.CompararHash(Senha, usuarioBuscado.Senha);

            if (confere)
            {
                return usuarioBuscado;
            }
        }

        return null!;
    }

    /// <summary>
    /// Busca um usuario pelo id inclundo os dados do seu tipo de Usuário
    /// </summary>
    /// <param name="id">id do usuario a ser buscado</param>
    /// <returns>Usuário buscado e seu tipo de usuário</returns>
    public Usuario BuscarPorId(Guid id)
    {
        return _context.Usuarios
            .Include(usuario => usuario.IdTipoUsuarioNavigation)
            .FirstOrDefault(usuario => usuario.IdUsuario == id)!;
    }

    /// <summary>
    /// Cadastra um novo usuário, A senha é criptografada e o Id gerado pelo banco
    /// </summary>
    /// <param name="usuario">Usuario a ser cadastrado</param>
    public void Cadastrar(Usuario usuario)
    {
        usuario.Senha = Criptografia.GerarHash(usuario.Senha);

        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }

    public List<Usuario> Listar()
    {
        return _context.Usuarios.ToList();
    }
}


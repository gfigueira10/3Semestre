using Filmes.WebAPI.DTO;
using Filmes.WebAPI.Interfaces;
using Filmes.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Filmes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginController(IUsuarioRepository usuarioRepository) 
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        public IActionResult Login(LoginDTO loginDTO) 
        {
            try
            {
                Usuario usuarioBuscado = _usuarioRepository.BuscarPorEmailESenha(loginDTO.Email!, loginDTO.Senha!);

                if (usuarioBuscado == null)
                {
                    return NotFound("Email ou Senha invalidos!");
                }


                // Caso encontre o usuario, prosseguir para a criacao do token

                //1 - Definir as informacoes(Claims) que serao fornecidas no token (Payload)
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti,usuarioBuscado.IdUsuario),

                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado?.Email!),

                    //new Claim("Claim Personalizada", "Valor da Claim Personalizada")
                };

                //2 - Definir a chave de acesso ao token
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chaves-autenticacao-webapi-dev"));

                //3 - Definir as credenciais do token (HEADER)
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //4 - Gerar Token
                var token = new JwtSecurityToken
                (
                    //emissor do token
                    issuer: "api_filmes",

                    //destinatario do token
                    audience: "api_filmes",

                    //dados definidos nas claims(informacoes)
                    claims: claims,

                    //tempo de expiracao do token
                    expires: DateTime.Now.AddMinutes(5),

                    //credenciais do token
                    signingCredentials: creds
                );

                //5 - Retornar o token criado
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });

            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }
    }
}

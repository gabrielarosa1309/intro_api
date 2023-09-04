using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using webapi.filmes.manha.Domains;
using webapi.filmes.manha.Interfaces;
using webapi.filmes.manha.Repositories;


namespace webapi.filmes.manha.Controllers
{
    //Define que a rota de uma requisição será no seguinte formato:
    //domínio/api/nomeController
    //Exemplo: http://localhost:5000/api/genero
    [Route("api/[controller]")]

    //Define que é um controlador de API
    [ApiController]

    //Define que o tipo de resposta da API será no formato JSON
    [Produces("application/json")]

    public class UsuarioController : ControllerBase
    {
        //Objeto _usuarioRepository que irá receber todos os métodos definidos na interface IUsuarioRepository
        private IUsuarioRepository _usuarioRepository { get; set; }

        //Instancia o objeto _usuarioRepository para que haja referência aos métodos no repositório
        public UsuarioController()
        {
            ///_usuarioRepository = new UsuarioRepository();
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpPost]
        public IActionResult Login(UsuarioDomain usuario)
        {
            try
            {
                UsuarioDomain usuarioBuscado = _usuarioRepository.Login(usuario.Email, usuario.Senha);

                if (usuarioBuscado == null)
                {
                    return NotFound("Email ou Senha inválidos!");
                }

                //Caso encontre o usuário, prossegue para a criação do token

                //1 - Definir as informações (Claims) que serão fornecidos no token (PAYLOAD)
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti,usuarioBuscado.IdUsuario.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email,usuarioBuscado.Email),
                    new Claim(ClaimTypes.Role,usuarioBuscado.Permissao.ToString()),

                    //Existe a possibilidade de criar uma claim personalizada
                    //new Claim("Claim Personalizada","Valor da claim personalizada")
                };

                //2 - Definir a chave de acesso ao token
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev"));

                //3 - Definir as credenciais do token (HEADER)
                var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

                return Ok(usuarioBuscado);
            }
            catch (Exception erro) 
            { 
            
            }
        }

    }
}

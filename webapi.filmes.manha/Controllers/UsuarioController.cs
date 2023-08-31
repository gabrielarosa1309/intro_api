using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        }

    }
}

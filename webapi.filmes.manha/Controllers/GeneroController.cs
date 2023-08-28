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

    //Método controlador que herda da controller base
    //Onde será criado os Endpoints
    public class GeneroController : ControllerBase
    {
        //Objeto _generoRepository que irá receber todos os métodos definidos na interface IGeneroRepository
        private IGeneroRepository _generoRepository { get; set; }

        //Instancia o objeto _generoRepository para que haja referência aos métodos no repositório
        public GeneroController()
        {
            _generoRepository = new GeneroRepository();
        }

        /// <summary>
        /// Endpoint que aciona o método ListarTodos no repositório
        /// </summary>
        /// <returns> Retorna a resposta para o usuário (front-end) </returns>
        [HttpGet]
        public IActionResult Get() 
        {
            try
            {
                //Cria uma lista que recebe os dados da requisição
                List<GeneroDomain> listaGeneros = _generoRepository.ListarTodos();

                //Retorna a lista no formato JSON com o status code Ok(200)
                return Ok(listaGeneros);
            }
            catch (Exception erro) 
            {
                //Retorna um status code 400 - BadRequest e a mensagem do erro
                return BadRequest(erro.Message);
            }
        }


        /// <summary>
        /// Endpoint que aciona o método de cadastro do gênero
        /// </summary>
        /// <param name="novoGenero"> Objeto recebido na requisição </param>
        /// <returns> Retorna a resposta para o usuário (front-end) </returns>
        [HttpPost]
        public IActionResult Post(GeneroDomain novoGenero)
        {
            try
            {
                //Fazendo a chamada para o método cadastrar passando o objeto como parâmetro
                _generoRepository.Cadastrar(novoGenero);

                //Retorna um status code 201 - Created
                return StatusCode(201);
            }
            catch (Exception erro) 
            { 
                //Retorna um status code 400 - BadRequest e a mensagem do erro
                return BadRequest (erro.Message);
            }
        }

        /// <summary>
        /// Endpoint que aciona o método de deletar o gênero
        /// </summary>
        /// <param name="IdGenero"> Objeto recebido na requisição </param>
        /// <returns> Retorna a resposta para o usuário (front-end) </returns>
        [HttpDelete]
        public IActionResult Delete (int IdGenero)
        {
            try
            {
                //Fazendo a chamada para o método deletar passando o objeto como parâmetro
                _generoRepository.Deletar(IdGenero);

                //Retorna um status code 202 - Deleted
                return StatusCode(202);
            }
            catch (Exception erro)
            {
                //Retorna um status code 400 - BadRequest e a mensagem do erro
                return BadRequest(erro.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                //Cria um objeto generoBuscado que irá receber o gênero buscado no db
                GeneroDomain generoBuscado = _generoRepository.GetById(id);

                //Verifica se nenhum gênero foi encontrado
                if (generoBuscado == null)
                {
                    //Caso não seja encontrado, retorna um status code 404 - Not Found com a mensagem personalizada
                    return NotFound("Nenhum gênero foi encontrado!");
                }

                //Caso seja encontrado, retorna o gênero buscado com um status code 200 - Ok
                return Ok(generoBuscado);
            }
            catch (Exception erro)
            {
                //Retorna um status code 400 - BadRequest e a mensagem do erro
                return BadRequest(erro.Message);
            }
        }


    }
}

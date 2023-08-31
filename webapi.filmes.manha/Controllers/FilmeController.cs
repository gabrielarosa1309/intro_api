using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.filmes.manha.Domains;
using webapi.filmes.manha.Interfaces;
using webapi.filmes.manha.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
    public class FilmeController : ControllerBase
    {
        //Objeto _filmeRepository que irá receber todos os métodos definidos na interface IFilmeRepository
        private IFilmeRepository _filmeRepository { get; set; }

        //Instancia o objeto _filmeRepository para que haja referência aos métodos no repositório
        public FilmeController()
        {
            _filmeRepository = new FilmeRepository();
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
                List<FilmeDomain> listaFilmes = _filmeRepository.ListarTodos();

                //Retorna a lista no formato JSON com o status code Ok(200)
                return Ok(listaFilmes);
            }
            catch (Exception erro)
            {
                //Retorna um status code 400 - BadRequest e a mensagem do erro
                return BadRequest(erro.Message);
            }
        }





        /// <summary>
        /// Endpoint que aciona o método de cadastro do filme
        /// </summary>
        /// <param name="novoFilme"> Objeto recebido na requisição </param>
        /// <returns> Retorna a resposta para o usuário (front-end) </returns>
        [HttpPost]
        public IActionResult Post(FilmeDomain novoFilme)
        {
            try
            {
                //Fazendo a chamada para o método cadastrar passando o objeto como parâmetro
                _filmeRepository.Cadastrar(novoFilme);

                //Retorna um status code 201 - Created
                return StatusCode(201);
            }
            catch (Exception erro)
            {
                //Retorna um status code 400 - BadRequest e a mensagem do erro
                return BadRequest(erro.Message);
            }
        }





        /// <summary>
        ///  Endpoint que aciona o método de deletar o filme
        /// </summary>
        /// <param name="IdFilme"> Objeto recebido na requisição </param>
        /// <returns> Retorna a resposta para o usuário (front-end) </returns>
        [HttpDelete("{IdFilme}")]
        public IActionResult Delete(int IdFilme)
        {
            try
            {
                //Fazendo a chamada para o método deletar passando o objeto como parâmetro
                _filmeRepository.Deletar(IdFilme);

                //Retorna um status code 202 - Deleted
                return StatusCode(202);
            }
            catch (Exception erro)
            {
                //Retorna um status code 400 - BadRequest e a mensagem do erro
                return BadRequest(erro.Message);
            }
        }





       
        /// <summary>
        /// Endpoint que aciona o método de buscar filme pelo id
        /// </summary>
        /// <param name="id"> Objeto recebido na requisição </param>
        /// <returns> Retorna a resposta para o usuário (front-end) </returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                //Cria um objeto filmeBuscado que irá receber o filme buscado no db
                FilmeDomain filmeBuscado = _filmeRepository.GetById(id);

                //Verifica se nenhum filme foi encontrado
                if (filmeBuscado == null)
                {
                    //Caso não seja encontrado, retorna um status code 404 - Not Found com a mensagem personalizada
                    return NotFound("Nenhum gênero foi encontrado!");
                }

                //Caso seja encontrado, retorna o filme buscado com um status code 200 - Ok
                return Ok(filmeBuscado);
            }
            catch (Exception erro)
            {
                //Retorna um status code 400 - BadRequest e a mensagem do erro
                return BadRequest(erro.Message);
            }
        }





        /// <summary>
        /// Endpoint que aciona o método de atualizar filme pelo corpo buscando pelo id
        /// </summary>
        /// <param name="filme"> Objeto recebido na requisição </param>
        /// <returns> Retorna a resposta para o usuário (front-end) </returns>
        [HttpPut]
        public IActionResult PutIdBody(FilmeDomain filme)
        {
            try
            {
                FilmeDomain filmeBuscado = _filmeRepository.GetById(filme.IdFilme);

                if (filmeBuscado != null)
                {
                    try
                    {
                        _filmeRepository.UpdateByIdBody(filme);

                        //Retorna o filme atualizado com um status code 200 - Ok
                        return StatusCode(204);
                    }
                    catch (Exception erro)
                    {
                        //Retorna um status code 400 - BadRequest e a mensagem do erro
                        return BadRequest(erro.Message);
                    }
                }

                return NotFound("Filme não encontrado!");

            }
            catch (Exception erro)
            {
                //Retorna um status code 400 - BadRequest e a mensagem do erro
                return BadRequest(erro.Message);
            }
        }





        /// <summary>
        ///  Endpoint que aciona o método de atualizar filme buscando pelo id
        /// </summary>
        /// <param name="id"> Objeto recebido na requisição </param>
        /// <param name="filme"> Objeto devolvido na requisição </param>
        /// <returns> Retorna a resposta para o usuário (front-end) </returns>
        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, FilmeDomain filme)
        {
            try
            {
                FilmeDomain filmeBuscado = _filmeRepository.GetById(id);

                if (filmeBuscado == null)
                {
                    //Caso não seja encontrado, retorna um status code 404 - Not Found com a mensagem personalizada
                    return NotFound("Nenhum filme foi encontrado para ser atualizado!");
                }

                _filmeRepository.UpdateByIdUrl(id, filme);

                //Retorna o filme atualizado com um status code 200 - Ok
                return StatusCode(200);
            }
            catch (Exception erro)
            {
                //Retorna um status code 400 - BadRequest e a mensagem do erro
                return BadRequest(erro.Message);
            }
        }
    }
}

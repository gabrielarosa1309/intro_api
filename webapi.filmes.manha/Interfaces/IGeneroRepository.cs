using webapi.filmes.manha.Domains;

namespace webapi.filmes.manha.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório GeneroRepository
    /// Definir os métodos que serão implementados pelo GeneroRepository
    /// </summary>
    public interface IGeneroRepository
    {
        //TipoRetorno NomeMetodo(TipoParametro NomeParametro)

        /// <summary>
        /// Cadastrar um novo genero
        /// </summary>
        /// <param name="novoGenero"> Objeto que será cadastrado </param>
        void Cadastrar(GeneroDomain novoGenero);




        /// <summary>
        /// Listar todos os objeos cadastrados
        /// </summary>
        /// <returns> Lista com os objetos </returns>
        List<GeneroDomain> ListarTodos();




        /// <summary>
        /// Atualizar um objeto existente passando o seu id pelo corpo da requisição
        /// </summary>
        /// <param name="genero"> Objeto atualizado (novas informações) </param>
        void UpdateByIdBody(GeneroDomain genero);




        /// <summary>
        /// Atualizar objeto existente passando o seu id pela url
        /// </summary>
        /// <param name="id"> Id do objeto que será atualizado </param>
        /// <param name="generoUpdateUrl"> Objeto atualizado (novas informações) </param>
        void UpdateByIdUrl(int id,GeneroDomain generoUpdateUrl);




        /// <summary>
        /// Deletar um objeto
        /// </summary>
        /// <param name="id"> Id do objeto será deletado </param>
        void Deletar(int id);




        /// <summary>
        /// Buscar um objeto através do seu id
        /// </summary>
        /// <param name="id"> Id do objeto a ser buscado </param>
        /// <returns> Objeto buscado </returns>
        GeneroDomain GetById(int id);
    }
}

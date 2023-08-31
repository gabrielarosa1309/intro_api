using webapi.filmes.manha.Domains;

namespace webapi.filmes.manha.Interfaces
{
    public interface IFilmeRepository
    {
        void Cadastrar(FilmeDomain novoFilme);

        List<FilmeDomain> ListarTodos();

        void UpdateByIdBody(FilmeDomain filme);

        void UpdateByIdUrl(int id, FilmeDomain filmeUpdateUrl);

        void Deletar(int id);

        FilmeDomain GetById(int id);
    }
}

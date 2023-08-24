using webapi.filmes.manha.Domains;

namespace webapi.filmes.manha.Interfaces
{
    public interface IFilmeRepository
    {
        void Cadastrar(FilmeDomain novoFilme);

        List<FilmeDomain> ListarTodos();

        void AtualizarIdCorpo(FilmeDomain filme);

        void Deletar(int id);

        FilmeDomain BuscarPorId(int id);
    }
}

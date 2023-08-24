using System.ComponentModel.DataAnnotations;

namespace webapi.filmes.manha.Domains
{
    public class FilmeDomain
    {
        public int IdFilme { get; set; }
        public int IdGenero { get; set; }

        [Required(ErrorMessage = "O título do filme é obrigatório!")]
        public string? Titulo { get; set; }

        //Referência para a classe Genero
        public GeneroDomain? Genero { get; set; }
    }
}

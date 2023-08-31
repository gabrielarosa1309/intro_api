using System.Data.SqlClient;
using webapi.filmes.manha.Domains;
using webapi.filmes.manha.Interfaces;

namespace webapi.filmes.manha.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        /// <summary>
        /// String de conexão com o banco de dados que recebe os seguintes parâmetros:
        /// Data Source: nome do servidor
        /// Initial Catalog: Nome do banco de dados
        /// Autenticação: 
        ///     - Windows: Integrated Security = true
        ///     - SqlServer: User Id = sa; Pwd = Senha
        /// </summary>
        private string StringConexao = "Data Source = NOTE04-S15; Initial Catalog = Filmes; User Id = sa; Pwd = Senai@134";




        public void UpdateByIdBody(FilmeDomain filme)
        {
            //Declara a conexão passando a string de conexão como parâmetro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryUpdateBody = "UPDATE Filme SET Titulo = @novoTitulo, IdGenero = @novoGenero WHERE IdFilme = @idFilme";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryUpdateBody, con))
                {
                    cmd.Parameters.AddWithValue("@novoGenero", filme.IdGenero);
                    cmd.Parameters.AddWithValue("@novoTitulo", filme.Titulo);
                    cmd.Parameters.AddWithValue("@idFilme", filme.IdFilme);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        public void UpdateByIdUrl(int id, FilmeDomain filme)
        {
            //Declara a conexão passando a string de conexão como parâmetro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Abre a conexão com o banco de dados
                con.Open();

                //Declara a query que será executada
                string queryUpdateUrl = "UPDATE Filme SET Titulo = @novoTitulo, IdGenero = @novoGenero WHERE IdFilme = @id";

                //Declara o SqlCommand passando a query que será executada e a conexão com o db
                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    //Passa o valor do parâmetro @NomeUpdateUrl
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@novoGenero", filme.IdGenero);
                    cmd.Parameters.AddWithValue("@novoTitulo", filme.Titulo);

                    //Executar a query (queryInsert)
                    cmd.ExecuteNonQuery();
                }
            }
        }





        public FilmeDomain GetById(int id)
        {
            //Declara a conexão passando a string de conexão como parâmetro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a query que será executada
                string querySelectById = "SELECT Genero.Nome, Filme.Titulo, Filme.IdFilme, Filme.IdGenero FROM Filme LEFT JOIN Genero ON Genero.IdGenero = Filme.IdGenero WHERE IdFilme = @idFilme";

                //Abre a conexão com o banco de dados
                con.Open();

                //Declara o SqlDataReader para percorrer a tabela do banco de dados
                SqlDataReader rdr;

                //Declara o SqlCommand passando a query que será executada e a conexão com o db
                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    //Passa o valor para o parâmetro IdFilme
                    cmd.Parameters.AddWithValue("@IdFilme", id);

                    //Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    //Verifica se o resultado da query retornou algum registro
                    if (rdr.Read())
                    {
                        //Se sim, instancia um novo objeto generoBuscado do tipo FilmeDomain
                        FilmeDomain filmeBuscado = new FilmeDomain
                        {
                            IdFilme = Convert.ToInt32(rdr["IdFilme"]),
                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),
                            Titulo = rdr["Titulo"].ToString(),
                            Genero = new GeneroDomain
                            {
                                Nome = rdr["Nome"].ToString(),
                                IdGenero = Convert.ToInt32(rdr["IdGenero"])
                            }
                        };

                        //Retorna o generoBuscado com os dados obtidos
                        return filmeBuscado;
                    }

                    //Se não, retorna null
                    return null;
                }
            }
        }



        /// <summary>
        /// Cadastrar um novo filme
        /// </summary>
        /// <param name="novoFilme"> Filme a ser adicionado </param>
        public void Cadastrar(FilmeDomain novoFilme)
        {
            //Declara a conexão passando a string de conexão como parâmetro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a query que será executada
                string queryInsert = "INSERT INTO Filme(IdGenero,Titulo) VALUES (@idgenero,@titulo)";

                //Declara o SqlCommand passando a query que será executada e a conexão com o db
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    //Passa o valor do parâmetro @Nome
                    cmd.Parameters.AddWithValue("@idgenero", novoFilme.IdGenero);
                    cmd.Parameters.AddWithValue("@titulo", novoFilme.Titulo);

                    //Abre a conexão com o banco de dados
                    con.Open();

                    //Executar a query (queryInsert)
                    cmd.ExecuteNonQuery();
                }
            }
        }



        /// <summary>
        /// Deletar um filme existente
        /// </summary>
        /// <param name="IdFilme"> Id do filme a ser deletado </param>
        public void Deletar(int IdFilme)
        {
            //Declara a conexão passando a string de conexão como parâmetro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a query que será executada
                string queryDelete = "DELETE FROM Filme WHERE IdFilme = @IdFilmeDelete";

                //Declara o SqlCommand passando a query que será executada e a conexão com o db
                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    //Passa o valor do parâmetro IdFilmeDelete
                    cmd.Parameters.AddWithValue("@IdFilmeDelete", IdFilme);

                    //Abre a conexão com o banco de dados
                    con.Open();

                    //Executar a query (queryDelete)
                    cmd.ExecuteNonQuery();
                }
            }
        }




        public List<FilmeDomain> ListarTodos()
        {
            //Cria uma lista de objetos do tipo filme 
            List<FilmeDomain> listaFilmes = new List<FilmeDomain>();

            //Declara a SqlConnection passando a string de conexão como parâmetro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a instrução a ser executada 
                string querySelectAll = "SELECT IdFilme, Titulo, Filme.IdGenero, Genero.Nome FROM Filme LEFT JOIN Genero ON Filme.IdGenero = Genero.IdGenero";

                //Abre a conexão com o banco de dados
                con.Open();

                //Declara o SqlDataReader para percorrer a tabela do banco de dados
                SqlDataReader rdr;

                //Declara o SqlCommand passando a query que será executada e a conexão com o db
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    //Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        FilmeDomain filme = new FilmeDomain()
                        {
                            IdFilme = Convert.ToInt32(rdr["IdFilme"]),
                            Titulo = rdr["Titulo"].ToString(),
                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),
                            Genero = new GeneroDomain()
                            {
                                Nome = rdr["Nome"].ToString(),
                                IdGenero = Convert.ToInt32(rdr["IdGenero"])
                            }
                        };

                        //Adiciona cada objeto dentro da lista
                        listaFilmes.Add(filme);
                    }
                }
            }

            //Retorna a lista de gêneros
            return listaFilmes;
        }
    }
}

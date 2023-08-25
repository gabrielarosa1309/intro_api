using System.Data.SqlClient;
using webapi.filmes.manha.Domains;
using webapi.filmes.manha.Interfaces;

namespace webapi.filmes.manha.Repositories
{
    public class GeneroRepository : IGeneroRepository
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

        public void AtualizarIdCorpo(GeneroDomain genero)
        {
            throw new NotImplementedException();
        }

        public void AtualizarIdUrl(int id, GeneroDomain genero)
        {
            throw new NotImplementedException();
        }

        public GeneroDomain BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cadastrar um novo gênero
        /// </summary>
        /// <param name="novoGenero"> Objeto com as informações que serão cadastradas </param>
        public void Cadastrar(GeneroDomain novoGenero)
        {
            //Declara a conexão passando a string de conexão como parâmetro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a query que será executada
                string queryInsert = "INSERT INTO Genero(Nome) VALUES (@Nome)";

                //Declara o SqlCommand passando a query que será executada e a conexão com o db
                using (SqlCommand cmd = new SqlCommand(queryInsert,con))
                {
                    //Passa o valor do parâmetro @Nome
                    cmd.Parameters.AddWithValue("@Nome", novoGenero.Nome);

                    //Abre a conexão com o banco de dados
                    con.Open();

                    //Executar a query (queryInsert)
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Deletar um gênero existente
        /// </summary>
        /// <param name="IdGenero"> Objeto que será deletado </param>
        public void Deletar(int IdGenero)
        {
            //Declara a conexão passando a string de conexão como parâmetro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a query que será executada
                string queryDelete = "DELETE FROM Genero WHERE IdGenero = @IdDelete";

                //Declara o SqlCommand passando a query que será executada e a conexão com o db
                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    //Passa o valor do parâmetro @Nome
                    cmd.Parameters.AddWithValue("@IdDelete", IdGenero);

                    //Abre a conexão com o banco de dados
                    con.Open();

                    //Executar a query (queryInsert)
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Listar todos os objetos (generos)
        /// </summary>
        /// <returns> Lista de objetos (gêneros) </returns>
        public List<GeneroDomain> ListarTodos()
        {
            //Cria uma lista de objetos do tipo gênero 
            List<GeneroDomain> listaGeneros = new List<GeneroDomain>();

            //Declara a SqlConnection passando a string de conexão como parâmetro
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a instrução a ser executada 
                string querySelectAll = "SELECT IdGenero, Nome FROM Genero";

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
                        GeneroDomain genero = new GeneroDomain()
                        {
                            //Atribui a propriedade IdGenero o valor recebido no rdr
                            IdGenero = Convert.ToInt32(rdr[0]),
                            //Atribui a propriedade Nome o valor recebido no rdr
                            Nome = rdr["Nome"].ToString()
                        };

                        //Adiciona cada objeto dentro da lista
                        listaGeneros.Add(genero);
                    }
                }
            }

            //Retorna a lista de gêneros
            return listaGeneros;
        }
    }
}

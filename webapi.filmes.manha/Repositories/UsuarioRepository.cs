using System.Data.SqlClient;
using webapi.filmes.manha.Domains;
using webapi.filmes.manha.Interfaces;

namespace webapi.filmes.manha.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
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

        public UsuarioDomain Login(string email, string password)
        {
            using (SqlConnection con = new SqlConnection(StringConexao)) 
            {
                string queryLogin = "SELECT IdUsuario,Email,Permissao FROM Usuario WHERE Email = @email AND Senha = @password";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(queryLogin,con))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read()) 
                    {
                        UsuarioDomain usuario = new UsuarioDomain()
                        {
                            IdUsuario = Convert.ToInt32(rdr["IdUsuario"]),
                            Email = rdr["Email"].ToString(),
                            Permissao = Convert.ToBoolean(rdr["Permissao"])
                        };
                        return usuario;
                    }

                    return null;
                }
            }
        }
    }
}

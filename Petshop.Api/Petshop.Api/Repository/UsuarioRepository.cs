using Dapper;
using MySqlConnector;
using Petshop.Api.Entidades;
using Petshop.Api.Interface;

namespace Petshop.Api.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string ConnectionString = "Server=localhost;Database=petshop;Uid=root;Pwd=teste;";

        public async Task<Usuario> ObterUsuario(string email, string senha)
        {
            using (var conexao = new MySqlConnection(ConnectionString))
            {
                try
                {
                    conexao.Open();

                    var usuario = conexao.QueryFirstOrDefault<Usuario>(@"SELECT id, nome, senha, email, idCliente FROM usuario WHERE email = @Email and senha = @Senha", new
                    {
                        Email = email,
                        Senha = senha
                    });

                    return usuario;
                }
                catch (Exception ex) 
                {
                    return null;
                }

            }
        }
    }
}

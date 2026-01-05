using Petshop.Api.Domain;
using Petshop.Api.Interface;
using MySqlConnector;
using Dapper;
using Petshop.Api.DTO;
using Petshop.Api.Entidades;

namespace Petshop.Api.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private string ConnectionString = "Server=localhost;Database=petshop;Uid=root;Pwd=teste;";

        public void CadastrarCliente(Cliente cliente)
        {
            using (var conexao = new MySqlConnection(ConnectionString))
            {
                conexao.Open();

                string sql = "INSERT INTO cliente (nome, documento, dataNascimento, sexo) VALUES (@nome, @documento, @dataNascimento, @sexo)";

                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@documento", cliente.Documento);
                    cmd.Parameters.AddWithValue("@dataNascimento", cliente.DataNascimento);
                    cmd.Parameters.AddWithValue("@sexo", cliente.Sexo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Cliente ConsultarCliente(string documento)
        {
            using (var conexao = new MySqlConnection(ConnectionString))
            {
                try
                {                   
                    conexao.Open();

                    var cliente = conexao.QueryFirstOrDefault<Cliente>(@"
                           SELECT 
                                Id, 
                                nome, 
                                documento, 
                                CAST(IFNULL(datanascimento, STR_TO_DATE('01/01/1995', '%d/%m/%Y')) AS DATE) AS datanascimento, 
                                sexo 
                            FROM 
                                Cliente 
                            WHERE 
                                Documento = @documento", new { Documento = documento });

                    return cliente;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocorreu um erro ao conectar: {ex.Message}");
                    return null;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public List<Cliente> RetornaListaClientes()
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    // Exemplo: Executando uma consulta com Dapper
                    connection.Open();

                    // Executando uma consulta com Dapper
                    var listacliente = connection.Query<Cliente>("SELECT * FROM cliente");

                    return listacliente.ToList();
                }
                catch
                {
                    return null;
                }
                finally
                {
                    connection.Clone();
                }
            }
        }

        public async Task UpDateCliente(int id, string nome, string sexo, DateTime dataNascimento)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = @"UPDATE Cliente SET 
                        Nome = @Nome,
                        DataNascimento = @DataNascimento,
                        Sexo = @Sexo
                        WHERE id = @id";

                connection.Execute(sql, new
                {
                        Id = id,
                        Nome = nome,
                        Sexo = sexo,
                        DataNascimento = dataNascimento                      
                });
            }
        }

        public Cliente ConsultarClienteById(int id)
        {
            using (var conexao = new MySqlConnection(ConnectionString))
            {
                try
                {
                    conexao.Open();

                    var cliente = conexao.QueryFirstOrDefault<Cliente>(@"
                           SELECT 
                                Id, 
                                nome, 
                                documento, 
                                CAST(IFNULL(datanascimento, STR_TO_DATE('01/01/1995', '%d/%m/%Y')) AS DATE) AS datanascimento, 
                                sexo 
                            FROM 
                                Cliente 
                            WHERE 
                                id = @id", new { Id = id });

                    return cliente;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocorreu um erro ao conectar: {ex.Message}");
                    return null;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public async Task DeletarCliente(int id)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                var sql = @"UPDATE Cliente SET 
                            ativo = 0
                        WHERE id = @id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}

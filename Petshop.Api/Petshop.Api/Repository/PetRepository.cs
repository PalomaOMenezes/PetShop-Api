using Dapper;
using MySqlConnector;
using Petshop.Api.Entidades;
using Petshop.Api.Interface;

namespace Petshop.Api.Repository
{
    public class PetRepository : IPetRepository
    {
        private string ConnectionString = "Server=localhost;Database=petshop;Uid=root;Pwd=teste;";

        public void CadastrarPet(Pet pet)
        {
            using (var conexao = new MySqlConnection(ConnectionString))
            {
                conexao.Open();

                string sql = "INSERT INTO pet (nome_pet, cliente_id, tipo_pet, descricao_breve) VALUES (@nomePet, @clienteId, @tipoPet, @descricaoBreve)";

                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("nomePet", pet.NomePet);
                    cmd.Parameters.AddWithValue("clienteId", pet.ClienteId);
                    cmd.Parameters.AddWithValue("tipoPet", pet.TipoPet);
                    cmd.Parameters.AddWithValue("descricaoBreve", pet.DescricaoBreve);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public async Task UpDatePet(int id, int clienteId, string nomePet, string tipoPet, string descricaoBreve)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = @"UPDATE Pet SET
                        nome_pet = @nomePet,
                        cliente_id = @clienteId,
                        tipo_pet = @tipoPet,
                        descricao_breve = @descricaoBreve
                        WHERE id = @id";

                connection.Execute(sql, new
                {
                        id = id,
                        nomePet = nomePet,
                        clienteId = clienteId,
                        tipoPet = tipoPet,
                        descricaoBreve = descricaoBreve
                }); 
            }
        }

        public Pet ConsultarPetById(int id)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open ();

                    var pet = connection.QueryFirstOrDefault<Pet>(@"
                           SELECT 
                                id,
                                cliente_id
                                nome_pet,
                                tipo_pet,
                                descricao_breve
                            FROM pet
                            WHERE id = @id" , new {id = id});

                    return pet;
                }
                catch
                {
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}

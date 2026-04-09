using System.Text;
using Dapper;
using MySqlConnector;
using Petshop.Api.Entidades;
using Petshop.Api.Interface;
using Petshop.Api.Models;

namespace Petshop.Api.Repository
{
    public class ResumoServicoRepository : IResumoServicoRepository
    {
        private string ConnectionString = "Server=localhost;Database=petshop;Uid=root;Pwd=teste;";

        public List<ResumoServico> MostrarServicosRealizados()
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                var listaservicosatendidos = connection.Query<ResumoServico>
                    (@"select 
                            a.id as Id,
                            c.nome as ClienteNome, 
                            p.nome_pet as PetNome, 
                            p.descricao_breve as PetDescricaoBreve, 
                            s.tipo as TipoServico
                        from atendimento a
               
                    inner join cliente c on a.cliente_id = c.id
                    inner join pet p on p.id = a.pet_id
                    inner join atendimentoservicos ats on ats.atendimento_id = a.id
                    inner join servicos s on ats.servico_id = s.id
                    
                    where a.ativo = 1"
                    );

                return listaservicosatendidos.ToList();
            }
        }


        public async Task<List<ResumoServico>> FiltrarResumo(string? clienteNome, string? petNome, string? tipoServico)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                // 1. Montamos a Query com JOINs para buscar dados de todas as tabelas
                var sql = new StringBuilder(@"
                      SELECT 
                        a.id AS Id,
		                c.nome AS ClienteNome,
		                p.nome_pet AS PetNome,
		                s.tipo AS TipoServico

                      FROM 
			                atendimentoservicos ats
                      inner join 
			                atendimento a On ats.atendimento_id = a.Id
	                  inner join 
			                servicos s on ats.servico_id = s.id
	                  inner join 
			                cliente c on a.cliente_id = c.id
	                  inner join 
			                pet p on a.pet_id = p.id
                WHERE 1=1 "); // O truque do 1=1 para facilitar os filtros

                var parametros = new DynamicParameters();

                // 2. Filtro de Cliente (Note o c.Nome para não confundir com o nome do Pet)
                if (!string.IsNullOrEmpty(clienteNome))
                {
                    sql.Append(" AND c.nome LIKE @Cliente");
                    parametros.Add("Cliente", $"%{clienteNome}%");
                }

                // 3. Filtro de Pet (Note o p.Nome)
                if (!string.IsNullOrEmpty(petNome))
                {
                    sql.Append(" AND p.nome_pet LIKE @Pet");
                    parametros.Add("Pet", $"%{petNome}%");
                }

                // 4. Filtro de Serviço (Note o s.Tipo ou s.Descricao, depende do seu banco)
                if (!string.IsNullOrEmpty(tipoServico))
                {
                    sql.Append(" AND s.Tipo LIKE @Servico");
                    parametros.Add("Servico", $"%{tipoServico}%");
                }

                // O Dapper faz o mapeamento automático porque usamos os 'AS' corretos
                var resultado = await connection.QueryAsync<ResumoServico>(sql.ToString(), parametros);

                return resultado.ToList();
            }
        }

        public async Task DeletarServicoRealizado(int id)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                var sql = @"UPDATE Atendimento SET 
                            ativo = 0
                        WHERE id = @id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task UpDateAtendimento(int id, int novoServicoId, string nome, string nomePet, string descricao)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = @"UPDATE atendimento a 

                               INNER JOIN cliente c ON a.cliente_id = c.id
                               INNER JOIN pet p ON a.pet_id = p.id
                               INNER JOIN atendimentoServicos ats ON ats.atendimento_id = a.id

                               SET

                                ats.servico_id = IF(@NovoServicoId = 0, ats.servico_id, @NovoServicoId),
                                c.nome = IF(IFNULL(@NovoNome, '') = '', c.nome, @NovoNome),
                                p.nome_pet = IF(IFNULL(@NovoNomePet, '') = '', p.nome_pet, @NovoNomePet),
                                p.descricao_breve = IF(IFNULL(@NovaDescricao, '') = '', p.descricao_breve, @NovaDescricao)

                               WHERE a.id = @atendimento_id";

                await connection.ExecuteAsync(sql, new
                {
                            AtendimentoId = id,
                            NovoServicoId = novoServicoId,
                            NovoNome = nome,
                            NovoNomePet = nomePet,
                            NovaDescricao = descricao
                }); 
            }
        }
    }
}

using Petshop.Api.Models;

namespace Petshop.Api.Interface
{
    public interface IResumoServicoRepository
    {
        List<ResumoServico> MostrarServicosRealizados();
        Task<List<ResumoServico>> FiltrarResumo(string? clienteNome, string? petNome, string? tipoServico);
        Task DeletarServicoRealizado(int id);
        Task UpDateAtendimento(int id, int novoServicoId, string nome, string nomePet, string descricao);
    }
}

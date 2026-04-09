using Petshop.Api.DTO;

namespace Petshop.Api.Interface
{
    public interface IResumoServicoService
    {
        List<ResumoServicoDTO> MostrarServicosRealizados();
        Task<List<ResumoServicoDTO>> FiltrarResumoDTO(string? clienteNome, string? petNome, string? tipoServico);
        Task DeletarServicoRealizado(int id);
        Task UpDateAtendimento(int id, int novoServicoId, string nome, string nomePet, string descricao);
    }
}

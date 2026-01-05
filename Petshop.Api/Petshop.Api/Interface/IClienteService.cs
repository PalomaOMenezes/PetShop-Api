using Petshop.Api.Domain;
using Petshop.Api.DTO;
using Petshop.Api.Entidades;

namespace Petshop.Api.Interface
{
    public interface IClienteService
    {
        void CadastrarCliente(ClienteDTO cliente);
        ClienteDTO ConsultarCliente(string documento);
        List<ClienteDTO> RetornarListarClientes();
        Task UpDateCliente(int id, string nome, string sexo, DateTime dataNascimento);
        ClienteDTO ConsultarClienteById(int id);
        Task DeletarCliente(int id);
    }
}

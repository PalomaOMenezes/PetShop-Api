using Petshop.Api.Domain;
using Petshop.Api.Entidades;

namespace Petshop.Api.Interface
{
    public interface IClienteRepository
    {
        void CadastrarCliente(Cliente cliente);
        Cliente ConsultarCliente(string documento);
        List<Cliente> RetornaListaClientes();
        Task UpDateCliente(int id, string nome, string sexo, DateTime dataNascimento);
        Cliente ConsultarClienteById(int id);
        Task DeletarCliente(int id);
    }
}

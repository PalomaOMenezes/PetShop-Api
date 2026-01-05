using Petshop.Api.DTO;
using Petshop.Api.Entidades;
using Petshop.Api.Interface;

namespace Petshop.Api.Domain
{
    public class ClienteService : IClienteService
    {

        //Aqui você está declarando uma variável privada, serve como um "espaço reservado" para guardar a ferramenta que acessa o banco de dados.
        private IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository repository) 
        {
            //Aqui você pega a ferramenta que recebeu (parâmetro repository) e a guarda no "bolso" da classe
            //(_clienteRepository) para usar depois em outros métodos.
            _clienteRepository = repository;
        }



        public void CadastrarCliente(ClienteDTO clienteDTO)
        {
            var cliente = new Cliente();

            cliente.Nome = clienteDTO.Nome;
            cliente.Documento = clienteDTO.Documento;
            cliente.DataNascimento = clienteDTO.DataNascimento;
            cliente.Sexo = clienteDTO.Sexo;

            //_clienteRepository é a ferramenta de banco de dados que você recebeu no construtor
            _clienteRepository.CadastrarCliente(cliente);
        }

        public ClienteDTO ConsultarCliente(string documento)
        {
            ClienteDTO clienteDTO = new ClienteDTO();

            var cliente = _clienteRepository.ConsultarCliente(documento);

            clienteDTO.Nome = cliente.Nome;
            clienteDTO.Documento = cliente.Documento;
            clienteDTO.DataNascimento = cliente.DataNascimento;
            clienteDTO.Sexo = cliente.Sexo;

            return clienteDTO;
        }

        public List<ClienteDTO> RetornarListarClientes()
        {

            var listaClientDomain = _clienteRepository.RetornaListaClientes();  //O Repositorio é resposavel por pegar itens no banco de dados

            var listaClienteDTO = new List<ClienteDTO>();

            foreach (var item in listaClientDomain)
            {
                ClienteDTO clientedto = new ClienteDTO();
                clientedto.Nome = item.Nome;
                clientedto.DataNascimento = item.DataNascimento;
                clientedto.Documento = item.Documento;
                clientedto.Sexo = item.Sexo;

                listaClienteDTO.Add(clientedto);
            }

            return listaClienteDTO;
        }
        
        public async Task UpDateCliente(int id, string nome, string sexo, DateTime dataNascimento)
        {
            var cliente = _clienteRepository.ConsultarClienteById(id);

            await _clienteRepository.UpDateCliente(id, nome, sexo, dataNascimento); //Apenas manda atualizar

        }

        public ClienteDTO ConsultarClienteById(int id)
        {
            ClienteDTO clienteDTO = new ClienteDTO();

            var cliente = _clienteRepository.ConsultarClienteById(id);

            clienteDTO.Nome = cliente.Nome;
            clienteDTO.Documento = cliente.Documento;
            clienteDTO.DataNascimento = cliente.DataNascimento;
            clienteDTO.Sexo = cliente.Sexo;

            return clienteDTO;
        }

        public async Task DeletarCliente(int id)
        {
            await _clienteRepository.DeletarCliente(id);
        }
    }
}

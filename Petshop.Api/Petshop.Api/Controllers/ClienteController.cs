using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petshop.Api.Domain;
using Petshop.Api.DTO;
using Petshop.Api.Interface;

namespace Petshop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : Controller
    {

        private IClienteService _clienteDomain; 

        public ClienteController(IClienteService repository)
        {
            _clienteDomain = repository;
        }


        [HttpPost]
        [AllowAnonymous]
        public void CadastrarCliente(ClienteDTO cliente) //POST SE USA DTO  (PASSAR O DADO VIA BODY)
        {
            _clienteDomain.CadastrarCliente(cliente);
        }

        [HttpGet("{documento}")] //Sempre passar o paramentro com o mesmo nome 
        [Authorize]
        public ClienteDTO ConsultarCliente(string documento) //GET  (PASSAR O DADO VIA QUERYSTRING)
        {

            var clienteDTO = _clienteDomain.ConsultarCliente(documento);

            return clienteDTO;

        }


        [HttpGet]
        [Authorize]
        public List<ClienteDTO> ConsultarListaDeCliente() //GET  (PASSAR O DADO VIA QUERYSTRING)
        {
            var clienteDTO = _clienteDomain.RetornarListarClientes();

            return clienteDTO;
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task UpDateCliente(int id, string nome, string sexo, DateTime dataNascimento)
        {
            await _clienteDomain.UpDateCliente(id, nome, sexo, dataNascimento);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task DeleteCliente(int id)
        {
            await _clienteDomain.DeletarCliente(id);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Petshop.Api.DTO;
using Petshop.Api.Entidades;
using Petshop.Api.Interface;

namespace Petshop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetController : Controller
    {
        private IPetService _petDomain;

        public PetController(IPetService repository)
        {
            _petDomain = repository;
        }

        [HttpPost]
        public void CadastrarPet(PetDTO pet)
        {
            _petDomain.CadastrarPet(pet);
        }

        [HttpPut("{id}")]
        public async Task UpDatePet(int id, int clienteId, string nomePet, string tipoPet, string descricaoBreve)
        {
            await _petDomain.UpDatePet(id, clienteId, nomePet, tipoPet, descricaoBreve);
        }
    }
}

using Petshop.Api.DTO;
using Petshop.Api.Entidades;

namespace Petshop.Api.Interface
{
    public interface IPetService
    {
        void CadastrarPet(PetDTO pet);
        PetDTO ConsultarPetById(int id);
        Task UpDatePet(int id, int clienteId, string nomePet, string tipoPet, string descricaoBreve);
    }
}

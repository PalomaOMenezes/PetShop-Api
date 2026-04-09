using Petshop.Api.Entidades;

namespace Petshop.Api.Interface
{
    public interface IPetRepository
    {
        void CadastrarPet(Pet pet);
        Pet ConsultarPetById(int id);
        Task UpDatePet(int id, int clienteId, string nomePet, string tipoPet, string descricaoBreve);
    }
}

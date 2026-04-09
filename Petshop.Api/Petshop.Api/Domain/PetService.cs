using Petshop.Api.DTO;
using Petshop.Api.Entidades;
using Petshop.Api.Interface;
using Petshop.Api.Repository;

namespace Petshop.Api.Domain
{
    public class PetService : IPetService
    {
        //Aqui você está declarando uma variável privada, serve como um "espaço reservado" para guardar a ferramenta que acessa o banco de dados.
        private IPetRepository _petRepository;

        public PetService(IPetRepository repository)
        {
            //Aqui você pega a ferramenta que recebeu (parâmetro repository) e a guarda no "bolso" da classe
            //(_petRepository) para usar depois em outros métodos.
            _petRepository = repository;
        }

        public void CadastrarPet(PetDTO petDTO)
        {
            var pet = new Pet();

            pet.NomePet = petDTO.NomePet;
            pet.ClienteId = petDTO.ClienteId;
            pet.TipoPet = petDTO.TipoPet;
            pet.DescricaoBreve = petDTO.DescricaoBreve;

            _petRepository.CadastrarPet(pet);
        }

        public PetDTO ConsultarPetById(int id)
        {
            PetDTO petDTO = new PetDTO();

            var pet = _petRepository.ConsultarPetById(id);

            petDTO.NomePet = pet.NomePet;
            petDTO.ClienteId = pet.ClienteId;
            petDTO.TipoPet = pet.TipoPet;
            petDTO.DescricaoBreve = pet.DescricaoBreve;

            return petDTO;
        }

        public async Task UpDatePet(int id, int clienteId, string nomePet, string tipoPet, string descricaoBreve)
        {
            var pet = _petRepository.ConsultarPetById(id);

            switch (tipoPet)
            {
                case "pequeno":
                    await _petRepository.UpDatePet(id, clienteId, nomePet, "0", descricaoBreve);
                    break;

                case "medio":
                    await _petRepository.UpDatePet(id, clienteId, nomePet, "1", descricaoBreve);
                    break;

                case "grande":
                    await _petRepository.UpDatePet(id, clienteId, nomePet, "2", descricaoBreve);
                    break;

                default:
                    break;
            }          
        }
    }
}

using Microsoft.AspNetCore.Mvc.Formatters;
using Petshop.Api.DTO;
using Petshop.Api.Entidades;
using Petshop.Api.Interface;
using Petshop.Api.Models;
using Petshop.Api.Repository;

namespace Petshop.Api.Domain
{
    public class ResumoServicoService : IResumoServicoService
    {
        //Aqui você está declarando uma variável privada, serve como um "espaço reservado" para guardar a ferramenta que acessa o banco de dados.
        private IResumoServicoRepository _resumoRepository;

        public ResumoServicoService(IResumoServicoRepository repository)
        {
            //Aqui você pega a ferramenta que recebeu (parâmetro repository) e a guarda no "bolso" da classe
            //(_resumoRepository) para usar depois em outros métodos.
            _resumoRepository = repository;
        }

        public List<ResumoServicoDTO> MostrarServicosRealizados()
        {
            var listaservicos = _resumoRepository.MostrarServicosRealizados();  //O Repositorio é resposavel por pegar itens no banco de dados

            var listaservicosDTO = new List<ResumoServicoDTO>();

            foreach (var item in listaservicos)
            {
                ResumoServicoDTO resumodto = new ResumoServicoDTO();
                resumodto.Id = item.Id;
                resumodto.ClienteNome = item.ClienteNome;
                resumodto.PetNome = item.PetNome;
                resumodto.PetDescricaoBreve = item.PetDescricaoBreve;
                resumodto.TipoServico = item.TipoServico;

                listaservicosDTO.Add(resumodto);
            }

            return listaservicosDTO;
        }

        public async Task<List<ResumoServicoDTO>> FiltrarResumoDTO(string? clienteNome, string? petNome, string? tipoServico)
        {
            var listaServicos = await _resumoRepository.FiltrarResumo(clienteNome, petNome, tipoServico);


            var listaFiltradaDTO = new List<ResumoServicoDTO>();

            foreach (var item in listaServicos)
            {
                ResumoServicoDTO resumoServicoDTO = new ResumoServicoDTO();

                resumoServicoDTO.Id = item.Id;
                resumoServicoDTO.ClienteNome = item.ClienteNome;
                resumoServicoDTO.PetNome = item.PetNome;
                resumoServicoDTO.TipoServico = item.TipoServico;

                listaFiltradaDTO.Add(resumoServicoDTO);

            }

            return listaFiltradaDTO;
        }

        public async Task DeletarServicoRealizado(int id)
        {
            await _resumoRepository.DeletarServicoRealizado(id);
        }

        public async Task UpDateAtendimento(int id, int novoServicoId, string nome, string nomePet, string descricao)
        {
            await _resumoRepository.UpDateAtendimento(id, novoServicoId, nome, nomePet, descricao);
        }
    }
}

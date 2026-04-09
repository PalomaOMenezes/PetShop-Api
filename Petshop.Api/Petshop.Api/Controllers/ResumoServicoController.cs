using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Petshop.Api.DTO;
using Petshop.Api.Interface;

namespace Petshop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ResumoServicoController : Controller
    {
        private IResumoServicoService _resumomodel;

        public ResumoServicoController(IResumoServicoService repository)
        {
            _resumomodel = repository;
        }


        [HttpGet]
        
        public List<ResumoServicoDTO> MostrarServicosRealizados()
        {
            var listaservicos = _resumomodel.MostrarServicosRealizados();

            return listaservicos;
        }

        [HttpGet("pesquisar")]
        [Authorize]
        public async Task<IActionResult> Pesquisar(
            [FromQuery] string? cliente,
            [FromQuery] string? pet,
            [FromQuery] string? servico)
        {
            // Passa os filtros para o repositório
            var lista = await _resumomodel.FiltrarResumoDTO(cliente, pet, servico);

            return Ok(lista);
        }

        [HttpDelete("{id}")]
        public async Task DeletarServicoRealizado(int id)
        {
            await _resumomodel.DeletarServicoRealizado(id);
        }

        [HttpPut("{id}")]
        public async Task UpDateAtendimento(int id, int novoServicoId, string nome, string nomePet, string descricao)
        {
            await _resumomodel.UpDateAtendimento(id, novoServicoId, nome, nomePet, descricao);
        }
    }
}

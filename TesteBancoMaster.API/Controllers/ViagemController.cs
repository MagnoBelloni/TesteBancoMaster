using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TesteBancoMaster.API.Models;
using TesteBancoMaster.API.Services.Interfaces;

namespace TesteBancoMaster.API.Controllers
{
    [Route("api/viagem")]
    [ApiController]
    public class ViagemController : ControllerBase
    {
        private readonly IViagemService _viagemService;

        public ViagemController(IViagemService viagemService)
        {
            _viagemService = viagemService;
        }

        /// <summary>
        /// Lista os todas as viagens
        /// </summary>
        /// <returns>Informações das viagens</returns>
        /// <response code="200">Retorna todas as viagens cadastradas</response>
        [HttpGet]
        public async Task<IActionResult> ObterViagens()
        {
            var resultado = await _viagemService.ObterTodos();
            return Ok(resultado);
        }

        /// <summary>
        /// Lista os possiveis destinos com base na origem
        /// </summary>
        /// <returns>Informações das viagens</returns>
        /// <response code="200">Retorna as viagens cadastradas</response>
        [HttpGet("{origem}")]
        public async Task<IActionResult> ObterDestinos([FromRoute][Required] string origem)
        {
            var resultado = await _viagemService.ObterDestinos(origem);
            return Ok(resultado);
        }

        /// <summary>
        /// Cadastra uma nova viagem
        /// </summary>
        /// <returns>Informações das viagens</returns>
        /// <response code="200">Retorna a viagens cadastrada</response>
        [HttpPost]
        public async Task<IActionResult> CadastrarViagem([FromBody] ViagemCadastroModelRequest request)
        {
            var resultado = await _viagemService.CadastrarViagem(request);
            return Ok(resultado);
        }

        /// <summary>
        /// Atualiza uma viagem cadastrada anteriormente
        /// </summary>
        /// <returns>Informações das viagens</returns>
        /// <response code="200">Retorna a viagem atualizada</response>
        [HttpPut("{idOrigem}")]
        public async Task<IActionResult> AtualizarViagem(
            [FromRoute] int idOrigem,
            [FromBody] ViagemAtualizarModelRequest request)
        {
            var resultado = await _viagemService.AtualizarViagem(idOrigem, request);

            return Ok(resultado);
        }

        /// <summary>
        /// Deleta uma viagem cadastrada anteriormente
        /// </summary>
        /// <returns>Informações das viagens</returns>
        /// <response code="200">Sucesso</response>
        [HttpDelete("{idOrigem}")]
        public async Task<IActionResult> DeletarViagem([FromRoute][Required] int idOrigem)
        {
            var resultado = await _viagemService.DeletarViagem(idOrigem);
            return Ok(resultado);
        }

        /// <summary>
        /// Procura a viagem com o custo mais baixo com base na origem e destino
        /// </summary>
        /// <returns>Lista com as possiveis rotas e a rota mais barata.</returns>
        /// <response code="200">Retorna as possiveis rotas.</response>
        [HttpPost("custoBaixo")]
        public async Task<IActionResult> ObterRotaCustoBaixo([FromBody] ViagemObterRotaCustoBaixoModelRequest request)
        {
            try
            {
                var resultado = await _viagemService.ObterRotaCustoBaixo(request);
                return Ok(resultado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}

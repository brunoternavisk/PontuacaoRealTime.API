using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PontuacaoRealTime.API.Domain.Entities;
using PontuacaoRealTime.API.Domain.Dtos;
using PontuacaoRealTime.API.Data;
using PontuacaoRealTime.API.Domain.Interfaces;


namespace PontuacaoRealTime.API.Controllers
{
    [Route("api/pontos")]
    [ApiController]
    public class PontosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPontosService _pontosService;

        public PontosController(IPontosService  pontosService, AppDbContext context)
        {
            _pontosService = pontosService;
            _context = context;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarPontos([FromBody] ConsumoInputModelDTO input)
        {
            var consumo = new ConsumoEntity
            {
                PessoaId = input.PessoaId,
                DataConsumo = input.DataConsumo,
                ValorTotal = input.ValorTotal
            };

            var pontosGanhos = (int)(consumo.ValorTotal / 10);

            await _pontosService.RegistrarPontosAsync(consumo);

            // Recupera o saldo atualizado do banco para resposta
            var saldo = await _context.Pontos
                .Where(p => p.PessoaId == input.PessoaId)
                .Select(p => p.Saldo)
                .FirstOrDefaultAsync();

            var response = new RegistroPontosResponseDTO
            {
                PessoaId = input.PessoaId,
                PontosGanhos = pontosGanhos,
                SaldoAtual = saldo
            };

            return Ok(response);
        }
        
        [HttpGet("extrato/{pessoaId}")]
        public async Task<IActionResult> ObterExtrato(
            int pessoaId,
            [FromQuery] int dias = 30,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var response = await _pontosService.ObterExtratoAsync(pessoaId, dias, page, pageSize);

            if (response.Extrato == null || response.Extrato.Count == 0)
                return NotFound(new { message = "Nenhum registro encontrado para este consumidor." });

            return Ok(response);
        }
    }
}
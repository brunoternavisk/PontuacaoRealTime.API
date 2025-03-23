using PontuacaoRealTime.API.Domain.Entities;
using PontuacaoRealTime.API.Domain.Dtos;
using PontuacaoRealTime.API.Domain.Interfaces;

namespace PontuacaoRealTime.API.Services
{
    public class PontosService : IPontosService
    {
        private readonly IPontosRepository _pontosRepository;

        public PontosService(IPontosRepository pontosRepository)
        {
            _pontosRepository = pontosRepository;
        }

        public async Task RegistrarPontosAsync(ConsumoEntity consumoEntity)
        {
            await _pontosRepository.RegistrarPontosAsync(consumoEntity);
        }
        
        public async Task<ExtratoPontosResponseDTO> ObterExtratoAsync(int pessoaId, int dias, int page, int pageSize)
        {
            var dataLimite = DateTime.UtcNow.AddDays(-dias);

            var extratoCompleto = await _pontosRepository.BuscarExtratoAsync(pessoaId, dataLimite);

            var totalRegistros = extratoCompleto.Count;
            var totalPaginas = (int)Math.Ceiling(totalRegistros / (double)pageSize);
            var extratoPaginado = extratoCompleto
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var saldo = await _pontosRepository.BuscarSaldoAsync(pessoaId);

            return new ExtratoPontosResponseDTO
            {
                PessoaId = pessoaId,
                SaldoAtual = saldo,
                Extrato = extratoPaginado,
                PaginaAtual = page,
                TotalPaginas = totalPaginas,
                TotalRegistros = totalRegistros
            };
        }

    }
}
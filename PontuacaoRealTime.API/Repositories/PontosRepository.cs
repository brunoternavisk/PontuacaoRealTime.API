using Microsoft.EntityFrameworkCore;
using PontuacaoRealTime.API.Data;
using PontuacaoRealTime.API.Domain.Entities;
using PontuacaoRealTime.API.Domain.Dtos;
using PontuacaoRealTime.API.Domain.Interfaces;

namespace PontuacaoRealTime.API.Repositories
{
    public class PontosRepository : IPontosRepository
    {
        private readonly AppDbContext _context;

        public PontosRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task RegistrarPontosAsync(ConsumoEntity consumo)
        {
            var pontosGanhos = (int)(consumo.ValorTotal / 10);

            // 1. Salvar o consumo primeiro
            await _context.Consumos.AddAsync(consumo);
            await _context.SaveChangesAsync();

            // 2. Criar registro no Memorial
            var memorial = new MemorialEntity
            {
                ConsumoId = consumo.Id,
                ValorTotal = consumo.ValorTotal,
                ValorPontuavel = consumo.ValorTotal,
                PontosObtidos = pontosGanhos,
                DataCriacao = DateTime.UtcNow
            };

            await _context.Memorial.AddAsync(memorial);
            await _context.SaveChangesAsync();

            // 3. Atualizar saldo de pontos
            var pontos = await _context.Pontos
                .FirstOrDefaultAsync(p => p.PessoaId == consumo.PessoaId);

            if (pontos == null)
            {
                pontos = new PontosEntity
                {
                    PessoaId = consumo.PessoaId,
                    Saldo = pontosGanhos,
                    DataAtualizacao = DateTime.UtcNow
                };
                await _context.Pontos.AddAsync(pontos);
            }
            else
            {
                pontos.Saldo += pontosGanhos;
                pontos.DataAtualizacao = DateTime.UtcNow;
                _context.Pontos.Update(pontos);
            }

            await _context.SaveChangesAsync();
        }
        
        public async Task<List<ExtratoPontosDTO>> BuscarExtratoAsync(int pessoaId, DateTime dataLimite)
        {
            return await (from consumo in _context.Consumos
                join memorial in _context.Memorial
                    on consumo.Id equals memorial.ConsumoId
                where consumo.PessoaId == pessoaId
                      && consumo.DataConsumo >= dataLimite
                orderby consumo.DataConsumo descending
                select new ExtratoPontosDTO
                {
                    DataConsumo = consumo.DataConsumo,
                    ValorTotal = consumo.ValorTotal,
                    PontosObtidos = memorial.PontosObtidos
                }).ToListAsync();
        }

        public async Task<int> BuscarSaldoAsync(int pessoaId)
        {
            return await _context.Pontos
                .Where(p => p.PessoaId == pessoaId)
                .Select(p => p.Saldo)
                .FirstOrDefaultAsync();
        }
    }
}
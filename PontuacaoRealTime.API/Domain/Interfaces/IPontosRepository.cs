using PontuacaoRealTime.API.Domain.Entities;
using System.Threading.Tasks;
using PontuacaoRealTime.API.Domain.Dtos;

namespace PontuacaoRealTime.API.Domain.Interfaces
{
    public interface IPontosRepository
    {
        Task RegistrarPontosAsync(ConsumoEntity consumo);
        Task<List<ExtratoPontosDTO>> BuscarExtratoAsync(int pessoaId, DateTime dataLimite);
        Task<int> BuscarSaldoAsync(int pessoaId);
    }
}

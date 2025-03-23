using PontuacaoRealTime.API.Domain.Entities;
using PontuacaoRealTime.API.Domain.Dtos;

namespace PontuacaoRealTime.API.Domain.Interfaces
{
    public interface IPontosService
    {
        Task RegistrarPontosAsync(ConsumoEntity consumoEntity);

        Task<ExtratoPontosResponseDTO> ObterExtratoAsync(
            int pessoaId,
            int dias,
            int page,
            int pageSize
        );
    }
}
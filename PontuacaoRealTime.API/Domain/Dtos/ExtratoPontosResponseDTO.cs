namespace PontuacaoRealTime.API.Domain.Dtos
{
    public class ExtratoPontosResponseDTO
    {
        public int PessoaId { get; set; }
        public int SaldoAtual { get; set; }
        public List<ExtratoPontosDTO> Extrato { get; set; }
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalRegistros { get; set; }
    }
}
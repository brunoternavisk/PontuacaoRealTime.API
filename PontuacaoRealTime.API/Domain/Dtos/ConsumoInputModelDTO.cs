namespace PontuacaoRealTime.API.Domain.Dtos
{
    public class ConsumoInputModelDTO
    {
        public int PessoaId { get; set; }
        public DateTime DataConsumo { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
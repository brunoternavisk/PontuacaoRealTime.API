namespace PontuacaoRealTime.API.Domain.Dtos
{
    public class ExtratoPontosDTO
    {
        public DateTime DataConsumo { get; set; }
        public decimal ValorTotal { get; set; }
        public int PontosObtidos { get; set; }
    }
}
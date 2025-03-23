namespace PontuacaoRealTime.API.Domain.Entities
{
    public class MemorialEntity
    {
        public int Id { get; set; } // id_registro_memorial
        public int ConsumoId { get; set; } // FK para Consumo
        public decimal ValorTotal { get; set; }
        public decimal ValorPontuavel { get; set; }
        public int PontosObtidos { get; set; } // pontos_obtidos_na_transacao
        public DateTime DataCriacao { get; set; } // data_criacao_registro_memorial

        // Navegação
        public ConsumoEntity ConsumoEntity { get; set; }
    }
}
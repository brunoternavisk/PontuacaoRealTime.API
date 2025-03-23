namespace PontuacaoRealTime.API.Domain.Entities
{
    public class ConsumoEntity
    {
        public int Id { get; set; } // id_consumo
        public int PessoaId { get; set; } // id_pessoa_que consumiu
        public DateTime DataConsumo { get; set; }
        public decimal ValorTotal { get; set; }

        // Navegação
        public ICollection<MemorialEntity> RegistrosMemorial { get; set; }
    }
}
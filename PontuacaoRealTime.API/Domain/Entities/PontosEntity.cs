namespace PontuacaoRealTime.API.Domain.Entities
{
    public class PontosEntity
    {
        public int Id { get; set; } // id_cartela_pontos
        public int PessoaId { get; set; } // id_pessoa_que consumiu
        public int Saldo { get; set; } // saldo_de_pontos
        public DateTime DataAtualizacao { get; set; }
    }
}
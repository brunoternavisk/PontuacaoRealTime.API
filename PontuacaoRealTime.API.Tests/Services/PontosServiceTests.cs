using Moq;
using PontuacaoRealTime.API.Domain.Dtos;
using PontuacaoRealTime.API.Domain.Entities;
using PontuacaoRealTime.API.Domain.Interfaces;
using PontuacaoRealTime.API.Services;
using Xunit;

namespace PontuacaoRealTime.API.Tests.Services
{
    public class PontosServiceTests
    {
        [Fact]
        public async Task RegistrarPontos_DeveChamarRepositoryComConsumo()
        {
            // Arrange
            var repoMock = new Mock<IPontosRepository>();
            var service = new PontosService(repoMock.Object);
            var consumo = new ConsumoEntity
            {
                PessoaId = 1,
                DataConsumo = DateTime.UtcNow,
                ValorTotal = 100
            };

            // Act
            await service.RegistrarPontosAsync(consumo);

            // Assert
            repoMock.Verify(r => r.RegistrarPontosAsync(consumo), Times.Once);
        }

        [Fact]
        public async Task ObterExtratoAsync_DeveRetornarExtratoPaginadoCorretamente()
        {
            // Arrange
            var repoMock = new Mock<IPontosRepository>();
            var pessoaId = 1;
            var dias = 30;
            var page = 1;
            var pageSize = 2;

            var extratoMock = new List<ExtratoPontosDTO>
            {
                new() { DataConsumo = DateTime.UtcNow.AddDays(-1), ValorTotal = 100, PontosObtidos = 10 },
                new() { DataConsumo = DateTime.UtcNow.AddDays(-2), ValorTotal = 200, PontosObtidos = 20 },
                new() { DataConsumo = DateTime.UtcNow.AddDays(-3), ValorTotal = 300, PontosObtidos = 30 }
            };

            repoMock.Setup(r => r.BuscarExtratoAsync(pessoaId, It.IsAny<DateTime>()))
                .ReturnsAsync(extratoMock);

            repoMock.Setup(r => r.BuscarSaldoAsync(pessoaId))
                .ReturnsAsync(60);

            var service = new PontosService(repoMock.Object);

            // Act
            var result = await service.ObterExtratoAsync(pessoaId, dias, page, pageSize);

            // Assert
            Assert.Equal(pessoaId, result.PessoaId);
            Assert.Equal(60, result.SaldoAtual);
            Assert.Equal(1, result.PaginaAtual);
            Assert.Equal(2, result.TotalPaginas); // 3 registros, 2 por página
            Assert.Equal(3, result.TotalRegistros);
            Assert.Equal(2, result.Extrato.Count); // Deve retornar apenas os 2 primeiros
        }
        
        [Fact]
        public async Task ObterExtratoAsync_PaginaDois_DeveRetornarUltimoItem()
        {
            // Arrange
            var repoMock = new Mock<IPontosRepository>();
            var pessoaId = 1;
            var dias = 30;
            var page = 2;
            var pageSize = 2;

            var extratoMock = new List<ExtratoPontosDTO>
            {
                new() { DataConsumo = DateTime.UtcNow.AddDays(-1), ValorTotal = 100, PontosObtidos = 10 },
                new() { DataConsumo = DateTime.UtcNow.AddDays(-2), ValorTotal = 200, PontosObtidos = 20 },
                new() { DataConsumo = DateTime.UtcNow.AddDays(-3), ValorTotal = 300, PontosObtidos = 30 }
            };

            repoMock.Setup(r => r.BuscarExtratoAsync(pessoaId, It.IsAny<DateTime>()))
                .ReturnsAsync(extratoMock);

            repoMock.Setup(r => r.BuscarSaldoAsync(pessoaId))
                .ReturnsAsync(60);

            var service = new PontosService(repoMock.Object);

            // Act
            var result = await service.ObterExtratoAsync(pessoaId, dias, page, pageSize);

            // Assert
            Assert.Equal(1, result.Extrato.Count); // Página 2 deve ter 1 item
            Assert.Equal(30, result.Extrato.First().PontosObtidos);
        }

        [Fact]
        public async Task ObterExtratoAsync_ListaVazia_DeveRetornarExtratoVazio()
        {
            // Arrange
            var repoMock = new Mock<IPontosRepository>();
            var pessoaId = 1;
            var dias = 30;
            var page = 1;
            var pageSize = 10;

            repoMock.Setup(r => r.BuscarExtratoAsync(pessoaId, It.IsAny<DateTime>()))
                .ReturnsAsync(new List<ExtratoPontosDTO>());

            repoMock.Setup(r => r.BuscarSaldoAsync(pessoaId))
                .ReturnsAsync(0);

            var service = new PontosService(repoMock.Object);

            // Act
            var result = await service.ObterExtratoAsync(pessoaId, dias, page, pageSize);

            // Assert
            Assert.Empty(result.Extrato);
            Assert.Equal(0, result.SaldoAtual);
            Assert.Equal(0, result.TotalPaginas);
            Assert.Equal(0, result.TotalRegistros);
        }

        [Fact]
        public async Task ObterExtratoAsync_SaldoZero_DeveRetornarSaldoZero()
        {
            // Arrange
            var repoMock = new Mock<IPontosRepository>();
            var pessoaId = 1;
            var dias = 30;
            var page = 1;
            var pageSize = 10;

            var extratoMock = new List<ExtratoPontosDTO>
            {
                new() { DataConsumo = DateTime.UtcNow.AddDays(-5), ValorTotal = 100, PontosObtidos = 10 }
            };

            repoMock.Setup(r => r.BuscarExtratoAsync(pessoaId, It.IsAny<DateTime>()))
                .ReturnsAsync(extratoMock);

            repoMock.Setup(r => r.BuscarSaldoAsync(pessoaId))
                .ReturnsAsync(0);

            var service = new PontosService(repoMock.Object);

            // Act
            var result = await service.ObterExtratoAsync(pessoaId, dias, page, pageSize);

            // Assert
            Assert.Single(result.Extrato);
            Assert.Equal(0, result.SaldoAtual);
        }
    }
}

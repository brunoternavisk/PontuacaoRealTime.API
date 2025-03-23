
# 🧠 Pontuação em Tempo Real – API Superbus

Este projeto é uma proposta de solução para o desafio técnico da empresa **Senex Superbus**, com o objetivo de implementar um sistema de pontuação em tempo real que coexista com o processamento noturno já existente.

---

## 📌 Descrição da Solução

A solução foi desenvolvida em **ASP.NET Core (C#)** com **SQLite** para simular a persistência local. Ela permite registrar uma transação de consumo, calcular os pontos obtidos no momento da compra e atualizar o saldo total do consumidor. Os dados são armazenados nas mesmas estruturas do sistema legado:

- `CONSUMO` – Registro da compra  
- `MEMORIAL` – Histórico de pontos por transação  
- `PONTOS` – Saldo acumulado por consumidor  

A lógica aplicada replica fielmente o modelo de pontuação do lote (**1 ponto a cada R$10**), garantindo **consistência de dados** mesmo com a coexistência dos dois processos.

---

## 🚀 Como executar

1. Clone o repositório
2. Restaure os pacotes NuGet:

   ```bash
   dotnet restore
   ```

3. Execute as migrations:

   ```bash
   dotnet ef database update
   ```

4. Rode o projeto:

   ```bash
   dotnet run
   ```

5. Acesse via Swagger:  
   👉 [http://localhost:5085/swagger](http://localhost:5085/swagger)

---

## 📬 Endpoints principais

### `POST /api/pontos/registrar`

Registra um consumo e retorna os pontos gerados e o saldo atual.

```json
{
  "pessoaId": 1,
  "dataConsumo": "2025-03-23T14:00:00",
  "valorTotal": 120.00
}
```

---

### `GET /api/pontos/extrato/{pessoaId}?dias=30&page=1&pageSize=10`

Retorna o extrato paginado do consumidor com saldo atual e total de pontos gerados por transação.

---

## 📊 Diagrama técnico (alto nível)

📂 Arquivo disponível em PDF na pasta `PontuacaoRealTime.API/docs/`:

📎 [`PontuacaoRealTime.API/docs/FluxoPontuacaoTempoReal.pdf`](./PontuacaoRealTime.API/docs/FluxoPontuacaoTempoReal.pdf)

---

## 🧱 Estrutura do projeto

```
📁 Controllers
   └── PontosController.cs
📁 Data
   ├── AppDbContext.cs
   └── DatabaseConfig.cs
📁 Domain
   📁 Dtos
       ├── ConsumoInputModelDTO.cs
       ├── RegistroPontosResponseDTO.cs
       └── ExtratoPontosResponseDTO.cs
   📁 Entities
       ├── ConsumoEntity.cs
       ├── PontosEntity.cs
       └── MemorialEntity.cs
   📁 Interfaces
       ├── IPontosRepository.cs
       └── IPontosService.cs
📁 Repositories
   └── PontosRepository.cs
📁 Services
   └── PontosService.cs
📁 docs
   └── FluxoPontuacaoTempoReal.pdf
📁 Migrations
📁 PontuacaoRealTime.API.Tests
   📁 Services
       └── PontosServiceTests.cs

```

---

## ✅ Considerações finais

- A aplicação está preparada para uso real com injeção de dependência e boas práticas de design.
- O fluxo evita inconsistências entre pontuação real-time e processamento noturno.
- Swagger implementado para facilitar testes com qualquer ferramenta (Postman, Insomnia, etc).
- Estrutura modular e extensível, pronta para evolução ou desacoplamento em microsserviços.
- Testes unitários implementados para garantir a confiabilidade da lógica de negócio.
---

Desenvolvido com 💙 para o desafio técnico da **Senex Superbus**.

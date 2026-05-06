using OficinaAPI.Data;
using OficinaAPI.DTOs;
using OficinaAPI.Models;

namespace OficinaAPI.Services
{
    public class OrcamentoService : IOrcamentoService
    {
        private readonly AppDbContext _db;
        public OrcamentoService(AppDbContext db)
        {
            _db = db;
        }
        public (bool Sucesso, string Mensagem, decimal? ValorTotal) CriarOrcamento(CriarOrcamentoRequest request)
        {
            // Validações
            if (request.ClienteId <= 0)
                return (false, "clienteId é obrigatório", null);

            if (request.VeiculoId <= 0)
                return (false, "veiculoId é obrigatório", null);

            if (request.Itens == null || !request.Itens.Any())
                return (false, "O orçamento deve possuir pelo menos um item", null);

            foreach (var item in request.Itens)
            {
                if (string.IsNullOrWhiteSpace(item.Descricao))
                    return (false, "Item com descrição inválida", null);

                if (item.Quantidade <= 0)
                    return (false, "Quantidade deve ser maior que zero", null);

                if (item.ValorUnitario <= 0)
                    return (false, "Valor unitário deve ser maior que zero", null);
            }

            // Cálculo do total
            decimal valorTotal = request.Itens
                .Sum(i => i.Quantidade * i.ValorUnitario);

            var orcamento = new Orcamento
            {
                ClienteId = request.ClienteId,
                VeiculoId = request.VeiculoId,
                Status = "Aberto",
                DataCriacao = DateTime.Now,
                Itens = request.Itens.Select(i => new OrcamentoItem
                {
                    Descricao = i.Descricao,
                    Quantidade = i.Quantidade,
                    ValorUnitario = i.ValorUnitario,
                    ValorTotal = i.Quantidade * i.ValorUnitario
                }).ToList()
            };

            orcamento.ValorTotal = orcamento.Itens.Sum(i => i.ValorTotal);

            _db.Orcamentos.Add(orcamento);
            _db.SaveChanges();

            return (true, "Orçamento criado com sucesso", orcamento.ValorTotal);
        }
    }
}
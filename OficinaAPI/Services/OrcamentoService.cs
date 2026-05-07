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

        public (bool Sucesso, string Mensagem, decimal? ValorTotal, int? OrcamentoId)
            CriarOrcamento(CriarOrcamentoRequest request)
        {
            // Validações
            if (request.ClienteId <= 0)
                return (false, "clienteId é obrigatório", null, null);

            if (request.VeiculoId <= 0)
                return (false, "veiculoId é obrigatório", null, null);

            if (request.Itens == null || !request.Itens.Any())
                return (false, "O orçamento deve possuir pelo menos um item", null, null);

            foreach (var item in request.Itens)
            {
                if (string.IsNullOrWhiteSpace(item.Descricao))
                    return (false, "Item com descrição inválida", null, null);

                if (item.Quantidade <= 0)
                    return (false, "Quantidade deve ser maior que zero", null, null);

                if (item.ValorUnitario <= 0)
                    return (false, "Valor unitário deve ser maior que zero", null, null);
            }

            // Criação do orçamento
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

            // Cálculo do total
            orcamento.ValorTotal = orcamento.Itens.Sum(i => i.ValorTotal);

            // Persistência
            _db.Orcamentos.Add(orcamento);

            _db.SaveChanges();

            // Retorno
            return (
                true,
                "Orçamento criado com sucesso",
                orcamento.ValorTotal,
                orcamento.Id
            );
        }

        public (bool Sucesso, string Mensagem, decimal? ValorTotal)
            AtualizarOrcamento(
                Orcamento orcamento,
                CriarOrcamentoRequest request)
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

            // Atualização dos dados
            orcamento.ClienteId = request.ClienteId;
            orcamento.VeiculoId = request.VeiculoId;

            // Remove itens antigos
            orcamento.Itens.Clear();

            // Adiciona novos itens
            foreach (var item in request.Itens)
            {
                orcamento.Itens.Add(new OrcamentoItem
                {
                    Descricao = item.Descricao,
                    Quantidade = item.Quantidade,
                    ValorUnitario = item.ValorUnitario,
                    ValorTotal = item.Quantidade * item.ValorUnitario
                });
            }

            // Recalcula total
            orcamento.ValorTotal = orcamento.Itens.Sum(i => i.ValorTotal);

            return (
                true,
                "Orçamento atualizado com sucesso",
                orcamento.ValorTotal
            );
        }
    }
}
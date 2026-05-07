using OficinaAPI.DTOs;
using OficinaAPI.Models;

namespace OficinaAPI.Services
{
    public interface IOrcamentoService
    {
        (bool Sucesso, string Mensagem, decimal? ValorTotal, int? OrcamentoId)
            CriarOrcamento(CriarOrcamentoRequest request);

        (bool Sucesso, string Mensagem, decimal? ValorTotal)
            AtualizarOrcamento(
                Orcamento orcamento,
                CriarOrcamentoRequest request);
    }
}
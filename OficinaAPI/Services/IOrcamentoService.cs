using OficinaAPI.DTOs;

namespace OficinaAPI.Services
{
    public interface IOrcamentoService
    {
        (bool Sucesso, string Mensagem, decimal? ValorTotal) CriarOrcamento(CriarOrcamentoRequest request);
    }
}
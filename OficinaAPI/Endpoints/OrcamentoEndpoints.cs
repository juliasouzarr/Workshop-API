using OficinaAPI.Data;
using OficinaAPI.DTOs;
using OficinaAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace OficinaAPI.Endpoints
{
    public static class OrcamentoEndpoints
    {
        public static void MapOrcamentoEndpoints(this WebApplication app)
        {
            app.MapPost("/orcamentos", (CriarOrcamentoRequest request, IOrcamentoService service) =>
            {
                var resultado = service.CriarOrcamento(request);

                if (!resultado.Sucesso)
                    return Results.BadRequest(new { erro = resultado.Mensagem });

                return Results.Ok(new
                {
                    mensagem = resultado.Mensagem,
                    valorTotal = resultado.ValorTotal
                });
            });

            app.MapGet("/orcamentos", (AppDbContext db) =>
            {
                var orcamentos = db.Orcamentos
                    .Select(o => new
                    {
                        o.Id,
                        o.ClienteId,
                        o.VeiculoId,
                        o.ValorTotal,
                        o.Status
                    })
                    .ToList();

                return Results.Ok(orcamentos);
            });
        }
    }
}   

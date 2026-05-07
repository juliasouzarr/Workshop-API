using Microsoft.EntityFrameworkCore;
using OficinaAPI.Data;
using OficinaAPI.DTOs;
using OficinaAPI.Services;

namespace OficinaAPI.Endpoints
{
    public static class OrcamentoEndpoints
    {
        public static void MapOrcamentoEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/orcamentos")
                .WithTags("Orcamentos");

            // GET ALL
            group.MapGet("/", async (AppDbContext db) =>
            {
                var orcamentos = await db.Orcamentos
                    .AsNoTracking()
                    .Select(o => new
                    {
                        o.Id,
                        o.ClienteId,
                        o.VeiculoId,
                        o.ValorTotal,
                        o.Status
                    })
                    .ToListAsync();

                return Results.Ok(orcamentos);
            })
            .WithName("GetAllOrcamentos")
            .Produces(StatusCodes.Status200OK);

            // GET BY ID
            group.MapGet("/{id}", async (int id, AppDbContext db) =>
            {
                var orcamento = await db.Orcamentos
                    .Include(o => o.Itens)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (orcamento is null)
                {
                    return Results.NotFound(new
                    {
                        erro = "Orçamento não encontrado."
                    });
                }

                return Results.Ok(orcamento);
            })
            .WithName("GetOrcamentoById")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            // CREATE
            group.MapPost("/", (
                CriarOrcamentoRequest request,
                IOrcamentoService service) =>
            {
                var resultado = service.CriarOrcamento(request);

                if (!resultado.Sucesso)
                {
                    return Results.BadRequest(new
                    {
                        erro = resultado.Mensagem
                    });
                }

                return Results.Created(
                    $"/orcamentos/{resultado.OrcamentoId}",
                    new
                    {
                        id = resultado.OrcamentoId,
                        mensagem = resultado.Mensagem,
                        valorTotal = resultado.ValorTotal
                    });
            })
            .WithName("CreateOrcamento")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            // UPDATE
            group.MapPut("/{id}", async (
                int id,
                CriarOrcamentoRequest request,
                AppDbContext db,
                IOrcamentoService service) =>
            {
                var orcamento = await db.Orcamentos
                    .Include(o => o.Itens)
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (orcamento is null)
                {
                    return Results.NotFound(new
                    {
                        erro = "Orçamento não encontrado."
                    });
                }

                var resultado = service.AtualizarOrcamento(orcamento, request);

                if (!resultado.Sucesso)
                {
                    return Results.BadRequest(new
                    {
                        erro = resultado.Mensagem
                    });
                }

                await db.SaveChangesAsync();

                return Results.Ok(new
                {
                    mensagem = resultado.Mensagem,
                    valorTotal = resultado.ValorTotal
                });
            })
            .WithName("UpdateOrcamento")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

            // DELETE
            group.MapDelete("/{id}", async (int id, AppDbContext db) =>
            {
                var orcamento = await db.Orcamentos
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (orcamento is null)
                {
                    return Results.NotFound(new
                    {
                        erro = "Orçamento não encontrado."
                    });
                }

                db.Orcamentos.Remove(orcamento);

                await db.SaveChangesAsync();

                return Results.Ok(new
                {
                    mensagem = "Orçamento removido com sucesso."
                });
            })
            .WithName("DeleteOrcamento")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        }
    }
}
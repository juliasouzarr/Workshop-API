using Microsoft.EntityFrameworkCore;
using OficinaAPI.Models;
using System.Collections.Generic;

namespace OficinaAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Orcamento> Orcamentos { get; set; }
        public DbSet<OrcamentoItem> OrcamentoItens { get; set; }
    }
}
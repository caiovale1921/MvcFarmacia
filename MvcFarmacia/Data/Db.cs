using Microsoft.EntityFrameworkCore;
using MvcFarmacia.Models;

namespace MvcFarmacia.Data
{
    public class Db : DbContext
    {
        public Db(DbContextOptions<Db> options) : base(options)
        {
        }

        public DbSet<TipoProduto> TipoProdutos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Farmacia> Farmacias { get; set; }
    }
}

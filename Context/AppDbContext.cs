using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {

        }

        public DbSet<Categoria>? Categorias { get; set; }
        public DbSet<Produto>? Produtos { get; set; }

        //protected override void OnModelCreating(ModelBuilder mb)
        //{
        //    //Categoria
        //    mb.Entity<Categoria>().HasKey(c => c.CategoriaId); //PrimaryKey
        //    mb.Entity<Categoria>().Property(c=> c.Nome).HasMaxLength(80).IsRequired();
        //    mb.Entity<Categoria>().Property(c => c.ImagemUrl).HasMaxLength(300).IsRequired();

        //    //Produto
        //    mb.Entity<Produto>().HasKey(p => p.ProdutoId); //PrimaryKey
        //    mb.Entity<Produto>().Property(p=> p.Nome).HasMaxLength(80).IsRequired();
        //    mb.Entity<Produto>().Property(p => p.Descricao).HasMaxLength(300).IsRequired();
        //    mb.Entity<Produto>().Property(p => p.Preco).HasPrecision(10,2).IsRequired();
        //    mb.Entity<Produto>().Property(p => p.ImagemUrl).HasMaxLength(300).IsRequired();

        //    //Relacionamento
        //    mb.Entity<Produto>()
        //        .HasOne(c => c.Categoria)
        //            .WithMany(p => p.Produtos)
        //                .HasForeignKey(p => p.ProdutoId);
        //}
    }
}

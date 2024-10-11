using Microsoft.EntityFrameworkCore;
using KitLobieDesign.Models;

namespace KitLobieDesign.Context
{
    public class AppDbContext : DbContext
    {
        // Construtor que recebe as opções e passa para a classe base
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Definição das tabelas do banco de dados
        public DbSet<Kit> Kits { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacionamento um-para-muitos entre Kit e Categoria
            modelBuilder.Entity<Kit>()
                .HasMany(k => k.Categories)
                .WithOne()
                .HasForeignKey("KitId");

            // Relacionamento um-para-muitos entre Categoria e Item
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Items)
                .WithOne(i => i.Category)
                .HasForeignKey(i => i.CategoryId);

            base.OnModelCreating(modelBuilder); // Chama a implementação da classe base
        }
    }
}

using Microsoft.EntityFrameworkCore;
using ProvaMedGroup.DomainModel.Entities;
using ProvaMedGroup.Infra.Mappings;



namespace ProvaMedGroup.Infra.Context
{
    public class ProvaMedGroupDbContext : DbContext
    {
        public DbSet<Contato> Contatos { get; set; }

        public ProvaMedGroupDbContext(DbContextOptions<ProvaMedGroupDbContext> options) : base(options) { }

        protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContatoMapping());

            base.OnModelCreating(modelBuilder);
        }

    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProvaMedGroup.DomainModel.Entities;

namespace ProvaMedGroup.Infra.Mappings
{
    public class ContatoMapping : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Nome)
                   .IsRequired()
                   .HasColumnType("varchar(100)");

            builder.Property(f => f.DataNascimento)
                   .IsRequired()
                   .HasColumnType("Datetime");

            builder.Property(f => f.Sexo)
                   .IsRequired()
                   .HasColumnType("char(1)");

            builder.Property(p => p.Ativo)
                   .IsRequired()
                   .HasColumnType("bit");

            builder.ToTable("Contato");
        }

    }
}

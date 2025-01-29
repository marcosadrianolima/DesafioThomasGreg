using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Entidades;

namespace POC.ThomasGreg.Cadastro.Infra.SqlServer.Feature.Cliente
{
    public class ClienteConfiguration : IEntityTypeConfiguration<ClienteVO>
    {
        public void Configure(EntityTypeBuilder<ClienteVO> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Logotipo)
                .HasColumnType("varbinary(max)");

            // Configuração do relacionamento 1:N com Logradouro
            builder.HasMany(c => c.Logradouros)
                .WithOne()
                .HasForeignKey(l => l.ClienteId)
                .OnDelete(DeleteBehavior.Cascade); // Remove os logradouros ao excluir o cliente
        }
    }
}

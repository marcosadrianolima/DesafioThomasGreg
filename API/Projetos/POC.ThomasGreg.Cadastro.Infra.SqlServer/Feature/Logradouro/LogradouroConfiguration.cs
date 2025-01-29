using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Entidades;
using POC.ThomasGreg.Cadastro.Domain.Features.Logradouro.Entidade;

namespace POC.ThomasGreg.Cadastro.Infra.SqlServer.Feature.Cliente
{
    public class LogradouroConfiguration : IEntityTypeConfiguration<LogradouroVO>
    {
        public void Configure(EntityTypeBuilder<LogradouroVO> builder)
        {
            builder.ToTable("Logradouros");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id)
                .ValueGeneratedOnAdd();

            builder.Property(l => l.Nome)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(l => l.ClienteId)
                .HasColumnName("ClienteId")
                .IsRequired();

            // Configuração do relacionamento N:1 com Cliente
            builder.HasOne<ClienteVO>()
                .WithMany(c => c.Logradouros)
                .HasForeignKey(l => l.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

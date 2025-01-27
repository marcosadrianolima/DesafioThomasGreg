using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Entidades;

namespace POC.ThomasGreg.Cadastro.Infra.SqlServer.Configuration
{
    public class ClienteConfiguration : IEntityTypeConfiguration<ClienteVO>
    {
        public void Configure(EntityTypeBuilder<ClienteVO> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}

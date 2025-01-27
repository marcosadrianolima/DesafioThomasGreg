using Microsoft.EntityFrameworkCore;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Entidades;
using POC.ThomasGreg.Cadastro.Infra.SqlServer.Configuration;

namespace POC.ThomasGreg.Cadastro.Infra.SqlServer
{
    public class CadastroDbContext : DbContext
    {
        public CadastroDbContext(DbContextOptions<CadastroDbContext> options)
            : base(options)
        {
        }

        // Defina os DbSets para suas entidades
        public DbSet<ClienteVO> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplica as configurações de mapeamento manual
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());

            base.OnModelCreating(modelBuilder);

        }
    }

}

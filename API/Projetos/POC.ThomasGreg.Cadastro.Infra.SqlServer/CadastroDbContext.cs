using Microsoft.EntityFrameworkCore;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Entidades;
using POC.ThomasGreg.Cadastro.Domain.Features.Logradouro.Entidade;
using POC.ThomasGreg.Cadastro.Infra.SqlServer.Feature.Cliente;

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
        public DbSet<LogradouroVO> Logradouros { get; set; }


        /// <summary>
        /// Ativar o EnableSensitiveDataLogging para debugar em caso de erros na execução de querys com o entity
        /// </summary>
        /// <param name="optionsBuilder"></param>
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.LogTo(Console.WriteLine)
        //                  .EnableSensitiveDataLogging();
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplica as configurações de mapeamento manual
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new LogradouroConfiguration());


            base.OnModelCreating(modelBuilder);

        }
    }

}

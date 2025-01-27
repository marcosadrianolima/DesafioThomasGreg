using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace POC.ThomasGreg.Cadastro.Infra.SqlServer
{
    public static class Inicializacao
    {
        public static IServiceCollection AdicionarDependenciasInfraSqlServer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CadastroDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceProvider AdicionarMigration(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<CadastroDbContext>();
                dbContext.Database.Migrate(); // Cria o banco de dados e aplica as migrations
            }

            return services;
        }
    }
}

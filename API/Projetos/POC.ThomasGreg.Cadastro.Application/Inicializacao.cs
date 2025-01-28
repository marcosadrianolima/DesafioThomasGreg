using Microsoft.Extensions.DependencyInjection;

namespace POC.ThomasGreg.Cadastro.Application
{
    public static class Inicializacao
    {
        public static IServiceCollection AdicionarDependenciasApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Repositorio;
using POC.ThomasGreg.Cadastro.Domain.Features.Logradouro.Repositorio;
using POC.ThomasGreg.Cadastro.Infra.Features.Cliente;

namespace POC.ThomasGreg.Cadastro.Infra
{
    public static class Inicializacao
    {
        public static IServiceCollection AdicionarDependenciasInfraMemoria(this IServiceCollection service)
        {
            service.AddScoped<IRepositorioCliente, RepositorioCliente>();
            service.AddScoped<IRepositorioLogradouro, RepositorioLogradouro>();

            return service;
        }
    }
}

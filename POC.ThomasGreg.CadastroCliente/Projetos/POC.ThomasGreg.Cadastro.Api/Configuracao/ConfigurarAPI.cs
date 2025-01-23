using POC.ThomasGreg.Cadastro.Application;
using POC.ThomasGreg.Cadastro.Application.Features.Cliente.Listar;
using POC.ThomasGreg.Cadastro.Infra;

namespace POC.ThomasGreg.Cadastro.Api.Configuracao
{
    public static class ConfigurarAPI
    {
        public static IServiceCollection ConfiguracaoEspecifica(this IServiceCollection service)
        {
            // Registra o MediatR
            service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ListarClientQueryHandler).Assembly));

            //Dependencias de Infra
            service.AdicionarDependenciasInfra();

            // Configurar AutoMapper
            // Registrar o AutoMapper
            service.AddAutoMapper(typeof(MappingProfile).Assembly);

            return service;
        }
    }
}

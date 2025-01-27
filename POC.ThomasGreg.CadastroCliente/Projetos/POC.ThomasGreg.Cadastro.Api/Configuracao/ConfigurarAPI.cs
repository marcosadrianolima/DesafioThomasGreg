using POC.ThomasGreg.Cadastro.Application;
using POC.ThomasGreg.Cadastro.Application.Features.Cliente.Listar;
using POC.ThomasGreg.Cadastro.Infra;
using POC.ThomasGreg.Cadastro.Infra.Log;
using POC.ThomasGreg.Cadastro.Infra.SqlServer;
using Serilog;

namespace POC.ThomasGreg.Cadastro.Api.Configuracao
{
    public static class ConfigurarAPI
    {
        public static WebApplicationBuilder ConfiguracaoEspecifica(this WebApplicationBuilder builder)
        {
            // Registra o MediatR
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ListarClienteQueryHandler).Assembly));

            //Dependencias de Infra
            builder.Services.AdicionarDependenciasInfraMemoria();

            builder.Services.AdicionarDependenciasInfraSqlServer(connectionString: builder.Configuration.GetConnectionString("DefaultConnection"));

            // Configurar AutoMapper
            // Registrar o AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

            AdicionarLogFile(builder);


            return builder;
        }

        private static void AdicionarLogFile(WebApplicationBuilder builder)
        {
            // Configura o Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration) // Lê as configurações do appsettings.json
                .Enrich.FromLogContext()
                .Enrich.WithThreadId()
                .CreateLogger();

            builder.Services.AddSingleton(Log.Logger);

            builder.Services.AdicionarServicoLog();
        }
    }
}

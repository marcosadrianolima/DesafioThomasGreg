using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using POC.ThomasGreg.Cadastro.Application;
using POC.ThomasGreg.Cadastro.Application.Features.Cliente.Listar;
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
            //builder.Services.AdicionarDependenciasInfraMemoria();

            builder.Services.AdicionarDependenciasInfraSqlServer(connectionString: builder.Configuration.GetConnectionString("DefaultConnection"));

            // Configurar AutoMapper
            // Registrar o AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

            AdicionarLogFile(builder);

            builder.Services.AdicionarJwtBearer(builder.Configuration.GetSection("JwtSettings"));

            return builder;
        }
        public static IServiceCollection AdicionarJwtBearer(this IServiceCollection services, IConfigurationSection bearerConfiguration)
        {
            var secretKey = bearerConfiguration["Secret"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = bearerConfiguration["Issuer"],
                    ValidAudience = bearerConfiguration["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ClockSkew = TimeSpan.Zero // Evita aceitar tokens expirados
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        var teste = context.Exception.Message;
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        var teste = context.ErrorDescription;
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddSingleton<JwtService>();

            return services;
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

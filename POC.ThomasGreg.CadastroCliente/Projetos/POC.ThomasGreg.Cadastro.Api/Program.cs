using Microsoft.OpenApi.Models;
using POC.ThomasGreg.Cadastro.Api.Configuracao;
using POC.ThomasGreg.Cadastro.Infra.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços à coleção.
builder.Services.AddControllers(options =>
{
    // Adiciona o filtro global
    options.Filters.Add<GlobalExceptionFilter>();
});

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    // Definindo a autenticação no Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid JWT token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddEndpointsApiExplorer();

builder.ConfiguracaoEspecifica();

var app = builder.Build();

app.Services.AdicionarMigration();

// Configure o pipeline de requisições HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Coloque o middleware CustomExceptionMiddleware antes da chamada para MapControllers
app.UseMiddleware<CustomExceptionMiddleware>();  // Middleware de exceção

// Habilita redirecionamento de HTTPS
app.UseHttpsRedirection();

// Habilita a autorização
app.UseAuthentication();
app.UseAuthorization();

// Mapeia os controladores
app.MapControllers();

app.Run();

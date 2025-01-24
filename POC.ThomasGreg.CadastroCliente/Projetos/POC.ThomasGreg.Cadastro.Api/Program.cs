using POC.ThomasGreg.Cadastro.Api.Configuracao;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços à coleção.
builder.Services.AddControllers(options =>
{
    // Adiciona o filtro global
    options.Filters.Add<GlobalExceptionFilter>();
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.ConfiguracaoEspecifica();

var app = builder.Build();

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
app.UseAuthorization();

// Mapeia os controladores
app.MapControllers();

app.Run();

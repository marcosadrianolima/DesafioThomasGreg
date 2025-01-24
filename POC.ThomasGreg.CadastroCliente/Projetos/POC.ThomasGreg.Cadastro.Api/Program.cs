using POC.ThomasGreg.Cadastro.Api.Configuracao;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os servi�os � cole��o.
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

// Configure o pipeline de requisi��es HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Coloque o middleware CustomExceptionMiddleware antes da chamada para MapControllers
app.UseMiddleware<CustomExceptionMiddleware>();  // Middleware de exce��o

// Habilita redirecionamento de HTTPS
app.UseHttpsRedirection();

// Habilita a autoriza��o
app.UseAuthorization();

// Mapeia os controladores
app.MapControllers();

app.Run();

namespace POC.ThomasGreg.Cadastro.Api.Configuracao
{
    ///<summary>
    /// Middleware personalizado para capturar e tratar exceções que ocorrem durante o pipeline de requisição.
    /// </summary>
    /// <remarks>
    /// Esta classe existe para garantir que erros inesperados ou exceções sejam interceptados e tratados de forma consistente,
    /// fornecendo respostas padronizadas ao cliente e evitando a exposição de detalhes sensíveis.
    /// 
    /// Objetivos principais:
    /// - Centralizar o tratamento de erros.
    /// - Registrar logs detalhados sobre exceções para análise posterior.
    /// - Retornar respostas com mensagens amigáveis para o cliente, incluindo status HTTP apropriados.
    /// 
    /// Exemplos de erros tratados:
    /// - Problemas de validação no request.
    /// - Exceções de banco de dados.
    /// - Qualquer erro não tratado no código da aplicação.
    /// </remarks>
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // Continua para o próximo middleware
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex); // Captura e trata exceções
            }

            // Captura respostas BadRequest de forma explícita
            if (context.Response.StatusCode == StatusCodes.Status400BadRequest && !context.Response.HasStarted)
            {
                var response = new
                {
                    IsSuccess = false,
                    Message = "A requisição não pôde ser processada.",
                    StatusCode = context.Response.StatusCode
                };

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(response);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = new
            {
                IsSuccess = false,
                Message = "Ocorreu um erro inesperado.",
                Details = exception.Message
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return context.Response.WriteAsJsonAsync(response);
        }
    }

}

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace POC.ThomasGreg.Cadastro.Api.Configuracao
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            // Verifica se a exceção é uma de validação (ou outro tipo de exceção que você deseja tratar)
            if (exception is ValidationException validationException)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    message = "Ocorreu um erro no campo enviado.",
                    errors = validationException.Message
                });
            }
            else
            {
                // Tratar outros tipos de exceções de forma genérica
                context.Result = new ObjectResult(new { message = exception.Message })
                {
                    StatusCode = 500
                };
            }

            context.ExceptionHandled = true;
        }
    }

}

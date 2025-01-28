using MediatR;
using POC.ThomasGreg.Cadastro.Application.Features.Cliente.Listar;

namespace POC.ThomasGreg.Cadastro.Application.Features.Cliente.Inserir
{
    public class ExcluirClienteCommand : IRequest<ExcluirClienteResposta>
    {
        public long Id { get; set; }
    }
}

using MediatR;

namespace POC.ThomasGreg.Cadastro.Application.Features.Cliente.Inserir
{
    public class ExcluirClientCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }
}

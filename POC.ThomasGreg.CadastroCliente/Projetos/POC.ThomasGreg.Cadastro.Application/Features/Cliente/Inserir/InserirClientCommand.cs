using MediatR;
using POC.ThomasGreg.Cadastro.Application.DTO;

namespace POC.ThomasGreg.Cadastro.Application.Features.Cliente.Inserir
{
    public class InserirClientCommand : IRequest<ClienteDTO>
    {
        public ClienteDTO ClienteDTO { get; set; }
    }
}

using MediatR;
using POC.ThomasGreg.Cadastro.Application.DTO;
using POC.ThomasGreg.Cadastro.Application.Features.Cliente.Listar;

namespace POC.ThomasGreg.Cadastro.Application.Features.Cliente.Inserir
{
    public class InserirClienteCommand : IRequest<InserirClienteResposta>
    {
        public ClienteDTO ClienteDTO { get; set; }
    }
}

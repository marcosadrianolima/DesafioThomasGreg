using MediatR;
using POC.ThomasGreg.Cadastro.Application.DTO;
using POC.ThomasGreg.Cadastro.Application.Features.Cliente.Listar;

namespace POC.ThomasGreg.Cadastro.Application.Features.Cliente.Editar
{
    public class EditarClienteCommand : IRequest<EditarClienteResposta>
    {
        public ClienteDTO ClienteDTO { get; set; }
        public long Id { get; set; }
    }
}

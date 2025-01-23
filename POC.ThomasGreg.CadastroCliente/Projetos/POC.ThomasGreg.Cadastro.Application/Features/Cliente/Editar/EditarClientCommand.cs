using MediatR;
using POC.ThomasGreg.Cadastro.Application.DTO;

namespace POC.ThomasGreg.Cadastro.Application.Features.Cliente.Editar
{
    public class EditarClientCommand : IRequest<Unit>
    {
        public ClienteDTO ClienteDTO { get; set; }
        public long Id { get; set; }
    }
}

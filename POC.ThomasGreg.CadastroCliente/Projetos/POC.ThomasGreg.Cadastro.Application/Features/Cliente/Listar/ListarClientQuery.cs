using MediatR;
using POC.ThomasGreg.Cadastro.Application.DTO;

namespace POC.ThomasGreg.Cadastro.Application.Features.Cliente.Listar
{
    public class ListarClientQuery : IRequest<List<ClienteDTO>>
    {
    }
}

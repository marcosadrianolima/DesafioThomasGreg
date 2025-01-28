using MediatR;

namespace POC.ThomasGreg.Cadastro.Application.Features.Cliente.Listar
{
    public class BuscarPorIdQuery : IRequest<BuscarPorIdResposta>
    {
        public long Id { get; set; }
    }
}

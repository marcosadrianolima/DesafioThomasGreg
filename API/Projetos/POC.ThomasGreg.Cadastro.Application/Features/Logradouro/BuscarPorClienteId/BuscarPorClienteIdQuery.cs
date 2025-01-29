using MediatR;

namespace POC.ThomasGreg.Cadastro.Application.Features.Logradouro.BuscarPorId
{
    public class BuscarPorClienteIdQuery : IRequest<BuscarPorClienteIdResposta>
    {
        public long Id { get; set; }
    }
}

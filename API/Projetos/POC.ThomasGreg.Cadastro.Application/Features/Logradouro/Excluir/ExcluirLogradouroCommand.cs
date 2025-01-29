using MediatR;

namespace POC.ThomasGreg.Cadastro.Application.Features.Logradouro.Excluir
{
    public class ExcluirLogradouroCommand : IRequest<ExcluirLogradouroResposta>
    {
        public long Id { get; set; }
    }
}

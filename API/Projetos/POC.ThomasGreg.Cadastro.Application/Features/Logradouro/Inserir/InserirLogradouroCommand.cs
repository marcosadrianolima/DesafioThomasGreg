using MediatR;
using POC.ThomasGreg.Cadastro.Application.DTO;

namespace POC.ThomasGreg.Cadastro.Application.Features.Logradouro.Inserir
{
    public class InserirLogradouroCommand : IRequest<InserirLogradouroResposta>
    {
        public LogradouroDTO LogradouroDTO { get; set; }
    }
}

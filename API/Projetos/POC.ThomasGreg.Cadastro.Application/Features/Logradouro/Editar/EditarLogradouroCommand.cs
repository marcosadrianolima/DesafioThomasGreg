using MediatR;
using POC.ThomasGreg.Cadastro.Application.DTO;

namespace POC.ThomasGreg.Cadastro.Application.Features.Logradouro.Editar
{
    public class EditarLogradouroCommand : IRequest<EditarLogradouroResposta>
    {
        public LogradouroDTO Logradouro { get; set; }
        public long Id { get; set; }
    }
}

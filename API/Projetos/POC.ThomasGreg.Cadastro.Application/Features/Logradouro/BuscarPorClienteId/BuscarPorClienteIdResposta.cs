using POC.ThomasGreg.Cadastro.Application.Compartilhado;
using POC.ThomasGreg.Cadastro.Application.DTO;

namespace POC.ThomasGreg.Cadastro.Application.Features.Logradouro.BuscarPorId
{
    public class BuscarPorClienteIdResposta : RespostaBase
    {
        public List<LogradouroDTO> LogradouroDTO { get; set; }
    }
}

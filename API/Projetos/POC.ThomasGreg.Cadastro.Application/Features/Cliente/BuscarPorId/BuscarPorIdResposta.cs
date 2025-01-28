using POC.ThomasGreg.Cadastro.Application.Compartilhado;
using POC.ThomasGreg.Cadastro.Application.DTO;

namespace POC.ThomasGreg.Cadastro.Application.Features.Cliente.Listar
{
    public class BuscarPorIdResposta : RespostaBase
    {
        public ClienteDTO ClienteDTO { get; set; }
    }
}

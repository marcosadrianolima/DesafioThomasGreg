using POC.ThomasGreg.Cadastro.Application.Compartilhado;
using POC.ThomasGreg.Cadastro.Application.DTO;

namespace POC.ThomasGreg.Cadastro.Application.Features.Cliente.Listar
{
    public class ListarClienteResposta : RespostaBase
    {
        public List<ClienteDTO> ClienteDTOs { get; set; }
    }
}

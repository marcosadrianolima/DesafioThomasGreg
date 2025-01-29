using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Entidades;
using POC.ThomasGreg.Cadastro.Domain.Features.Logradouro.Entidade;

namespace POC.ThomasGreg.Cadastro.Infra
{
    public class BancoVirtualTeste
    {
        public static List<ClienteVO> Clientes { get; set; } = new List<ClienteVO>();
        public static List<LogradouroVO> Logradouros { get; set; } = new List<LogradouroVO>();
    }
}

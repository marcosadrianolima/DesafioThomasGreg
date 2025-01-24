using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Entidades;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Repositorio;

namespace POC.ThomasGreg.Cadastro.Infra.Features.Cliente
{
    public class RepositorioLogradouro : IRepositorioLogradouro
    {
        public List<LogradouroVO> BuscarTodos()
        {
            return BancoVirtualTeste.Logradouros.ToList();
        }
    }
}

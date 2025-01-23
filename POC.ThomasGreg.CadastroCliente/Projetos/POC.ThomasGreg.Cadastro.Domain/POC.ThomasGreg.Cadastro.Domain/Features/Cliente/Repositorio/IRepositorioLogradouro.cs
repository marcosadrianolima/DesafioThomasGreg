using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Entidades;

namespace POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Repositorio
{
    public interface IRepositorioLogradouro
    {
        List<LogradouroVO> BuscarTodos();
    }
}

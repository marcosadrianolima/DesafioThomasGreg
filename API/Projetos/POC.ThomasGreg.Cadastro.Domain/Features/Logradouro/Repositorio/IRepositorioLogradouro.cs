using POC.ThomasGreg.Cadastro.Domain.Features.Logradouro.Entidade;

namespace POC.ThomasGreg.Cadastro.Domain.Features.Logradouro.Repositorio
{
    public interface IRepositorioLogradouro
    {
        List<LogradouroVO> BuscarPorClienteId(long id);

        LogradouroVO BuscarPorId(long id);

        long Inserir(LogradouroVO cliente);

        void Editar(long id, LogradouroVO cliente);

        bool Excluir(long id);
    }
}

using POC.ThomasGreg.Cadastro.Domain.Features.Logradouro.Entidade;
using POC.ThomasGreg.Cadastro.Domain.Features.Logradouro.Repositorio;

namespace POC.ThomasGreg.Cadastro.Infra.Features.Cliente
{
    public class RepositorioLogradouro : IRepositorioLogradouro
    {
        public List<LogradouroVO> BuscarPorClienteId(long id)
        {
            throw new NotImplementedException();
        }

        public LogradouroVO BuscarPorId(long id)
        {
            throw new NotImplementedException();
        }

        public void Editar(long id, LogradouroVO cliente)
        {
            throw new NotImplementedException();
        }

        public bool Excluir(long id)
        {
            throw new NotImplementedException();
        }

        public long Inserir(LogradouroVO cliente)
        {
            throw new NotImplementedException();
        }
    }
}

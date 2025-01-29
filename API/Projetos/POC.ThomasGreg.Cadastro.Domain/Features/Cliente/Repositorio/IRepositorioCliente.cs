using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Entidades;

namespace POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Repositorio
{
    public interface IRepositorioCliente
    {
        List<ClienteVO> BuscarTodos(bool incluirLougradouro);

        ClienteVO BuscarPorId(long id);

        long Inserir(ClienteVO cliente);

        void Editar(long id, ClienteVO cliente);

        bool Excluir(long id);

        bool ExisteEmailCadastrado(string email);
    }
}

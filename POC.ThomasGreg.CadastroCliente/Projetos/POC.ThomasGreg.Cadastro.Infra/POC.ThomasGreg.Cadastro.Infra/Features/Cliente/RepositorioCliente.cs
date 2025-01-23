using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Entidades;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Repositorio;

namespace POC.ThomasGreg.Cadastro.Infra.Features.Cliente
{
    public class RepositorioCliente : IRepositorioCliente
    {
        public List<ClienteVO> BuscarTodos(bool incluirLogradouro)
        {
            return BancoVirtualTeste.Clientes.ToList();
        }

        public ClienteVO BuscarPorId(long id)
        {
            if (BancoVirtualTeste.Clientes.Any())
                return BancoVirtualTeste.Clientes.FirstOrDefault(x => x.Id == id);

            return null;
        }

        public long Inserir(ClienteVO cliente)
        {
            long maiorId;

            if (BancoVirtualTeste.Clientes.Any())
                maiorId = BancoVirtualTeste.Clientes.Select(x => x.Id).Max(x => x);
            else
                maiorId = 1;

            cliente.Id = maiorId;

            BancoVirtualTeste.Clientes.Add(cliente);

            return maiorId;
        }

        public void Editar(long id, ClienteVO cliente)
        {
            var index = BancoVirtualTeste.Clientes.FindIndex(editar => editar.Id == id);

            var editar = BancoVirtualTeste.Clientes.ElementAt(index);

            editar.Atualizar(cliente);
        }

        public bool Excluir(long id)
        {
            var index = BancoVirtualTeste.Clientes.FindIndex(excluir => excluir.Id == id);

            BancoVirtualTeste.Clientes.RemoveAt(index);

            return true;
        }
    }
}

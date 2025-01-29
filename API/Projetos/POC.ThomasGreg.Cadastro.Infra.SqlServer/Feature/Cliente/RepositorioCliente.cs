using Microsoft.EntityFrameworkCore;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Entidades;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Repositorio;

namespace POC.ThomasGreg.Cadastro.Infra.SqlServer.Feature.Cliente
{
    public class RepositorioCliente : IRepositorioCliente
    {
        private readonly CadastroDbContext _context;

        public RepositorioCliente(CadastroDbContext context)
        {
            _context = context;
        }

        public List<ClienteVO> BuscarTodos(bool incluirLogradouro)
        {
            if (incluirLogradouro)
            {
                return _context.Clientes
                    .Include(c => c.Logradouros)
                    .ToList();
            }

            return _context.Clientes.ToList();
        }

        public ClienteVO BuscarPorId(long id)
        {
            return _context.Clientes
                .Include(c => c.Logradouros)
                .FirstOrDefault(c => c.Id == id);
        }

        public long Inserir(ClienteVO cliente)
        {
            var (valido, mensagemErro) = cliente.Valido();
            if (!valido)
            {
                throw new ArgumentException($"Os seguintes campos são obrigatórios: {mensagemErro}");
            }

            _context.Clientes.Add(cliente);
            _context.SaveChanges();
            return cliente.Id;
        }

        public void Editar(long id, ClienteVO cliente)
        {
            var clienteExistente = _context.Clientes
                .Include(c => c.Logradouros)
                .FirstOrDefault(c => c.Id == id);

            if (clienteExistente == null)
            {
                throw new KeyNotFoundException("Cliente não encontrado.");
            }

            clienteExistente.Atualizar(cliente);
            _context.SaveChanges();
        }

        public bool Excluir(long id)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.Id == id);

            if (cliente == null)
            {
                return false;
            }

            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
            return true;
        }

        public bool ExisteEmailCadastrado(string email)
        {
            return _context.Clientes.FirstOrDefault(x => x.Email == email) != null;
        }
    }
}

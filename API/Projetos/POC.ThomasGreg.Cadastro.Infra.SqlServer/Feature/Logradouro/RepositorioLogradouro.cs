using POC.ThomasGreg.Cadastro.Domain.Features.Logradouro.Entidade;
using POC.ThomasGreg.Cadastro.Domain.Features.Logradouro.Repositorio;
using POC.ThomasGreg.Cadastro.Infra.SqlServer;

public class RepositorioLogradouro : IRepositorioLogradouro
{
    private readonly CadastroDbContext _context;

    public RepositorioLogradouro(CadastroDbContext context)
    {
        _context = context;
    }

    public List<LogradouroVO> BuscarPorClienteId(long id)
    {
        return _context.Logradouros
            .Where(l => l.ClienteId == id)
            .ToList();
    }

    public LogradouroVO BuscarPorId(long id)
    {
        return _context.Logradouros
                .FirstOrDefault(l => l.Id == id);
    }

    public void Editar(long id, LogradouroVO logradouro)
    {
        var logradouroExistente = _context.Logradouros.Find(id);

        logradouroExistente.Nome = logradouro.Nome;

        _context.SaveChanges();
    }

    public bool Excluir(long id)
    {
        var logradouro = _context.Logradouros.Find(id);

        _context.Logradouros.Remove(logradouro);

        _context.SaveChanges();

        return true;
    }

    public long Inserir(LogradouroVO logradouro)
    {
        _context.Logradouros.Add(logradouro);
        _context.SaveChanges();

        return logradouro.Id;
    }
}

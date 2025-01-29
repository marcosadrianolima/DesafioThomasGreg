using MediatR;
using POC.ThomasGreg.Cadastro.Application.Features.Cliente.Listar;
using POC.ThomasGreg.Cadastro.Domain.Compartilhados.Log;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Repositorio;

namespace POC.ThomasGreg.Cadastro.Application.Features.Cliente.Inserir
{
    public class ExcluirClienteCommandHandler : IRequestHandler<ExcluirClienteCommand, ExcluirClienteResposta>
    {
        private readonly IRepositorioCliente _repositorioCliente;
        private readonly ILogThomasGreg _log;
        public ExcluirClienteCommandHandler(IRepositorioCliente repositorioCliente, ILogThomasGreg log)
        {
            _repositorioCliente = repositorioCliente;
            _log = log;
        }

        public Task<ExcluirClienteResposta> Handle(ExcluirClienteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _log.Verbose($"Excluindo registro com ID {request.Id}");

                if (request.Id <= 0)
                {
                    _log.Debug($"O campo ID deve ser informado");

                    return Task.FromResult(new ExcluirClienteResposta()
                    {
                        IsSucess = false,
                        Mensagem = $"O campo ID deve ser informado",
                    });
                }

                var itemExcluido = _repositorioCliente.Excluir(request.Id);

                return Task.FromResult(new ExcluirClienteResposta()
                {
                    IsSucess = true,
                    Mensagem = "Cliente excluir com sucesso"
                });
            }
            catch (Exception ex)
            {
                _log.Erro($"Ocorreu uma falha no fluxo de exclusão do cliente - Erro {ex.Message}");

                return Task.FromResult(new ExcluirClienteResposta()
                {
                    IsSucess = false,
                    Mensagem = $"Ocorreu um erro na exclusão do cliente",
                });
            }
        }
    }
}

using MediatR;
using POC.ThomasGreg.Cadastro.Domain.Compartilhados.Log;
using POC.ThomasGreg.Cadastro.Domain.Features.Logradouro.Repositorio;

namespace POC.ThomasGreg.Cadastro.Application.Features.Logradouro.Excluir
{
    public class ExcluirLogradouroCommandHandler : IRequestHandler<ExcluirLogradouroCommand, ExcluirLogradouroResposta>
    {
        private readonly IRepositorioLogradouro _repositorioLogradouro;
        private readonly ILogThomasGreg _log;
        public ExcluirLogradouroCommandHandler(IRepositorioLogradouro repositorioLogradouro, ILogThomasGreg log)
        {
            this._repositorioLogradouro = repositorioLogradouro;
            _log = log;
        }

        public Task<ExcluirLogradouroResposta> Handle(ExcluirLogradouroCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id <= 0)
                {
                    _log.Debug($"O campo ID deve ser informado");

                    return Task.FromResult(new ExcluirLogradouroResposta()
                    {
                        IsSucess = false,
                        Mensagem = $"O campo ID deve ser informado",
                    });
                }

                _log.Verbose($"Excluindo registro com ID {request.Id}");

                var itemExcluido = _repositorioLogradouro.Excluir(request.Id);

                _log.Verbose($"Registro {request.Id} excluído com sucesso");

                return Task.FromResult(new ExcluirLogradouroResposta()
                {
                    IsSucess = true,
                    Mensagem = "logradouro excluir com sucesso"
                });
            }
            catch (Exception ex)
            {
                _log.Erro($"Ocorreu uma falha no fluxo de exclusão do logradouro - Erro {ex.Message}");

                return Task.FromResult(new ExcluirLogradouroResposta()
                {
                    IsSucess = false,
                    Mensagem = $"Ocorreu um erro na exclusão do logradouro",
                });
            }
        }
    }
}

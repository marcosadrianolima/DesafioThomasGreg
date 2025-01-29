using AutoMapper;
using MediatR;
using POC.ThomasGreg.Cadastro.Domain.Compartilhados.Log;
using POC.ThomasGreg.Cadastro.Domain.Features.Logradouro.Entidade;
using POC.ThomasGreg.Cadastro.Domain.Features.Logradouro.Repositorio;

namespace POC.ThomasGreg.Cadastro.Application.Features.Logradouro.Inserir
{
    public class InserirLogradouroCommandHandler : IRequestHandler<InserirLogradouroCommand, InserirLogradouroResposta>
    {
        private readonly IRepositorioLogradouro _repositorioLogradouro;
        private readonly IMapper _mapper;
        private readonly ILogThomasGreg _log;
        public InserirLogradouroCommandHandler(IRepositorioLogradouro repositorioLogradouro, IMapper mapper, ILogThomasGreg log)
        {
            _repositorioLogradouro = repositorioLogradouro;
            _mapper = mapper;
            _log = log;
        }

        public Task<InserirLogradouroResposta> Handle(InserirLogradouroCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _log.Verbose($"Mapeando VO para inserir logradouro");

                var logradouroVo = _mapper.Map<LogradouroVO>(request.LogradouroDTO);

                _log.Verbose($"Validando os campos do logradouro");

                var (isValid, mensagemErro) = logradouroVo.Valido();

                if (!isValid)
                {
                    _log.Debug($"Os seguintes campos estão invalidos: {mensagemErro}");

                    return Task.FromResult(new InserirLogradouroResposta()
                    {
                        IsSucess = false,
                        Mensagem = $"Os seguintes campos estão invalidos: {mensagemErro}",
                    });
                }

                _log.Verbose($"Inserindo registro no banco");

                _repositorioLogradouro.Inserir(logradouroVo);

                _log.Verbose($"registro inserido com sucesso");

                return Task.FromResult(new InserirLogradouroResposta()
                {
                    IsSucess = true,
                    Mensagem = "logradouro inserido com sucesso"
                });
            }
            catch (Exception ex)
            {
                _log.Erro($"Ocorreu uma falha no fluxo de inserção do logradouro - Erro {ex.Message}");

                return Task.FromResult(new InserirLogradouroResposta()
                {
                    IsSucess = false,
                    Mensagem = $"Ocorreu um erro na inserção do logradouro",
                });
            }
        }
    }
}

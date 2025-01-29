using AutoMapper;
using MediatR;
using POC.ThomasGreg.Cadastro.Application.Features.Cliente.Listar;
using POC.ThomasGreg.Cadastro.Domain.Compartilhados.Log;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Entidades;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Repositorio;

namespace POC.ThomasGreg.Cadastro.Application.Features.Cliente.Inserir
{
    public class InserirClienteCommandHandler : IRequestHandler<InserirClienteCommand, InserirClienteResposta>
    {
        private readonly IRepositorioCliente _repositorioCliente;
        private readonly IMapper _mapper;
        private readonly ILogThomasGreg _log;
        public InserirClienteCommandHandler(IRepositorioCliente repositorioCliente, IMapper mapper, ILogThomasGreg log)
        {
            _repositorioCliente = repositorioCliente;
            _mapper = mapper;
            _log = log;
        }

        public Task<InserirClienteResposta> Handle(InserirClienteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _log.Verbose($"Mapeando VO para inserir cliente");

                var clienteVo = _mapper.Map<ClienteVO>(request.ClienteDTO);

                _log.Verbose($"Validando os campos do cliente");

                var (isValid, mensagemErro) = clienteVo.Valido();

                if (!isValid)
                {
                    _log.Debug($"Os seguintes campos estão invalidos: {mensagemErro}");

                    return Task.FromResult(new InserirClienteResposta()
                    {
                        IsSucess = false,
                        Mensagem = $"Os seguintes campos estão invalidos: {mensagemErro}",
                    });
                }

                _log.Verbose($"Inserindo registro no banco");

                _repositorioCliente.Inserir(clienteVo);

                _log.Verbose($"registro inserido com sucesso");

                return Task.FromResult(new InserirClienteResposta()
                {
                    IsSucess = true,
                    Mensagem = "Cliente inserido com sucesso"
                });
            }
            catch (Exception ex)
            {
                _log.Erro($"Ocorreu uma falha no fluxo de inserção do cliente - Erro {ex.Message}");

                return Task.FromResult(new InserirClienteResposta()
                {
                    IsSucess = false,
                    Mensagem = $"Ocorreu um erro na inserção do cliente",
                });
            }
        }
    }
}

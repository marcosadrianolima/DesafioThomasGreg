using AutoMapper;
using MediatR;
using POC.ThomasGreg.Cadastro.Application.Features.Cliente.Listar;
using POC.ThomasGreg.Cadastro.Domain.Compartilhados.Log;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Entidades;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Repositorio;

namespace POC.ThomasGreg.Cadastro.Application.Features.Cliente.Editar
{
    public class EditarClienteCommandHandler : IRequestHandler<EditarClienteCommand, EditarClienteResposta>
    {
        private readonly IRepositorioCliente _repositorioCliente;
        private readonly IMapper _mapper;
        private readonly ILogThomasGreg _log;
        public EditarClienteCommandHandler(IRepositorioCliente repositorioCliente, IMapper mapper, ILogThomasGreg log)
        {
            _repositorioCliente = repositorioCliente;
            _mapper = mapper;
            _log = log;
        }

        public Task<EditarClienteResposta> Handle(EditarClienteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _log.Verbose($"Mapeando objeto para edição do cliente id {request.Id}");

                var clienteVO = _mapper.Map<ClienteVO>(request.ClienteDTO);

                _log.Verbose($"Validando os campos do cliente");

                var (isValid, mensagemErro) = clienteVO.Valido();

                if (!isValid)
                {
                    _log.Debug($"Os seguintes campos estão invalidos: {mensagemErro}");

                    return Task.FromResult(new EditarClienteResposta()
                    {
                        IsSucess = false,
                        Mensagem = $"Os seguintes campos estão invalidos: {mensagemErro}",
                    });
                }

                _log.Verbose($"Editando registro");

                _repositorioCliente.Editar(request.Id, clienteVO);

                _log.Verbose($"Registro editado com sucesso");

                return Task.FromResult(new EditarClienteResposta()
                {
                    IsSucess = true,
                    Mensagem = "Cliente editado com sucesso"
                });
            }
            catch (Exception ex)
            {
                _log.Erro($"Ocorreu uma falha no fluxo de edição do cliente - Erro {ex.Message}");

                return Task.FromResult(new EditarClienteResposta()
                {
                    IsSucess = false,
                    Mensagem = $"Ocorreu um erro na edição do cliente",
                });
            }
        }
    }
}

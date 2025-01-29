using AutoMapper;
using MediatR;
using POC.ThomasGreg.Cadastro.Domain.Compartilhados.Log;
using POC.ThomasGreg.Cadastro.Domain.Features.Logradouro.Entidade;
using POC.ThomasGreg.Cadastro.Domain.Features.Logradouro.Repositorio;

namespace POC.ThomasGreg.Cadastro.Application.Features.Logradouro.Editar
{
    public class EditarLogradouroCommandHandler : IRequestHandler<EditarLogradouroCommand, EditarLogradouroResposta>
    {
        private readonly IRepositorioLogradouro _repositorioLogradouro;
        private readonly IMapper _mapper;
        private readonly ILogThomasGreg _log;
        public EditarLogradouroCommandHandler(IMapper mapper, ILogThomasGreg log, IRepositorioLogradouro repositorioLogradouro)
        {
            _mapper = mapper;
            _log = log;
            _repositorioLogradouro = repositorioLogradouro;
        }

        public Task<EditarLogradouroResposta> Handle(EditarLogradouroCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _log.Verbose($"Mapeando objeto para edição do logradouro id {request.Id}");

                var logradouroVO = _mapper.Map<LogradouroVO>(request.Logradouro);

                _log.Verbose($"Validando os campos do logradouro");

                if (request.Id <= 0)
                    return RetornoFalha($"O Id deve ser informado");

                _log.Verbose($"Validando se logradouro está cadastrado");

                var logradouroEncontrado = _repositorioLogradouro.BuscarPorId(request.Id);

                if (logradouroEncontrado == null)
                    return RetornoFalha($"Logradouro não encontrado para o ID {request.Id}");

                _log.Verbose($"Validando os campos do logradouro");

                var (isValid, mensagemErro) = logradouroVO.Valido();

                if (!isValid)
                {
                    return RetornoFalha($"Os seguintes campos estão invalidos: {mensagemErro}");
                }

                _log.Verbose($"Editando registro");

                _repositorioLogradouro.Editar(request.Id, logradouroVO);

                _log.Debug($"Registro editado com sucesso");

                return Task.FromResult(new EditarLogradouroResposta()
                {
                    IsSucess = true,
                    Mensagem = "Logradouro editado com sucesso"
                });
            }
            catch (Exception ex)
            {
                _log.Erro($"Ocorreu uma falha no fluxo de edição do logradouro - Erro {ex.Message}");

                return Task.FromResult(new EditarLogradouroResposta()
                {
                    IsSucess = true,
                    Mensagem = $"Ocorreu um erro na edição do logradouro",
                });
            }
        }

        private Task<EditarLogradouroResposta> RetornoFalha(string mensagemErro)
        {
            _log.Debug(mensagemErro);

            return Task.FromResult(new EditarLogradouroResposta()
            {
                IsSucess = false,
                Mensagem = mensagemErro,
            });
        }
    }
}

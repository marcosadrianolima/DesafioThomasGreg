using AutoMapper;
using MediatR;
using POC.ThomasGreg.Cadastro.Application.DTO;
using POC.ThomasGreg.Cadastro.Domain.Compartilhados.Log;
using POC.ThomasGreg.Cadastro.Domain.Features.Logradouro.Repositorio;

namespace POC.ThomasGreg.Cadastro.Application.Features.Logradouro.BuscarPorId
{
    public class BuscarPorClienteIdQueryHandler : IRequestHandler<BuscarPorClienteIdQuery, BuscarPorClienteIdResposta>
    {
        private readonly IRepositorioLogradouro _repositorioLogradouro;
        private readonly IMapper _mapper;
        private readonly ILogThomasGreg _log;
        public BuscarPorClienteIdQueryHandler(IRepositorioLogradouro repositorioLogradouro, IMapper mapper, ILogThomasGreg log)
        {
            _repositorioLogradouro = repositorioLogradouro;
            _mapper = mapper;
            _log = log;
        }

        public Task<BuscarPorClienteIdResposta> Handle(BuscarPorClienteIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id <= 0)
                {
                    _log.Debug($"O Id não foi informado");

                    return Task.FromResult(new BuscarPorClienteIdResposta()
                    {
                        IsSucess = false,
                        Mensagem = $"O Id não foi informado",
                    });
                }
                _log.Verbose($"Buscando os logradouros pelo cliente ID {request.Id}");

                var logradourosVo = _repositorioLogradouro.BuscarPorClienteId(request.Id);

                _log.Verbose($"Mapeando objetos para LogradouroDTO");

                var logradourosDto = _mapper.Map<List<LogradouroDTO>>(logradourosVo);

                _log.Verbose($"Retornando logradouros do cliente {request.Id}");

                return Task.FromResult(new BuscarPorClienteIdResposta()
                {
                    IsSucess = true,
                    Mensagem = $"Encontrado {logradourosDto.Count}",
                    LogradouroDTO = logradourosDto
                });
            }
            catch (Exception ex)
            {
                _log.Erro($"Ocorreu uma falha no fluxo de busca dos logradouro - Erro {ex.Message}");

                return Task.FromResult(new BuscarPorClienteIdResposta()
                {
                    IsSucess = false,
                    Mensagem = $"Ocorreu um erro na busca dos logradouro cadastrados",
                });
            }
        }
    }
}

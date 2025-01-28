using AutoMapper;
using MediatR;
using POC.ThomasGreg.Cadastro.Application.DTO;
using POC.ThomasGreg.Cadastro.Domain.Compartilhados.Log;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Repositorio;

namespace POC.ThomasGreg.Cadastro.Application.Features.Cliente.Listar
{
    public class BuscarPorIdQueryHandler : IRequestHandler<BuscarPorIdQuery, BuscarPorIdResposta>
    {
        private readonly IRepositorioCliente _repositorioCliente;
        private readonly IMapper _mapper;
        private readonly ILogThomasGreg _log;
        public BuscarPorIdQueryHandler(IRepositorioCliente repositorioCliente, IMapper mapper, ILogThomasGreg log)
        {
            _repositorioCliente = repositorioCliente;
            _mapper = mapper;
            _log = log;
        }

        public Task<BuscarPorIdResposta> Handle(BuscarPorIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id <= 0)
                {
                    _log.Debug($"O Id não foi informado");

                    return Task.FromResult(new BuscarPorIdResposta()
                    {
                        IsSucess = false,
                        Mensagem = $"O Id não foi informado",
                    });
                }
                _log.Debug($"Buscando o cliente pelo ID {request.Id}");

                var cliente = _repositorioCliente.BuscarPorId(request.Id);

                _log.Debug($"Mapeando objetos para ClienteDTO");

                var clienteDTO = _mapper.Map<ClienteDTO>(cliente);

                _log.Debug($"Retornando Objetos Mapeados");

                return Task.FromResult(new BuscarPorIdResposta()
                {
                    IsSucess = true,
                    Mensagem = $"Cliente encontrado para ID {clienteDTO.Id}",
                    ClienteDTO = clienteDTO
                });
            }
            catch (Exception ex)
            {
                _log.Erro($"Ocorreu uma falha no fluxo de busca dos clientes - Erro {ex.Message}");

                return Task.FromResult(new BuscarPorIdResposta()
                {
                    IsSucess = true,
                    Mensagem = $"Ocorreu um erro na busca dos clientes cadastrados",
                });
            }
        }
    }
}

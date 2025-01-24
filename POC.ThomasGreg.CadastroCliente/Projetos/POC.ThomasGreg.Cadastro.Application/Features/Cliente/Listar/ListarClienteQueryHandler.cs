using AutoMapper;
using MediatR;
using POC.ThomasGreg.Cadastro.Application.DTO;
using POC.ThomasGreg.Cadastro.Domain.Compartilhados.Log;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Repositorio;

namespace POC.ThomasGreg.Cadastro.Application.Features.Cliente.Listar
{
    public class ListarClienteQueryHandler : IRequestHandler<ListarClienteQuery, ListarClienteResposta>
    {
        private readonly IRepositorioCliente _repositorioCliente;
        private readonly IMapper _mapper;
        private readonly ILogThomasGreg _log;
        public ListarClienteQueryHandler(IRepositorioCliente repositorioCliente, IMapper mapper, ILogThomasGreg log)
        {
            _repositorioCliente = repositorioCliente;
            _mapper = mapper;
            _log = log;
        }

        public Task<ListarClienteResposta> Handle(ListarClienteQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _log.Debug($"Buscando os clientes cadastrados");

                var clientes = _repositorioCliente.BuscarTodos(incluirLougradouro: false);

                _log.Debug($"Mapeando objetos para ClienteDTO");

                var clientesDTO = _mapper.Map<List<ClienteDTO>>(clientes);

                _log.Debug($"Retornando Objetos Mapeados");

                return Task.FromResult(new ListarClienteResposta()
                {
                    IsSucess = true,
                    Mensagem = $"Foram encontrados {clientesDTO.Count} cliente(s)",
                    ClienteDTOs = clientesDTO
                });
            }
            catch (Exception ex)
            {
                _log.Erro($"Ocorreu uma falha no fluxo de busca dos clientes - Erro {ex.Message}");

                return Task.FromResult(new ListarClienteResposta()
                {
                    IsSucess = true,
                    Mensagem = $"Ocorreu um erro na busca dos clientes cadastrados",
                });
            }
        }
    }
}

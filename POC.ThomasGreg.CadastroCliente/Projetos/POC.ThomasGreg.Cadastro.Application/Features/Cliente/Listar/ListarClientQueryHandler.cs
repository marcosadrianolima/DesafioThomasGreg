using AutoMapper;
using MediatR;
using POC.ThomasGreg.Cadastro.Application.DTO;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Repositorio;

namespace POC.ThomasGreg.Cadastro.Application.Features.Cliente.Listar
{
    public class ListarClientQueryHandler : IRequestHandler<ListarClientQuery, List<ClienteDTO>>
    {
        private readonly IRepositorioCliente _repositorioCliente;
        private readonly IMapper _mapper;
        public ListarClientQueryHandler(IRepositorioCliente repositorioCliente, IMapper mapper)
        {
            _repositorioCliente = repositorioCliente;
            _mapper = mapper;
        }

        public Task<List<ClienteDTO>> Handle(ListarClientQuery request, CancellationToken cancellationToken)
        {
            var clientes = _repositorioCliente.BuscarTodos(incluirLougradouro: true);

            var clientesDTO = _mapper.Map<List<ClienteDTO>>(clientes);

            return Task.FromResult(clientesDTO);
        }
    }
}

using AutoMapper;
using MediatR;
using POC.ThomasGreg.Cadastro.Application.DTO;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Entidades;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Repositorio;

namespace POC.ThomasGreg.Cadastro.Application.Features.Cliente.Inserir
{
    public class InserirClientCommandHandler : IRequestHandler<InserirClientCommand, ClienteDTO>
    {
        private readonly IRepositorioCliente _repositorioCliente;
        private readonly IMapper _mapper;
        public InserirClientCommandHandler(IRepositorioCliente repositorioCliente, IMapper mapper)
        {
            _repositorioCliente = repositorioCliente;
            _mapper = mapper;
        }

        public Task<ClienteDTO> Handle(InserirClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var clienteVo = _mapper.Map<ClienteVO>(request.ClienteDTO);

                clienteVo.Id = _repositorioCliente.Inserir(clienteVo);

                var clienteDTO = _mapper.Map<ClienteDTO>(clienteVo);

                return Task.FromResult(clienteDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

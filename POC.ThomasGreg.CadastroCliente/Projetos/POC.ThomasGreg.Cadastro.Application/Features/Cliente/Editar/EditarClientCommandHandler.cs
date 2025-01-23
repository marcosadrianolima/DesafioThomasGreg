using AutoMapper;
using MediatR;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Entidades;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Repositorio;

namespace POC.ThomasGreg.Cadastro.Application.Features.Cliente.Editar
{
    public class EditarClientCommandHandler : IRequestHandler<EditarClientCommand, Unit>
    {
        private readonly IRepositorioCliente _repositorioCliente;
        private readonly IMapper _mapper;
        public EditarClientCommandHandler(IRepositorioCliente repositorioCliente, IMapper mapper)
        {
            _repositorioCliente = repositorioCliente;
            _mapper = mapper;
        }

        public Task<Unit> Handle(EditarClientCommand request, CancellationToken cancellationToken)
        {
            var editar = _mapper.Map<ClienteVO>(request.ClienteDTO);

            _repositorioCliente.Editar(request.Id, editar);

            return Task.FromResult(Unit.Value);
        }
    }
}

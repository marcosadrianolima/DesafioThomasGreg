using AutoMapper;
using MediatR;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Repositorio;

namespace POC.ThomasGreg.Cadastro.Application.Features.Cliente.Inserir
{
    public class ExcluirClientCommandHandler : IRequestHandler<ExcluirClientCommand, bool>
    {
        private readonly IRepositorioCliente _repositorioCliente;
        private readonly IMapper _mapper;
        public ExcluirClientCommandHandler(IRepositorioCliente repositorioCliente, IMapper mapper)
        {
            _repositorioCliente = repositorioCliente;
            _mapper = mapper;
        }

        public Task<bool> Handle(ExcluirClientCommand request, CancellationToken cancellationToken)
        {
            var itemExcluido = _repositorioCliente.Excluir(request.Id);

            return Task.FromResult(itemExcluido);
        }
    }
}

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using POC.ThomasGreg.Cadastro.Application.DTO;
using POC.ThomasGreg.Cadastro.Application.Features.Cliente.Editar;
using POC.ThomasGreg.Cadastro.Application.Features.Cliente.Inserir;
using POC.ThomasGreg.Cadastro.Application.Features.Cliente.Listar;
using POC.ThomasGreg.Cadastro.Domain.Compartilhados.Log;

namespace POC.ThomasGreg.Cadastro.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogThomasGreg _log;

        public ClienteController(IMediator mediator, IMapper mapper, ILogThomasGreg log)
        {
            _mediator = mediator;
            _mapper = mapper;
            _log = log;
        }

        [HttpGet(Name = "Listar")]
        [ProducesResponseType(typeof(ListarClienteResposta), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ListarClienteResposta), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Listar()
        {
            _log.IdentificadorLog($"Listagem Clientes");

            _log.Information($"Iniciando listagem de cliente");

            var listarClienteResposta = await _mediator.Send(new ListarClienteQuery());

            _log.Information($"Finalizando listagem de cliente");

            return Ok(listarClienteResposta);
        }

        [HttpPost(Name = "Inserir")]
        [ProducesResponseType(typeof(InserirClienteResposta), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(InserirClienteResposta), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Inserir([FromBody] ClienteDTO cliente)
        {
            _log.IdentificadorLog($"Inserção Cliente");

            _log.Information($"Iniciando inserção de cliente");

            var clientes = await _mediator.Send(new InserirClienteCommand()
            {
                ClienteDTO = cliente
            });

            _log.Information($"Finalizando inserção de cliente");

            return Ok(clientes);
        }

        [HttpPut(Name = "Editar")]
        [ProducesResponseType(typeof(EditarClienteResposta), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EditarClienteResposta), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Editar([FromBody] ClienteDTO cliente)
        {
            _log.IdentificadorLog($"Edição Cliente");

            _log.Information($"Iniciando edição de cliente");

            var clientes = await _mediator.Send(new EditarClienteCommand()
            {
                ClienteDTO = cliente,
                Id = cliente.Id
            });

            _log.Information($"Finalizando edição de cliente");

            return Ok(clientes);
        }

        [HttpDelete(Name = "Excluir")]
        [ProducesResponseType(typeof(ExcluirClienteResposta), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExcluirClienteResposta), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Excluir([FromBody] long id)
        {
            _log.IdentificadorLog($"Exclusão Cliente");

            _log.Information($"Iniciando exclusão de cliente");

            var clientes = await _mediator.Send(new ExcluirClienteCommand()
            {
                Id = id
            });

            _log.Information($"Finalizando exclusão de cliente");

            return Ok(clientes);
        }
    }
}

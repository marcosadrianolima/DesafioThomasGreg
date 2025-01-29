using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using POC.ThomasGreg.Cadastro.Api.Configuracao;
using POC.ThomasGreg.Cadastro.Application.DTO;
using POC.ThomasGreg.Cadastro.Application.Features.Logradouro.BuscarPorId;
using POC.ThomasGreg.Cadastro.Application.Features.Logradouro.Editar;
using POC.ThomasGreg.Cadastro.Application.Features.Logradouro.Excluir;
using POC.ThomasGreg.Cadastro.Application.Features.Logradouro.Inserir;
using POC.ThomasGreg.Cadastro.Domain.Compartilhados.Log;

namespace POC.ThomasGreg.Cadastro.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    //[Authorize]
    public class LogradouroController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogThomasGreg _log;
        private readonly JwtService _jwtService;

        public LogradouroController(IMediator mediator, IMapper mapper, ILogThomasGreg log, JwtService jwtService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _log = log;
            _jwtService = jwtService;
        }

        [HttpGet("BuscarCliente/{clienteId}", Name = "BuscarPorClienteId")]
        [ProducesResponseType(typeof(BuscarPorClienteIdResposta), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BuscarPorClienteIdResposta), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BuscarPorClienteId(long clienteId)
        {
            _log.IdentificadorLog($"Busca de Logradouro por Cliente");

            _log.Information($"Iniciando busca de logradouro por Cliente ID");

            var logradouroResposta = await _mediator.Send(new BuscarPorClienteIdQuery()
            {
                Id = clienteId
            });

            _log.Information($"Finalizando busca de logradouro por Cliente ID");

            return Ok(logradouroResposta);
        }

        [HttpPost]
        [ProducesResponseType(typeof(InserirLogradouroResposta), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(InserirLogradouroResposta), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Inserir([FromBody] LogradouroDTO logradouro)
        {
            _log.IdentificadorLog($"Inserção Logradouro");

            _log.Information($"Iniciando inserção de logradouro");

            var logradouroResposta = await _mediator.Send(new InserirLogradouroCommand()
            {
                LogradouroDTO = logradouro
            });

            _log.Information($"Finalizando inserção de logradouro");

            return Ok(logradouroResposta);
        }

        [HttpPut]
        [ProducesResponseType(typeof(EditarLogradouroResposta), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EditarLogradouroResposta), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Editar([FromBody] LogradouroDTO logradouro)
        {
            _log.IdentificadorLog($"Edição Logradouro");

            _log.Information($"Iniciando edição de logradouro");

            var clientes = await _mediator.Send(new EditarLogradouroCommand()
            {
                Logradouro = logradouro,
                Id = logradouro.Id
            });

            _log.Information($"Finalizando edição de logradouro");

            return Ok(clientes);
        }

        [HttpDelete("Excluir/{id}")]
        [ProducesResponseType(typeof(ExcluirLogradouroResposta), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExcluirLogradouroResposta), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Excluir(long id)
        {
            _log.IdentificadorLog($"Exclusão Logradouro");

            _log.Information($"Iniciando exclusão de logradouro");

            var logradouroResposta = await _mediator.Send(new ExcluirLogradouroCommand()
            {
                Id = id
            });

            _log.Information($"Finalizando exclusão de logradouro");

            return Ok(logradouroResposta);
        }
    }
}

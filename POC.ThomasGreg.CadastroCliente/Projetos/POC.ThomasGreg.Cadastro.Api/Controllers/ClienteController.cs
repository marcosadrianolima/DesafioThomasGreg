using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using POC.ThomasGreg.Cadastro.Application.DTO;
using POC.ThomasGreg.Cadastro.Application.Features.Cliente.Editar;
using POC.ThomasGreg.Cadastro.Application.Features.Cliente.Inserir;
using POC.ThomasGreg.Cadastro.Application.Features.Cliente.Listar;

namespace POC.ThomasGreg.Cadastro.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ClienteController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet(Name = "Listar")]
        public async Task<IActionResult> Listar()
        {
            var clientes = await _mediator.Send(new ListarClientQuery());

            return Ok(clientes);
        }

        [HttpPost(Name = "Inserir")]
        public async Task<IActionResult> Inserir([FromBody] ClienteDTO cliente)
        {
            var clientes = await _mediator.Send(new InserirClientCommand()
            {
                ClienteDTO = cliente
            });

            return Ok(clientes);
        }

        [HttpPut(Name = "Editar")]
        public async Task<IActionResult> Editar([FromBody] ClienteDTO cliente)
        {
            var clientes = await _mediator.Send(new EditarClientCommand()
            {
                ClienteDTO = cliente,
                Id = cliente.Id
            });

            return Ok(clientes);
        }

        [HttpDelete(Name = "Excluir")]
        public async Task<IActionResult> Excluir([FromBody] long id)
        {
            var clientes = await _mediator.Send(new ExcluirClientCommand()
            {
                Id = id
            });

            return Ok(clientes);
        }
    }
}

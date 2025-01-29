using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using ThomaGregFront.Models;

namespace ThomaGregFront.Controllers
{
    public class ClienteController : Controller
    {
        private static readonly string apiBaseUrl = "http://localhost:5183/"; // URL da sua API
        private readonly HttpClient client;
        private ClienteDTO _clienteEmEdição;

        public ClienteController()
        {
            client = new HttpClient();
        }

        // Listar clientes
        public async Task<ActionResult> Index()
        {
            await ConfigurarAutenticacaoAsync();

            var response = await client.GetAsync(apiBaseUrl + "Cliente");
            var listarResult = await response.Content.ReadAsAsync<ListarClienteResposta>();
            return View(listarResult.ClienteDTOs);
        }

        // Detalhes do cliente
        public async Task<ActionResult> Details(int id)
        {
            var response = await client.GetAsync(apiBaseUrl + "Cliente/" + id);
            var cliente = await response.Content.ReadAsAsync<ClienteDTO>();
            return View(cliente);
        }

        // Criar cliente
        public async Task<ActionResult> Inserir()
        {
            await ConfigurarAutenticacaoAsync();

            var cliente = new ClienteDTO
            {
                Logradouros = new List<LogradouroDTO>() // Inicializa a lista de logradouros
            };
            return View(cliente);
        }

        [HttpPost]
        public async Task<ActionResult> Inserir(ClienteDTO cliente, IFormFile logotipo)
        {
            await ConfigurarAutenticacaoAsync();

            cliente.Logradouros = cliente.Logradouros ?? new List<LogradouroDTO>();

            ModelState.Remove("Logotipo");

            if (ModelState.IsValid)
            {
                if (logotipo != null && logotipo.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await logotipo.CopyToAsync(memoryStream);
                        cliente.Logotipo = memoryStream.ToArray();
                    }
                }

                // Enviar o cliente e seus logradouros para a API
                var response = await client.PostAsJsonAsync(apiBaseUrl + "Cliente/", cliente);

                if (response.IsSuccessStatusCode)
                {
                    var editarClienteResposta = await response.Content.ReadAsAsync<EditarClienteResposta>();

                    if (!editarClienteResposta.IsSucess)
                    {
                        TempData["MensagemErro"] = editarClienteResposta.Mensagem;

                        return View(cliente);
                    }

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Erro ao criar cliente");
            }

            TempData["MensagemErro"] = "Preencha todos os campos";

            return View(cliente);
        }

        // Editar cliente
        public async Task<ActionResult> Editar(int id)
        {
            await ConfigurarAutenticacaoAsync();

            var response = await client.GetAsync(apiBaseUrl + "Cliente/Buscar/" + id);
            var cliente = await response.Content.ReadAsAsync<BuscarPorIdResposta>();
            return View(cliente.ClienteDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Editar(int id, ClienteDTO cliente, IFormFile logotipo)
        {
            await ConfigurarAutenticacaoAsync();

            cliente.Logradouros = cliente.Logradouros ?? new List<LogradouroDTO>();

            ModelState.Remove("Logotipo");

            if (ModelState.IsValid)
            {
                var teste = Request.Form["LogotipoBase64"];
                if (logotipo != null && logotipo.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await logotipo.CopyToAsync(memoryStream);
                        cliente.Logotipo = memoryStream.ToArray();
                    }
                }
                else if (!string.IsNullOrEmpty(Request.Form["LogotipoBase64"]))
                {
                    cliente.Logotipo = Convert.FromBase64String(Request.Form["LogotipoBase64"]);
                }

                if (cliente.Logotipo == null)
                {
                    ModelState.AddModelError("", "Logotipo não informada");

                    TempData["MensagemErro"] = "Preencha a imagem";

                    return RedirectToAction("Editar", new { id = cliente.Id });
                }

                // Enviar o cliente e seus logradouros para a API
                var response = await client.PutAsJsonAsync(apiBaseUrl + "Cliente/", cliente);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Erro ao editar cliente");
            }
            else
            {
                TempData["MensagemErro"] = "Preencha todos os campos";
                return RedirectToAction("Editar", new { id = cliente.Id });
            }

            return View(cliente);
        }

        // Excluir cliente
        public async Task<ActionResult> Excluir(int id)
        {
            await ConfigurarAutenticacaoAsync();

            var response = await client.DeleteAsync(apiBaseUrl + "Cliente/Excluir/" + id);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);|
            return Ok(HttpStatusCode.BadRequest);
        }
        private async Task ConfigurarAutenticacaoAsync()
        {
            var token = await ObterTokenAutenticacaoAsync();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        private async Task<string> ObterTokenAutenticacaoAsync()
        {
            // Supondo que a API possua um endpoint para autenticação e retorno de token
            var response = await client.PostAsJsonAsync(apiBaseUrl + "Auth/Login", new
            {
                Usuario = "administrador",
                Senha = "senha@123"
            });

            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadAsAsync<TokenResult>();
                if (tokenResponse != null && !string.IsNullOrEmpty(tokenResponse.Token))
                    return tokenResponse.Token;

                return "autenticacão falhou";
            }

            throw new Exception("Erro ao obter o token de autenticação");
        }
    }
}

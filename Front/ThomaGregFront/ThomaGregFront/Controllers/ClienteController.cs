using System.Net;
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
        public ActionResult Inserir()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Inserir(ClienteDTO cliente, IFormFile logotipo)
        {
            cliente.Logradouros = new List<LogradouroDTO>();
            if (ModelState.IsValid)
            {
                if (logotipo != null && logotipo.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await logotipo.CopyToAsync(memoryStream);
                        cliente.Logotipo = memoryStream.ToArray(); // Convertendo a imagem para byte[]
                    }
                }

                // Agora você pode enviar o cliente para sua API ou banco de dados
                var response = await client.PostAsJsonAsync(apiBaseUrl + "Cliente/", cliente);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Erro ao criar cliente");
            }

            return View(cliente);
        }

        // Editar cliente
        public async Task<ActionResult> Editar(int id)
        {
            var response = await client.GetAsync(apiBaseUrl + "Cliente/Buscar/" + id);
            var cliente = await response.Content.ReadAsAsync<BuscarPorIdResposta>();
            return View(cliente.ClienteDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Editar(int id, ClienteDTO cliente, IFormFile logotipo)
        {
            if (ModelState.IsValid)
            {
                // Caso o logotipo não tenha sido alterado, mantemos o logotipo atual
                if (logotipo != null && logotipo.Length > 0)
                {
                    // Se houver uma nova imagem, processa o upload
                    using (var memoryStream = new MemoryStream())
                    {
                        await logotipo.CopyToAsync(memoryStream);
                        cliente.Logotipo = memoryStream.ToArray(); // Atualiza o logotipo com a nova imagem
                    }
                }

                //// Serializando o cliente com Newtonsoft.Json
                //var jsonContent = JsonConvert.SerializeObject(cliente); // Serializa o objeto ClienteDTO

                //// Criando o conteúdo para o PUT (enviando os dados como JSON)
                //var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Enviando os dados do cliente para o backend (API ou Banco de Dados)
                var response = await client.PutAsJsonAsync(apiBaseUrl + "Cliente/", cliente);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index"); // Redireciona para a lista de clientes
                }

                ModelState.AddModelError("", "Erro ao editar cliente");
            }

            return View(cliente);
        }

        // Excluir cliente
        public async Task<ActionResult> Excluir(int id)
        {
            var response = await client.DeleteAsync(apiBaseUrl + "Cliente/Excluir/" + id);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);|
            return Ok(HttpStatusCode.BadRequest);
        }
    }
}

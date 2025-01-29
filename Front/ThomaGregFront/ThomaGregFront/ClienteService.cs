using ThomaGregFront.Models;

namespace ThomaGregFront
{
    public class ClienteService
    {
        private string apiBaseUrl = "http://localhost:5183/"; // URL da sua API
        private readonly HttpClient client;

        public ClienteService(HttpClient client, string apiBaseUrl)
        {
            this.client = client;
            this.apiBaseUrl = apiBaseUrl;
        }

        public async void Inserir(ClienteDTO cliente)
        {
            // Agora você pode enviar o cliente para sua API ou banco de dados
            var response = await client.PostAsJsonAsync(apiBaseUrl + "Cliente/", cliente);

            if (response == null || !response.IsSuccessStatusCode)
            {

            }
        }
    }
}

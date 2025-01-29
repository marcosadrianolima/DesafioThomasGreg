

using POC.ThomasGreg.Cadastro.Domain.Features.Logradouro.Entidade;

namespace POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Entidades
{
    public class ClienteVO
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public List<LogradouroVO> Logradouros { get; set; }

        public byte[] Logotipo { get; set; }

        public void Atualizar(ClienteVO cliente)
        {
            Nome = cliente.Nome;
            Logotipo = cliente.Logotipo;
            Logradouros = cliente.Logradouros;
        }

        public (bool, string) Valido()
        {
            string camposInvalidosMensagem = string.Empty;
            bool valido = true;

            if (string.IsNullOrEmpty(Nome))
            {
                valido = false;
                camposInvalidosMensagem += "'Nome' ";
            }

            if (string.IsNullOrEmpty(Email))
            {
                valido = false;
                camposInvalidosMensagem += "'Email' ";
            }

            return (valido, camposInvalidosMensagem.Trim());
        }
    }
}

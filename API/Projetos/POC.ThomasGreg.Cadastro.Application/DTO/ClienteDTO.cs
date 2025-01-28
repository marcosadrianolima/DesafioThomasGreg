namespace POC.ThomasGreg.Cadastro.Application.DTO
{
    public class ClienteDTO
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public List<LogradouroDTO> Logradouros { get; set; }

        public byte[] Logotipo { get; set; }
    }
}

﻿
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
            Email = cliente.Email;
            Logotipo = cliente.Logotipo;
        }
    }
}

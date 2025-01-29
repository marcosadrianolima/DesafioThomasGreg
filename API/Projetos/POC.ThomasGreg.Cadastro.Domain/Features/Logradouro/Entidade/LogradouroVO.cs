namespace POC.ThomasGreg.Cadastro.Domain.Features.Logradouro.Entidade
{
    public class LogradouroVO
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        public long ClienteId { get; set; }

        public (bool isValid, object mensagemErro) Valido()
        {
            string mensagemErro = string.Empty;
            bool isValido = true;

            if (string.IsNullOrEmpty(Nome))
            {
                mensagemErro = "O Nome deve ser informado";
                isValido = false;
            }


            return (isValido, mensagemErro);
        }
    }
}

namespace POC.ThomasGreg.Cadastro.Application.Compartilhado
{
    public abstract class RespostaBase
    {
        public bool IsSucess { get; set; }
        public string Mensagem { get; set; }
    }
}

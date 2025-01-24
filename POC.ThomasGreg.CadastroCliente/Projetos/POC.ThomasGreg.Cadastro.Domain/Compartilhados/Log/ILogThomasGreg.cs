namespace POC.ThomasGreg.Cadastro.Domain.Compartilhados.Log
{
    public interface ILogThomasGreg
    {
        ILogThomasGreg IdentificadorLog(string mensagem);
        void Debug(string mensagem);
        void Information(string mensagem);
        void Verbose(string mensagem);
        void Warning(string mensagem);
        void Erro(string mensagem);
    }
}

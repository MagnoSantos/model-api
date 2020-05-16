namespace sample.API
{
    public class Falha
    {
        public Erro Error { get; }

        public Falha(string mensagem)
        {
            Error = new Erro { Mensagem = mensagem };
        }
    }

    public class Erro
    {
        public string Mensagem { get; set; }
    }
}
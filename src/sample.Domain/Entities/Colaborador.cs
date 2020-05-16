namespace sample.Domain.Entities
{
    public class Colaborador
    {
        public string Status { get; set; }

        public string Nome { get; set; }

        public string Id { get; set; }

        /// <summary>
        /// Construtor com base no retorno da requisição realizada
        /// </summary>
        /// <param name="status"></param>
        /// <param name="nome"></param>
        /// <param name="id"></param>
        public Colaborador(string status, string nome, string id)
        {
            Status = status;
            Nome = nome;
            Id = id;
        }
    }
}
namespace sample.Domain.Entities
{
    public class Colaborador
    {
        public string status { get; set; }

        public Dados data { get; set; }

        /// <summary>
        /// Construtor baseado no retorno da requisição
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="Data"></param>
        public Colaborador(string Status, Dados Data)
        {
            status = Status;
            data = Data;
        }
    }
}
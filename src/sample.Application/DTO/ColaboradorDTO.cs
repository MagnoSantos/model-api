using FluentValidation;
using System.Text.Json.Serialization;

namespace sample.Application.DTO
{
    public class ColaboradorDTO
    {
        [JsonPropertyName("name")]
        public string Nome { get; set; }

        [JsonPropertyName("salary")]
        public string Salario { get; set; }

        [JsonPropertyName("age")]
        public string Idade { get; set; }

        /// <summary>
        /// Validação dos parâmetros do corpo da requisição, no caso os dados do DTO: Colaborador
        /// Link de referência: https://fluentvalidation.net/
        /// </summary>
        public class ColaboradorValidator : AbstractValidator<ColaboradorDTO>
        {
            public ColaboradorValidator()
            {
                RuleFor(colaborador => colaborador.Nome)
                    .NotEmpty().WithMessage("O campo de nome deve ser preenchido")
                    .NotNull().WithMessage("O campo de nome não pode ser nulo");

                RuleFor(colaborador => colaborador.Salario)
                    .NotEmpty().WithMessage("O campo de salario deve ser preenchido")
                    .NotNull().WithMessage("O campo de salario não pode ser nulo");

                RuleFor(colaborador => colaborador.Idade)
                    .NotEmpty().WithMessage("O campo de idade deve ser preenchido")
                    .NotNull().WithMessage("O campo de idade não pode ser nulo");
            }
        }
    }
}
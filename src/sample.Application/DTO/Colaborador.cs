using FluentValidation;
using System.Text.Json.Serialization;

namespace sample.Application.DTO
{
    public class Colaborador
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
        public class ColaboradorValidator : AbstractValidator<Colaborador>
        {
            public ColaboradorValidator()
            {
                RuleFor(colaborador => colaborador.Nome)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotEmpty().WithMessage("O campo de texto deve ser preenchido")
                    .NotNull().WithMessage("O campo de texto não pode ser nulo");

                RuleFor(colaborador => colaborador.Salario)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotEmpty().WithMessage("O campo de data deve ser preenchido")
                    .NotNull().WithMessage("O campo de data não pode ser nulo");

                RuleFor(colaborador => colaborador.Idade)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotEmpty().WithMessage("O campo de data deve ser preenchido")
                    .NotNull().WithMessage("O campo de data não pode ser nulo");
            }
        }
    }
}
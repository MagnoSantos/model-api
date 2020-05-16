using AutoFixture;
using FluentValidation.TestHelper;
using NUnit.Framework;
using sample.Application.DTO;
using static sample.Application.DTO.ColaboradorDTO;

namespace sample.UnitTests.Application
{
    [TestFixture]
    public class TestesColaboradorValidator
    {
        private ColaboradorValidator _colaboradorValidator;

        private Fixture _fixture;

        [SetUp]
        public void ConfiguraDependencias()
        {
            _colaboradorValidator = new ColaboradorValidator();
        }

        [OneTimeSetUp]
        public void ConfiguraFixture()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void DeveRetornarErroQuandoONomeDoColaboradorForNullo()
        {
            _colaboradorValidator.ShouldHaveValidationErrorFor(colaborador => colaborador.Nome, null as string);
        }

        [Test]
        public void DeveRetornarQueNaoExisteErroComNomeDoColaboradorEspecificado()
        {
            var colaborador = _fixture.Build<ColaboradorDTO>()
                .With(c => c.Nome, "Magno")
                .Create();

            _colaboradorValidator.ShouldNotHaveValidationErrorFor(c => c.Nome, colaborador);
        }

        [Test]
        public void DeveRetornarErroComOColaboradorDTONaoPreenchido()
        {
            var colaborador = _fixture.Build<ColaboradorDTO>()
                .With(c => c.Nome, string.Empty)
                .With(c => c.Idade, string.Empty)
                .With(c => c.Salario, string.Empty)
                .Create();

            var resultado = _colaboradorValidator.TestValidate(colaborador);

            resultado.ShouldHaveValidationErrorFor(c => c.Nome)
                .WithErrorMessage("O campo de nome deve ser preenchido");

            resultado.ShouldHaveValidationErrorFor(c => c.Salario)
                .WithErrorMessage("O campo de salario deve ser preenchido");

            resultado.ShouldHaveValidationErrorFor(c => c.Idade)
                .WithErrorMessage("O campo de idade deve ser preenchido");
        }

        [Test]
        public void DeveRetornarErroComOColaboradorDTONullo()
        {
            var colaborador = _fixture.Build<ColaboradorDTO>()
                .With(c => c.Nome, null as string)
                .With(c => c.Idade, null as string)
                .With(c => c.Salario, null as string)
                .Create();

            var resultado = _colaboradorValidator.TestValidate(colaborador);

            resultado.ShouldHaveValidationErrorFor(c => c.Nome)
                .WithErrorMessage("O campo de nome não pode ser nulo");

            resultado.ShouldHaveValidationErrorFor(c => c.Salario)
                .WithErrorMessage("O campo de salario não pode ser nulo");

            resultado.ShouldHaveValidationErrorFor(c => c.Idade)
                .WithErrorMessage("O campo de idade não pode ser nulo");
        }
    }
}
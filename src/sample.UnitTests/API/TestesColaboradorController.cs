using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using sample.API;
using sample.API.Controllers;
using sample.Application;
using sample.Application.DTO;
using sample.Application.Repositories.Interfaces;
using sample.Domain.Entities;
using System.Threading.Tasks;

namespace sample.UnitTests.API
{
    [TestFixture]
    public class TestesColaboradorController
    {
        private readonly Fixture _fixture = new Fixture();
        private Mock<IColaboradorRepository> _colaboradorRepository;

        [SetUp]
        public void SetupMocks()
        {
            _colaboradorRepository = new Mock<IColaboradorRepository>();
        }

        [Test]
        [Description("POST api/colaboradores")]
        public async Task DeveRetornar422SeNaoConseguirProcessarAAdicaoDoColaborador()
        {
            var colaborador = _fixture.Create<ColaboradorDTO>();
            _colaboradorRepository
                .Setup(mock => mock.AdicionarColaborador(colaborador))
                .ReturnsAsync(null as Colaborador);
            var controller = Instanciar();

            var resultado = await controller.CadastrarColaborador(colaborador);

            resultado
                .Should()
                .BeOfType<UnprocessableEntityObjectResult>()
                .Which.Value
                .Should()
                .BeEquivalentTo(new Falha(Constantes.Mensagens.ErroProcessamento));
        }

        [Test]
        [Description("POST api/colaboradores")]
        public async Task DeveRetornar200SeConseguirProcessarAAdicaoDoColaborador()
        {
            var colaborador = _fixture.Create<ColaboradorDTO>();
            var colaboradorRetorno = _fixture.Create<Colaborador>();
            _colaboradorRepository
                .Setup(mock => mock.AdicionarColaborador(colaborador))
                .ReturnsAsync(colaboradorRetorno);
            var controller = Instanciar();

            var resultado = await controller.CadastrarColaborador(colaborador);

            resultado
                .Should()
                .BeOfType<OkObjectResult>()
                .Which.StatusCode
                .Should()
                .Be(200);

            resultado
                .Should()
                .BeOfType<OkObjectResult>()
                .Which.Value
                .Should()
                .BeEquivalentTo(colaboradorRetorno);
        }

        [Test]
        [Description("POST api/colaboradores")]
        public async Task DeveRetornar400SeOModelStateForInvalido()
        {
            var colaborador = _fixture.Create<ColaboradorDTO>();
            var controller = Instanciar();
            controller.ModelState.AddModelError("erroFalso", "erroFalso");

            var resultado = await controller.CadastrarColaborador(colaborador);

            resultado
                .Should()
                .BeOfType<BadRequestResult>()
                .Which.StatusCode
                .Should()
                .Be(400);
        }

        private ColaboradorController Instanciar()
        {
            return new ColaboradorController(
                _colaboradorRepository.Object
            );
        }
    }
}
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using NUnit.Framework;
using sample.API.HealthChecks;
using sample.Infrastructure.Responses;
using sample.Infrastructure.Services;
using System;
using System.Threading.Tasks;

namespace sample.UnitTests.API
{
    [TestFixture]
    public class TestesDummyAPIHealthCheck
    {
        private readonly Fixture _fixture = new Fixture();
        private Mock<IColaboradorService> _colaboradorService;

        [SetUp]
        public void SetupMocks()
        {
            _colaboradorService = new Mock<IColaboradorService>();
        }

        [Test]
        public async Task DeveChamarOMetodoDeVerificacaoDummyAPIHealth()
        {
            var colaborador = _fixture.Create<BuscarColaboradoresResponse>();
            var context = new HealthCheckContext();
            _colaboradorService
                .Setup(mock => mock.BuscarTodosOsColaboradores())
                .Returns(Task.FromResult(colaborador));
            var health = Instanciar();

            var resposta = await health.CheckHealthAsync(context, default);

            resposta.Should().NotBeNull();
            resposta.Status.Should().Be(HealthStatus.Healthy);
        }

        [Test]
        public async Task DeveRetornarUnhealthSeOcorrerAlgumErroAoBuscarColaboradores()
        {
            var context = new HealthCheckContext();
            _colaboradorService
                .Setup(mock => mock.BuscarTodosOsColaboradores())
                .Throws(new Exception());
            var health = Instanciar();

            var resposta = await health.CheckHealthAsync(context, default);

            resposta.Status.Should().Be(HealthStatus.Unhealthy);
            resposta.Description.Should().Be("Falha ao obter todos os colaboradores");
        }

        private DummyAPIHealthCheck Instanciar()
        {
            return new DummyAPIHealthCheck(
                _colaboradorService.Object
            );
        }
    }
}
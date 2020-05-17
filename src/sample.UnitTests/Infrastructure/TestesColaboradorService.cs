using AutoFixture;
using Flurl.Http.Testing;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using sample.Application.DTO;
using sample.Infrastructure;
using sample.Infrastructure.Responses;
using sample.Infrastructure.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace sample.UnitTests.Infrastructure
{
    [TestFixture]
    public class TestesColaboradorService
    {
        private readonly Fixture _fixture = new Fixture();
        private HttpTest _httpTest;
        private Mock<IOptionsMonitor<ConfiguracoesAplicacao>> _configuracoesAplicacao;
        private ConfiguracoesAplicacao _config;

        [SetUp]
        public void SetupMocks()
        {
            _httpTest = new HttpTest();

            _configuracoesAplicacao = new Mock<IOptionsMonitor<ConfiguracoesAplicacao>>();
            _config = _fixture
                .Build<ConfiguracoesAplicacao>()
                .With(c => c.UrlDummy, _fixture.Create<Uri>().ToString())
                .Create();
            _configuracoesAplicacao
                .Setup(c => c.CurrentValue)
                .Returns(_config);
        }

        [TearDown]
        public void TearDown()
        {
            _httpTest.Dispose();
        }

        [Test]
        public async Task DeveChamarAPIDummyCorretamente()
        {
            _httpTest.RespondWithJson(_fixture.Create<AdicionarColaboradorResponse>());
            var colaboradorDTO = _fixture.Create<ColaboradorDTO>();
            var service = Instanciar();

            await service.AdicionarColaborador(
                colaboradorDTO.Nome, 
                colaboradorDTO.Salario,
                colaboradorDTO.Idade
            );

            _httpTest
                .ShouldHaveCalled(_config.UrlDummy)
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(new
                {
                    name = $"{colaboradorDTO.Nome}",
                    salario = $"{colaboradorDTO.Salario}",
                    idade = $"{colaboradorDTO.Idade}"
                });
        }

        private ColaboradorService Instanciar()
        {
            return new ColaboradorService(
                _configuracoesAplicacao.Object
            );
        }
    }
}
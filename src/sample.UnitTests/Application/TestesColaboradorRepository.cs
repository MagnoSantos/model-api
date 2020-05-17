using AutoFixture;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using sample.Application.DTO;
using sample.Application.Repositories;
using sample.Infrastructure.Responses;
using sample.Infrastructure.Services;
using System.Threading.Tasks;

namespace sample.UnitTests.Application
{
    [TestFixture]
    public class TestesColaboradorRepository
    {
        private readonly Fixture _fixture = new Fixture();
        private Mock<IColaboradorService> _colaboradorService;

        [SetUp]
        public void SetupMocks()
        {
            _colaboradorService = new Mock<IColaboradorService>();
        }

        [Test]
        public async Task DeveChamarAoMenosUmaVezOMetodoDeAdicionarCliente()
        {
            var colaboradorDTO = _fixture.Create<ColaboradorDTO>();
            var adicionarColaboradorResponse = _fixture.Create<AdicionarColaboradorResponse>();
            _colaboradorService.Setup(
                mock => mock.AdicionarColaborador(
                    colaboradorDTO.Nome,
                    colaboradorDTO.Salario,
                    colaboradorDTO.Idade
                )
            ).ReturnsAsync(adicionarColaboradorResponse);
            var respository = Instanciar();

            await respository.AdicionarColaborador(colaboradorDTO);

            _colaboradorService.Verify(
                mock => mock.AdicionarColaborador(
                    colaboradorDTO.Nome,
                    colaboradorDTO.Salario,
                    colaboradorDTO.Idade
                ),
                Times.Once
            );
        }

        [Test]
        public async Task DeveRetornarNullSeColaboradorNaoExistir()
        {
            var colaboradorDTO = _fixture.Create<ColaboradorDTO>();
            var respository = Instanciar();

            var resultado = await respository.AdicionarColaborador(colaboradorDTO);

            resultado.Should().BeNull();
        }

        [Test]
        public async Task DeveRetornarColaboradorQueFoiAdicionado()
        {
            var colaboradorDTO = _fixture.Create<ColaboradorDTO>();
            var adicionarColaboradorResponse = _fixture.Create<AdicionarColaboradorResponse>();
            _colaboradorService.Setup(
                mock => mock.AdicionarColaborador(
                    colaboradorDTO.Nome,
                    colaboradorDTO.Salario,
                    colaboradorDTO.Idade
                )
            ).ReturnsAsync(adicionarColaboradorResponse);
            var respository = Instanciar();

            var resposta = await respository.AdicionarColaborador(colaboradorDTO);

            resposta.Should().NotBeNull();
            resposta.Nome.Should().Be(adicionarColaboradorResponse.Dados?.Nome);
            resposta.Id.Should().Be(adicionarColaboradorResponse.Dados?.Id);
        }

        private ColaboradorRepository Instanciar()
        {
            return new ColaboradorRepository(
                _colaboradorService.Object
            );
        }
    }
}
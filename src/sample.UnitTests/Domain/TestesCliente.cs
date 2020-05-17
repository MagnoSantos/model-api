using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using sample.Domain.Entities;

namespace sample.UnitTests.Domain
{
    [TestFixture]
    public class TestesCliente
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void DeveConstruirCorretamenteOColaborador()
        {
            var status = _fixture.Create<string>();
            var nome = _fixture.Create<string>();
            var id = _fixture.Create<string>();

            var colaborador = new Colaborador(status, nome, id);

            colaborador.Status.Should().Be(status);
            colaborador.Nome.Should().Be(nome);
            colaborador.Id.Should().Be(id);
        }
    }
}
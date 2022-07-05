using AutoFixture;
using AutoMapper;
using Moq;
using System.Linq.Expressions;
using TesteBancoMaster.API.Configurations;
using TesteBancoMaster.API.Models;
using TesteBancoMaster.API.Services;
using TesteBancoMaster.Infra.Data;
using TesteBancoMaster.Infra.Entities;
using TesteBancoMaster.Infra.Repositories;
using TesteBancoMaster.Tests.Helpers;

namespace TesteBancoMaster.Tests.Services
{
    public class ViagemServiceTests
    {
        [Fact]
        public async void Deve_Obter_Todas_Viagens()
        {
            Mock<IViagemRepository> viagemRepositoryMock = new Mock<IViagemRepository>();
            viagemRepositoryMock.Setup(x => x.ObterTodos())
                .ReturnsAsync(FixtureHelper.ObterListaLocalizacoes());

            Mock<IMapper> mapperMock = new Mock<IMapper>();
            ViagemService service = new ViagemService(viagemRepositoryMock.Object, mapperMock.Object);

            var result = await service.ObterTodos();

            Assert.NotNull(result);
            Assert.Equal(7, result.Count);
        }

        [Fact]
        public async void Deve_Obter_Todos_Destinos()
        {
            Mock<IViagemRepository> viagemRepositoryMock = new Mock<IViagemRepository>();
            viagemRepositoryMock.Setup(x => x.ObterViagens(It.IsAny<Expression<Func<Viagem, bool>>>()))
                .ReturnsAsync(FixtureHelper.ObterListaLocalizacoes());

            Mock<IMapper> mapperMock = new Mock<IMapper>();
            ViagemService service = new ViagemService(viagemRepositoryMock.Object, mapperMock.Object);

            var result = await service.ObterDestinos("GRU");

            Assert.NotNull(result);
            Assert.Equal(7, result.Count);
        }

        [Fact]
        public async void Deve_Cadastrar_Viagem_Com_Sucesso()
        {
            var fixture = new Fixture();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Commit())
                .ReturnsAsync(true);

            Mock<IViagemRepository> viagemRepositoryMock = new Mock<IViagemRepository>();
            viagemRepositoryMock.SetupProperty(x => x.UnitOfWork, unitOfWorkMock.Object);
            viagemRepositoryMock.Setup(x => x.ObterViagens(It.IsAny<Expression<Func<Viagem, bool>>>()))
                .ReturnsAsync(new List<Viagem>());

            var viagem = fixture.Create<Viagem>();
            viagemRepositoryMock.Setup(x => x.CadastrarViagem(viagem));

            var myProfile = new MyProfile();
            var config = new MapperConfiguration(x => x.AddProfile<MyProfile>());
            var mapper = config.CreateMapper();

            ViagemService service = new ViagemService(viagemRepositoryMock.Object, mapper);

            var viagemModel = fixture.Create<ViagemCadastroModelRequest>();
            var result = await service.CadastrarViagem(viagemModel);

            Assert.NotNull(result);
            Assert.Equal(viagemModel.Origem, result.Origem);
            Assert.Equal(viagemModel.Destino, result.Destino);
            Assert.Equal(viagemModel.ValorPassagem, result.ValorPassagem);
        }

        [Fact]
        public async void Deve_Voltar_Erro_Cadastrar_Viagem()
        {
            var fixture = new Fixture();

            Mock<IViagemRepository> viagemRepositoryMock = new Mock<IViagemRepository>();
            viagemRepositoryMock.Setup(x => x.ObterViagens(It.IsAny<Expression<Func<Viagem, bool>>>()))
                .ReturnsAsync(fixture.CreateMany<Viagem>(1).ToList());

            Mock<IMapper> mapperMock = new Mock<IMapper>();

            ViagemService service = new ViagemService(viagemRepositoryMock.Object, mapperMock.Object);

            var viagemModel = fixture.Create<ViagemCadastroModelRequest>();
            var servico = () => service.CadastrarViagem(viagemModel);

            var result = await Assert.ThrowsAsync<ArgumentException>(servico);
            Assert.Equal("Viagem já existente", result.Message);
        }

        [Fact]
        public async void Deve_Voltar_Erro_Valor_Negativo_Cadastrar_Viagem()
        {
            var fixture = new Fixture();

            Mock<IViagemRepository> viagemRepositoryMock = new Mock<IViagemRepository>();
            viagemRepositoryMock.Setup(x => x.ObterViagens(It.IsAny<Expression<Func<Viagem, bool>>>()))
                .ReturnsAsync(new List<Viagem>());

            Mock<IMapper> mapperMock = new Mock<IMapper>();

            ViagemService service = new ViagemService(viagemRepositoryMock.Object, mapperMock.Object);

            var viagemModel = fixture.Build<ViagemCadastroModelRequest>()
                .With(x => x.ValorPassagem, -8)
                .Create();
            
            var servico = () => service.CadastrarViagem(viagemModel);

            var result = await Assert.ThrowsAsync<ArgumentException>(servico);
            Assert.Equal("O valor da passagem deve ser maior que 5", result.Message);
        }

        [Fact]
        public async void Deve_Atualizar_Viagem_Com_Sucesso()
        {
            var fixture = new Fixture();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Commit())
                .ReturnsAsync(true);

            Mock<IViagemRepository> viagemRepositoryMock = new Mock<IViagemRepository>();
            viagemRepositoryMock.SetupProperty(x => x.UnitOfWork, unitOfWorkMock.Object);
            viagemRepositoryMock.Setup(x => x.ObterViagem(It.IsAny<Expression<Func<Viagem, bool>>>()))
                .ReturnsAsync(fixture.Create<Viagem>());

            var viagem = fixture.Create<Viagem>();

            viagemRepositoryMock.Setup(x => x.AtualizarViagem(viagem));

            var myProfile = new MyProfile();
            var config = new MapperConfiguration(x => x.AddProfile<MyProfile>());
            var mapper = config.CreateMapper();

            ViagemService service = new ViagemService(viagemRepositoryMock.Object, mapper);

            var viagemModel = fixture.Create<ViagemAtualizarModelRequest>();
            var result = await service.AtualizarViagem(It.IsAny<int>(), viagemModel);

            Assert.NotNull(result);
            Assert.Equal(viagemModel.Destino, result.Destino);
            Assert.Equal(viagemModel.ValorPassagem, result.ValorPassagem);
        }

        [Fact]
        public async void Deve_Voltar_Erro_Atualizar_Viagem()
        {
            var fixture = new Fixture();

            Mock<IViagemRepository> viagemRepositoryMock = new Mock<IViagemRepository>();
            viagemRepositoryMock.Setup(x => x.ObterViagem(It.IsAny<Expression<Func<Viagem, bool>>>()))
                .ReturnsAsync((Viagem)null);

            Mock<IMapper> mapperMock = new Mock<IMapper>();

            ViagemService service = new ViagemService(viagemRepositoryMock.Object, mapperMock.Object);

            var viagemModel = fixture.Create<ViagemAtualizarModelRequest>();
            var servico = () => service.AtualizarViagem(It.IsAny<int>(), viagemModel);

            var result = await Assert.ThrowsAsync<ArgumentException>(servico);
            Assert.Equal("Viagem não existente", result.Message);
        }

        [Fact]
        public async void Deve_Deletar_Viagem_Com_Sucesso()
        {
            var fixture = new Fixture();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Commit())
                .ReturnsAsync(true);

            Mock<IViagemRepository> viagemRepositoryMock = new Mock<IViagemRepository>();
            viagemRepositoryMock.SetupProperty(x => x.UnitOfWork, unitOfWorkMock.Object);
            viagemRepositoryMock.Setup(x => x.ObterViagem(It.IsAny<Expression<Func<Viagem, bool>>>()))
                .ReturnsAsync(fixture.Create<Viagem>());

            viagemRepositoryMock.Setup(x => x.DeletarViagem(It.IsAny<Viagem>()))
                .Returns(true);

            var myProfile = new MyProfile();
            var config = new MapperConfiguration(x => x.AddProfile<MyProfile>());
            var mapper = config.CreateMapper();

            ViagemService service = new ViagemService(viagemRepositoryMock.Object, mapper);

            var result = await service.DeletarViagem(It.IsAny<int>());

            Assert.True(result);
        }

        [Fact]
        public async void Deve_Voltar_Erro_Deletar_Viagem()
        {
            var fixture = new Fixture();

            Mock<IViagemRepository> viagemRepositoryMock = new Mock<IViagemRepository>();
            viagemRepositoryMock.Setup(x => x.ObterViagem(It.IsAny<Expression<Func<Viagem, bool>>>()))
                .ReturnsAsync((Viagem)null);

            Mock<IMapper> mapperMock = new Mock<IMapper>();

            ViagemService service = new ViagemService(viagemRepositoryMock.Object, mapperMock.Object);

            var servico = () => service.DeletarViagem(It.IsAny<int>());

            var result = await Assert.ThrowsAsync<ArgumentException>(servico);
            Assert.Equal("Viagem não existente", result.Message);
        }

        [Theory]
        [InlineData("GRU", "CDG", "GRU -> BRC -> SCL -> ORL -> CDG ao custo de 40")]
        [InlineData("GRU", "BRC", "GRU -> BRC ao custo de 10")]
        [InlineData("BRC", "ORL", "BRC -> SCL -> ORL ao custo de 25")]
        public async void Deve_Retornar_Sucesso_Rota_Custo_Baixo(string origem, string destino, string resultadoEsperado)
        {
            Mock<IViagemRepository> viagemRepositoryMock = new Mock<IViagemRepository>();
            viagemRepositoryMock.Setup(x => x.ObterTodos())
                .ReturnsAsync(FixtureHelper.ObterListaLocalizacoes());
            
            Mock<IMapper> mapperMock = new Mock<IMapper>();
            ViagemService service = new ViagemService(viagemRepositoryMock.Object, mapperMock.Object);

            var model = new ViagemObterRotaCustoBaixoModelRequest
            {
                Origem = origem,
                Destino = destino
            };
            var result = await service.ObterRotaCustoBaixo(model);

            Assert.NotNull(result);
            Assert.Equal(resultadoEsperado, result);
        }

        [Fact]
        public async void Deve_Retornar_Erro_Rota_Custo_Baixo()
        {
            Mock<IViagemRepository> viagemRepositoryMock = new Mock<IViagemRepository>();
            viagemRepositoryMock.Setup(x => x.ObterTodos())
                .ReturnsAsync(new List<Viagem>());

            Mock<IMapper> mapperMock = new Mock<IMapper>();

            ViagemService service = new ViagemService(viagemRepositoryMock.Object, mapperMock.Object);
            var servico = () => service.ObterRotaCustoBaixo(It.IsAny<ViagemObterRotaCustoBaixoModelRequest>());

            var result = await Assert.ThrowsAsync<ArgumentException>(servico);
            Assert.Equal("O destino/origem informados não existem", result.Message);
        }
    }
}
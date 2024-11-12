
using _1___Entities;
using _2___Services._Exceptions;
using _2___Services._Interfaces;
using _2___Services.Services.BeerService;
using _3___Mappers.Dtos.BeerDtos;
using AutoMapper;
using Moq;

namespace _2___Services.Tests.BeerServiceTests
{
    public class DeleteBeerTest
    {
        private readonly Mock<IRepository<BeerEntity>> _beerRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly DeleteBeerUseCase<BeerDto> _deleteBeerUseCase;

        public DeleteBeerTest()
        {
            _beerRepositoryMock = new Mock<IRepository<BeerEntity>>();
            _mapperMock = new Mock<IMapper>();
            _deleteBeerUseCase = new DeleteBeerUseCase<BeerDto>(
                _beerRepositoryMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async Task ServiceReturnsDto_Correctly()
        {
            _beerRepositoryMock.Setup(r => r.DeleteAsync(It.IsAny<int>())).ReturnsAsync(new BeerEntity());
            _mapperMock.Setup(m => m.Map<BeerDto>(It.IsAny<BeerEntity>())).Returns(new BeerDto());

            var actual = await _deleteBeerUseCase.ExecuteAsync(It.IsAny<int>());

            Assert.NotNull(actual);
            Assert.IsType<BeerDto>(actual);

            _beerRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Once());
            _mapperMock.Verify(m => m.Map<BeerDto>(It.IsAny<BeerEntity>()), Times.Once());
        }

        [Fact]
        public async Task IfEntityValidationIsFalse_ThrowException()
        {
            int idBeer = 1;

            _beerRepositoryMock.Setup(r => r.DeleteAsync(It.IsAny<int>())).ReturnsAsync((BeerEntity)null);

            var actualException = await Assert.ThrowsAsync<NotFoundException>(
                () => _deleteBeerUseCase.ExecuteAsync(idBeer));
            Assert.Equal($"No se encontró ninguna cerveza con ID {idBeer}", actualException.Message);

            _beerRepositoryMock.Verify(r => r.DeleteAsync(idBeer), Times.Once());
        }
    }
}


using _1___Entities;
using _2___Services._Exceptions;
using _2___Services._Interfaces;
using _2___Services.Services.BeerService;
using _3___Mappers.Dtos.BeerDtos;
using AutoMapper;
using Moq;

namespace _2___Services.Tests.BeerServiceTests
{
    public class UpdateBeerTest
    {
        private readonly Mock<IRepository<BeerEntity>> _beerRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IEntityValidator<BeerEntity>> _entityValidatorMock;
        private readonly UpdateBeerUseCase<BeerUpdateDto, BeerDto> _updateBeerUseCase;

        public UpdateBeerTest()
        {
            _beerRepositoryMock = new Mock<IRepository<BeerEntity>>();
            _mapperMock = new Mock<IMapper>();
            _entityValidatorMock = new Mock<IEntityValidator<BeerEntity>>();
            _updateBeerUseCase = new UpdateBeerUseCase<BeerUpdateDto, BeerDto>(
                _beerRepositoryMock.Object,
                _mapperMock.Object,
                _entityValidatorMock.Object);
        }

        [Fact]
        public async Task ServiceValidatesAndReturnsDto_Correctly()
        {
            _mapperMock.Setup(m => m.Map<BeerEntity>(It.IsAny<BeerUpdateDto>())).Returns(new BeerEntity());
            _entityValidatorMock.Setup(v => v.ValidateAsync(It.IsAny<BeerEntity>())).ReturnsAsync(true);
            _beerRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<BeerEntity>(), It.IsAny<int>())).ReturnsAsync(new BeerEntity());
            _mapperMock.Setup(m => m.Map<BeerDto>(It.IsAny<BeerEntity>())).Returns(new BeerDto());

            var actual = await _updateBeerUseCase.ExecuteAsync(new BeerUpdateDto(), It.IsAny<int>());

            Assert.NotNull(actual);
            Assert.IsType<BeerDto>(actual);

            _mapperMock.Verify(m => m.Map<BeerEntity>(It.IsAny<BeerUpdateDto>()), Times.Once());
            _entityValidatorMock.Verify(v => v.ValidateAsync(It.IsAny<BeerEntity>()), Times.Once());
            _beerRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<BeerEntity>(), It.IsAny<int>()), Times.Once());
            _mapperMock.Verify(m => m.Map<BeerDto>(It.IsAny<BeerEntity>()), Times.Once());
        }

        [Fact]
        public async Task IfEntityValidationIsFalse_ThrowException()
        {
            _mapperMock.Setup(m => m.Map<BeerEntity>(It.IsAny<BeerUpdateDto>())).Returns(new BeerEntity());
            _entityValidatorMock.Setup(v => v.ValidateAsync(It.IsAny<BeerEntity>())).ReturnsAsync(false);

            var actualException = await Assert.ThrowsAsync<EntityValidationException>(
                () => _updateBeerUseCase.ExecuteAsync(new BeerUpdateDto(), It.IsAny<int>()));

            _mapperMock.Verify(m => m.Map<BeerEntity>(It.IsAny<BeerUpdateDto>()), Times.Once());
            _entityValidatorMock.Verify(v => v.ValidateAsync(It.IsAny<BeerEntity>()), Times.Once());
        }

        [Fact]
        public async Task IfBeerNotFound_ThrowException()
        {
            int idBeer = 1;

            _beerRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<BeerEntity>(), It.IsAny<int>())).ReturnsAsync((BeerEntity)null);
            _entityValidatorMock.Setup(v => v.ValidateAsync(It.IsAny<BeerEntity>())).ReturnsAsync(true);

            var actualException = await Assert.ThrowsAsync<NotFoundException>(
                () => _updateBeerUseCase.ExecuteAsync(new BeerUpdateDto(), idBeer));
            Assert.Equal($"No se encontró ninguna cerveza con ID {idBeer}", actualException.Message);

            _beerRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<BeerEntity>(), It.IsAny<int>()), Times.Once());
            _entityValidatorMock.Verify(v => v.ValidateAsync(It.IsAny<BeerEntity>()), Times.Once());
        }
    }
}

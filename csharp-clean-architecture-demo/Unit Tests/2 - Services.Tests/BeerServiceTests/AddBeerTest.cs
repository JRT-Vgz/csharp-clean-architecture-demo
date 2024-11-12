
using _1___Entities;
using _2___Services._Exceptions;
using _2___Services._Interfaces;
using _2___Services.Services.BeerService;
using _3___Mappers.Dtos.BeerDtos;
using AutoMapper;
using Moq;

namespace _2___Services.Tests.BeerServiceTests
{
    public class AddBeerTest
    {
        private readonly Mock<IRepository<BeerEntity>> _beerRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IEntityValidator<BeerEntity>> _entityValidatorMock;
        private readonly AddBeerUseCase<BeerInsertDto, BeerDto> _addBeerUseCase;

        public AddBeerTest()
        {
            _beerRepositoryMock = new Mock<IRepository<BeerEntity>>();
            _mapperMock = new Mock<IMapper>();
            _entityValidatorMock = new Mock<IEntityValidator<BeerEntity>>();
            _addBeerUseCase = new AddBeerUseCase<BeerInsertDto, BeerDto>(
                _beerRepositoryMock.Object,
                _mapperMock.Object,
                _entityValidatorMock.Object);
        }

        [Fact]
        public async Task ServiceValidatesAndReturnsDto_Correctly()
        {
            _mapperMock.Setup(m => m.Map<BeerEntity>(It.IsAny<BeerInsertDto>())).Returns(new BeerEntity());
            _entityValidatorMock.Setup(v => v.ValidateAsync(It.IsAny<BeerEntity>())).ReturnsAsync(true);
            _beerRepositoryMock.Setup(r => r.AddAsync(It.IsAny<BeerEntity>())).ReturnsAsync(new BeerEntity());
            _mapperMock.Setup(m => m.Map<BeerDto>(It.IsAny<BeerEntity>())).Returns(new BeerDto());

            var actual = await _addBeerUseCase.ExecuteAsync(new BeerInsertDto());

            Assert.NotNull(actual);
            Assert.IsType<BeerDto>(actual);

            _mapperMock.Verify(m => m.Map<BeerEntity>(It.IsAny<BeerInsertDto>()), Times.Once());
            _entityValidatorMock.Verify(v => v.ValidateAsync(It.IsAny<BeerEntity>()), Times.Once());
            _beerRepositoryMock.Verify(r => r.AddAsync(It.IsAny<BeerEntity>()), Times.Once());
            _mapperMock.Verify(m => m.Map<BeerDto>(It.IsAny<BeerEntity>()), Times.Once());
        }

        [Fact]
        public async Task IfEntityValidationIsFalse_ThrowException()
        {
            _mapperMock.Setup(m => m.Map<BeerEntity>(It.IsAny<BeerInsertDto>())).Returns(new BeerEntity());
            _entityValidatorMock.Setup(v => v.ValidateAsync(It.IsAny<BeerEntity>())).ReturnsAsync(false);

            var actualException = await Assert.ThrowsAsync<EntityValidationException>(
                () => _addBeerUseCase.ExecuteAsync(new BeerInsertDto()));

            _mapperMock.Verify(m => m.Map<BeerEntity>(It.IsAny<BeerInsertDto>()), Times.Once());
            _entityValidatorMock.Verify(v => v.ValidateAsync(It.IsAny<BeerEntity>()), Times.Once());
        }
    }
}

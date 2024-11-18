
using _1___Entities;
using _2___Services._Exceptions;
using _2___Services._Interfaces;
using _2___Services.Services.BeerService;
using _3___Presenters.ViewModels;
using Moq;

namespace _2___Services.Tests.BeerServiceTests
{
    public class GetBeerDetailByIdTest
    {
        private readonly Mock<IRepository<BeerEntity>> _beerRepositoryMock;
        private readonly Mock<IPresenter<BeerEntity, BeerDetailViewModel>> _presenterMock;
        private readonly GetBeerDetailByIdUseCase<BeerDetailViewModel> _getBeerDetailByIdUseCase;

        public GetBeerDetailByIdTest()
        {
            _beerRepositoryMock = new Mock<IRepository<BeerEntity>>();
            _presenterMock = new Mock<IPresenter<BeerEntity, BeerDetailViewModel>>();
            _getBeerDetailByIdUseCase = new GetBeerDetailByIdUseCase<BeerDetailViewModel>(
                _beerRepositoryMock.Object,
                _presenterMock.Object);
        }

        [Fact]
        public async Task ServiceReturnsViewModel_Correctly()
        {
            _beerRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new BeerEntity());
            _presenterMock.Setup(p => p.Present(It.IsAny<BeerEntity>())).Returns(new BeerDetailViewModel());

            var actual = await _getBeerDetailByIdUseCase.ExecuteAsync(It.IsAny<int>());

            Assert.NotNull(actual);
            Assert.IsType<BeerDetailViewModel>(actual);

            _beerRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Once());
            _presenterMock.Verify(p => p.Present(It.IsAny<BeerEntity>()), Times.Once());
        }

        [Fact]
        public async Task IfBeerNotFound_ThrowException()
        {
            int idBeer = 1;

            _beerRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((BeerEntity)null);

            var actualException = await Assert.ThrowsAsync<NotFoundException>(
                () => _getBeerDetailByIdUseCase.ExecuteAsync(idBeer));
            Assert.Equal($"No se encontró ninguna cerveza con ID {idBeer}", actualException.Message);

            _beerRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Once());
        }
    }
}

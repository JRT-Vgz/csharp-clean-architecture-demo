
using _1___Entities;
using _2___Services._Exceptions;
using _2___Services._Interfaces;
using _2___Services.Services.BrandService;
using _3___Mappers.Dtos.BrandDtos;
using AutoMapper;
using Moq;

namespace _2___Services.Tests.BrandServiceTests
{
    public class DeleteBrandTest
    {
        private readonly Mock<IRepository<BrandEntity>> _brandRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly DeleteBrandUseCase<BrandDto> _deleteBrandUseCase;

        public DeleteBrandTest()
        {
            _brandRepositoryMock = new Mock<IRepository<BrandEntity>>();
            _mapperMock = new Mock<IMapper>();
            _deleteBrandUseCase = new DeleteBrandUseCase<BrandDto>(_brandRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task ServiceReturnsDto_Correctly()
        {
            int idBrand = 1;

            var brandEntity = new BrandEntity { Id = idBrand, Name = "Brand" };
            var expectedDto = new BrandDto { Id = idBrand, Name = "Brand" };

            _brandRepositoryMock.Setup(r => r.DeleteAsync(idBrand)).ReturnsAsync(brandEntity);
            _mapperMock.Setup(m => m.Map<BrandDto>(brandEntity)).Returns(expectedDto);

            var actual = await _deleteBrandUseCase.ExecuteAsync(1);

            Assert.NotNull(actual);
            Assert.IsType<BrandDto>(actual);
            Assert.Equal(expectedDto.Id, actual.Id);
            Assert.Equal(expectedDto.Name, actual.Name);

            _brandRepositoryMock.Verify(r => r.DeleteAsync(1), Times.Once(), "El repositorio debería ejecutarse una vez.");
            _mapperMock.Verify(m => m.Map<BrandDto>(brandEntity), Times.Once(), "El mapper debería ejecutarse una vez.");
        }

        [Fact]
        public async Task IfBrandNotFound_ReturnException()
        {
            int idBrand = 100;

            _brandRepositoryMock.Setup(r => r.DeleteAsync(It.IsAny<int>())).ReturnsAsync((BrandEntity)null);

            var actualException = await Assert.ThrowsAsync<NotFoundException>(() => _deleteBrandUseCase.ExecuteAsync(idBrand));
            Assert.Equal($"No se encontró ninguna marca con ID {idBrand}", actualException.Message);

            _brandRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Once(), "El repositorio deberia ejecutarse una vez.");

            
        }
    }
}

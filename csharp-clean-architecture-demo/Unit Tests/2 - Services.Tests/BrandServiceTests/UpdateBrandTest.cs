
using _1___Entities;
using _2___Services._Exceptions;
using _2___Services._Interfaces;
using _2___Services.Services.BrandService;
using _3___Mappers.Dtos.BrandDtos;
using AutoMapper;
using Moq;

namespace _2___Services.Tests.BrandServiceTests
{
    public class UpdateBrandTest
    {
        private readonly Mock<IRepository<BrandEntity>> _brandRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IEntityValidator<BrandEntity>> _entityValidatorMock;
        private readonly UpdateBrandUseCase<BrandUpdateDto, BrandDto> _updateBrandUseCase;

        public UpdateBrandTest()
        {
            _brandRepositoryMock = new Mock<IRepository<BrandEntity>>();
            _mapperMock = new Mock<IMapper>();
            _entityValidatorMock = new Mock<IEntityValidator<BrandEntity>>();
            _updateBrandUseCase = new UpdateBrandUseCase<BrandUpdateDto, BrandDto>(
                _brandRepositoryMock.Object,
                _mapperMock.Object,
                _entityValidatorMock.Object);
        }

        [Fact]
        public async Task ServiceValidatesBrandAndReturnsDto_Correctly()
        {
            int idBrand = 1;
            string brandName = "Brand";

            var brandUpdateDto = new BrandUpdateDto { Id = idBrand, Name = brandName };
            var brandEntity = new BrandEntity { Id = idBrand, Name = brandName };
            var expectedDto = new BrandDto { Id = idBrand, Name = brandName };

            _mapperMock.Setup(m => m.Map<BrandEntity>(It.IsAny<BrandUpdateDto>())).Returns(brandEntity);
            _entityValidatorMock.Setup(v => v.ValidateAsync(It.IsAny<BrandEntity>())).ReturnsAsync(true);
            _brandRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<BrandEntity>(), idBrand)).ReturnsAsync(brandEntity);
            _mapperMock.Setup(m => m.Map<BrandDto>(It.IsAny<BrandEntity>())).Returns(expectedDto);

            var actual = await _updateBrandUseCase.ExecuteAsync(brandUpdateDto, idBrand);

            Assert.NotNull(actual);
            Assert.IsType<BrandDto>(actual);
            Assert.Equal(expectedDto.Id, actual.Id);
            Assert.Equal(expectedDto.Name, actual.Name);

            _mapperMock.Verify(m => m.Map<BrandEntity>(It.IsAny<BrandUpdateDto>()), Times.Once());
            _entityValidatorMock.Verify(v => v.ValidateAsync(It.IsAny<BrandEntity>()), Times.Once());
            _brandRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<BrandEntity>(), idBrand), Times.Once());
            _mapperMock.Verify(m => m.Map<BrandDto>(It.IsAny<BrandEntity>()), Times.Once());
        }

        [Fact]
        public async Task IfEntityValidationFalse_ThrowsException()
        {
            int idBrand = 1;
            string brandName = "Brand";

            var brandUpdateDto = new BrandUpdateDto { Id = idBrand, Name = brandName };

            _mapperMock.Setup(m => m.Map<BrandEntity>(It.IsAny<BrandUpdateDto>())).Returns(new BrandEntity());
            _entityValidatorMock.Setup(v => v.ValidateAsync(It.IsAny<BrandEntity>())).ReturnsAsync(false);

            var actualException = await Assert.ThrowsAsync<EntityValidationException>(
                () => _updateBrandUseCase.ExecuteAsync(brandUpdateDto, idBrand));

            _mapperMock.Verify(m => m.Map<BrandEntity>(It.IsAny<BrandUpdateDto>()), Times.Once());
            _entityValidatorMock.Verify(v => v.ValidateAsync(It.IsAny<BrandEntity>()), Times.Once());
            _brandRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<BrandEntity>(), idBrand), Times.Never());
        }

        [Fact]
        public async Task IfBrandNotFound_ThrowsException()
        {
            int idBrand = 1;
            string brandName = "Brand";

            var brandUpdateDto = new BrandUpdateDto { Id = idBrand, Name = brandName };

            _mapperMock.Setup(m => m.Map<BrandEntity>(It.IsAny<BrandUpdateDto>())).Returns(new BrandEntity());
            _entityValidatorMock.Setup(v => v.ValidateAsync(It.IsAny<BrandEntity>())).ReturnsAsync(true);
            _brandRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<BrandEntity>(), idBrand)).ReturnsAsync((BrandEntity)null);

            var actualException = await Assert.ThrowsAsync<NotFoundException>(
                () => _updateBrandUseCase.ExecuteAsync(brandUpdateDto, idBrand));
            Assert.Equal($"No se encontró ninguna marca con ID {idBrand}", actualException.Message);

            _mapperMock.Verify(m => m.Map<BrandEntity>(It.IsAny<BrandUpdateDto>()), Times.Once());
            _entityValidatorMock.Verify(v => v.ValidateAsync(It.IsAny<BrandEntity>()), Times.Once());
            _brandRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<BrandEntity>(), idBrand), Times.Once());
        }
    }
}

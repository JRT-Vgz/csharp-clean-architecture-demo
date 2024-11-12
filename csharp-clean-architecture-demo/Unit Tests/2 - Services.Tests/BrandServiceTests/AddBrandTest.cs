
using _1___Entities;
using _2___Services._Exceptions;
using _2___Services._Interfaces;
using _2___Services.Services.BrandService;
using _3___Mappers.Dtos.BrandDtos;
using AutoMapper;
using Moq;

namespace _2___Services.Tests.BrandServiceTests
{
    public class AddBrandTest
    {
        private readonly Mock<IRepository<BrandEntity>> _brandRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IEntityValidator<BrandEntity>> _entityValidatorMock;
        private readonly AddBrandUseCase<BrandInsertDto, BrandDto> _addBrandUseCase;

        public AddBrandTest()
        {
            _brandRepositoryMock = new Mock<IRepository<BrandEntity>>();
            _mapperMock = new Mock<IMapper>();
            _entityValidatorMock = new Mock<IEntityValidator<BrandEntity>>();
            _addBrandUseCase = new AddBrandUseCase<BrandInsertDto, BrandDto>(
                _brandRepositoryMock.Object, 
                _mapperMock.Object,
                _entityValidatorMock.Object);
        }

        [Fact]
        public async Task ServiceValidatesBrandAndReturnsDto_Correctly()
        {
            int idBrand = 1;
            string brandName = "Brand";

            var brandInsertDto = new BrandInsertDto() { Name = brandName };
            var brandEntity = new BrandEntity() { Id = idBrand, Name = brandName };
            var expectedDto = new BrandDto() { Id = idBrand, Name = brandName };

            _mapperMock.Setup(m => m.Map<BrandEntity>(It.IsAny<BrandInsertDto>())).Returns(brandEntity);

            _entityValidatorMock.Setup(v => v.ValidateAsync(It.IsAny <BrandEntity>())).ReturnsAsync(true);

            _brandRepositoryMock.Setup(r => r.AddAsync(It.IsAny<BrandEntity>())).ReturnsAsync(brandEntity);

            _mapperMock.Setup(m => m.Map<BrandDto>(It.IsAny<BrandEntity>())).Returns(expectedDto);


            var actual = await _addBrandUseCase.ExecuteAsync(brandInsertDto);

            Assert.NotNull(actual);
            Assert.IsType<BrandDto>(actual);
            Assert.Equal(expectedDto.Id, actual.Id);
            Assert.Equal(expectedDto.Name, actual.Name);

            _mapperMock.Verify(m => m.Map<BrandEntity>(It.IsAny<BrandInsertDto>()), Times.Once());
            _entityValidatorMock.Verify(v => v.ValidateAsync(It.IsAny<BrandEntity>()), Times.Once());
            _brandRepositoryMock.Verify(r => r.AddAsync(It.IsAny<BrandEntity>()), Times.Once());
            _mapperMock.Verify(m => m.Map<BrandDto>(It.IsAny<BrandEntity>()), Times.Once());
        }

        [Fact]
        public async Task IfEntityValidationFalse_ThrowsException()
        {
            string brandName = "Brand";

            var brandInsertDto = new BrandInsertDto() { Name = brandName };
            var brandEntity = new BrandEntity() { Name = brandName };

            _mapperMock.Setup(m => m.Map<BrandEntity>(It.IsAny<BrandInsertDto>())).Returns(brandEntity);
            _entityValidatorMock.Setup(v => v.ValidateAsync(It.IsAny<BrandEntity>())).ReturnsAsync(false);

            var actualException = await Assert.ThrowsAsync<EntityValidationException>(
                () => _addBrandUseCase.ExecuteAsync(brandInsertDto));

            _mapperMock.Verify(m => m.Map<BrandEntity>(It.IsAny<BrandInsertDto>()), Times.Once());
            _entityValidatorMock.Verify(v => v.ValidateAsync(It.IsAny<BrandEntity>()), Times.Once());
            _brandRepositoryMock.Verify(r => r.AddAsync(It.IsAny<BrandEntity>()), Times.Never());
        }
    }
}


using _1___Entities;
using _3___Data;
using _3___Data.Models;
using _3___Validators.EntityValidators;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace _3___Validators.Tests.EntityValidator.Tests
{
    public class BrandEntityValidatorTest
    {
        private readonly Mock<BreweryContext> _contextMock;
        private readonly BrandEntityValidator _brandEntityValidator;

        public BrandEntityValidatorTest()
        {
            _contextMock = new Mock<BreweryContext>();
            _contextMock.Setup<DbSet<BrandModel>>(c => c.Brands)
                .ReturnsDbSet(CreateTestBrandModelList());

            _brandEntityValidator = new BrandEntityValidator(_contextMock.Object);
        }

        private static List<BrandModel> CreateTestBrandModelList() 
            =>  new List<BrandModel>()
                {
                    new BrandModel { Id = 1, Name = "Brand 1" },
                    new BrandModel { Id = 2, Name = "Brand 2", }
                };

        [Fact]
        public async Task ReturnTrue_WhenBrandNameDoesNotExist()
        {
            var brandEntity = new BrandEntity { Id = 3, Name = "Brand 3" };

            var actual = await _brandEntityValidator.ValidateAsync(brandEntity);

            Assert.True(actual);
            Assert.Empty(_brandEntityValidator.Errors);
        }

        [Fact]
        public async Task ReturnTrue_WhenBrandNameExistInDB_AndEntityIdMatchesModelId()
        {
            var brandEntity = new BrandEntity { Id = 2, Name = "Brand 2" };

            var actual = await _brandEntityValidator.ValidateAsync(brandEntity);

            Assert.True(actual);
            Assert.Empty(_brandEntityValidator.Errors);
        }

        [Fact]
        public async Task ReturnFalse_WhenBrandNameExistInDB_AndEntityIdDoesntMatchModelId()
        {
            var brandEntity = new BrandEntity { Id = 4, Name = "Brand 2" };

            var actual = await _brandEntityValidator.ValidateAsync(brandEntity);

            Assert.False(actual);
            Assert.True(_brandEntityValidator.Errors.Count == 1);
            Assert.Equal("Ya existe una marca con ese nombre.", _brandEntityValidator.Errors[0]);
        }
    }
}
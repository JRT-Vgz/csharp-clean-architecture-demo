
using _1___Entities;
using _3___Data;
using _3___Data.Models;
using _3___Validators.EntityValidators;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace _3___Validators.Tests.EntityValidator.Tests
{
    public class BeerEntityValidatorTest
    {
        private readonly Mock<BreweryContext> _contextMock;
        private readonly BeerEntityValidator _beerEntityValidator;

        public BeerEntityValidatorTest()
        {
            _contextMock = new Mock<BreweryContext>();
            _contextMock.Setup<DbSet<BeerModel>>(c => c.Beers).ReturnsDbSet(CreateTestBeerModelList());
            _contextMock.Setup<DbSet<BrandModel>>(c => c.Brands).ReturnsDbSet(CreateTestBrandModelList());

            _beerEntityValidator = new BeerEntityValidator(_contextMock.Object);
        }

        private static List<BeerModel> CreateTestBeerModelList()
            => new List<BeerModel>
            {
                new BeerModel{ Id = 1, Name = "Beer 1" },
                new BeerModel{ Id = 2, Name = "Beer 2" }
            };

        private static List<BrandModel> CreateTestBrandModelList()
            => new List<BrandModel>()
                {
                    new BrandModel { Id = 1, Name = "Brand 1" },
                    new BrandModel { Id = 2, Name = "Brand 2", }
                };


        [Theory]
        //BeerModel doesnt exist, Brand exists. Validation result = true. Errors = 0.
        [InlineData(3, "Beer 3", 1, true, 0, null)]        

        //BeerModel doesnt exist, Brand doesnt exist. Validation result = false. Errors = 1.
        [InlineData(3, "Beer 3", 3, false, 1, new string[] { "No existe ninguna marca con 'IdBrand' 3." })]

        //BeerModel exists, EntityId == Model Id. Brand doesnt exist. Validation result = false. Erros = 1.
        [InlineData(2, "Beer 2", 3, false, 1, new string[] { "No existe ninguna marca con 'IdBrand' 3." })]

        //BeerModel exists, EntityId != Model Id. Brand exists. Validation result = false. Erros = 1.
        [InlineData(1, "Beer 2", 2, false, 1, new string[] { "Ya existe una cerveza con ese nombre." })]

        //BeerModel exists, EntityId != Model Id. Brand doesnt exist. Validation result = false. Erros = 2.
        [InlineData(1, "Beer 2", 3, false, 2, new string[]
                                              { "Ya existe una cerveza con ese nombre.",
                                                "No existe ninguna marca con 'IdBrand' 3."})]

        public async Task ReturnExpectedBoolAndErrorList_AfterValidateBeerEntity
            (int idBeer, string beerName, int idBrand, bool expectedValidation, 
            int expectedErrorsCount, string[] expectedErrorMessages)
        {
            var beerEntity = new BeerEntity { Id = idBeer, Name = beerName, IdBrand = idBrand };

            var actual = await _beerEntityValidator.ValidateAsync(beerEntity);

            Assert.Equal(expectedValidation, actual);
            Assert.Equal(_beerEntityValidator.Errors.Count, expectedErrorsCount);
            if (expectedErrorMessages != null)
            {
                for (int i = 0; i < expectedErrorMessages.Length; i++)
                {
                    Assert.Equal(expectedErrorMessages[i], _beerEntityValidator.Errors[i]);
                }
            }
        }
    }
}

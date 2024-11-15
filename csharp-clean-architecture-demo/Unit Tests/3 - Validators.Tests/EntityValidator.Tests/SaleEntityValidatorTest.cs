
using _1___Entities;
using _3___Data;
using _3___Data.Models;
using _3___Validators.EntityValidators;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace _3___Validators.Tests.EntityValidator.Tests
{
    public class SaleEntityValidatorTest
    {
        private readonly Mock<BreweryContext> _contextMock;
        private readonly SaleEntityValidator _saleEntityValidator;

        public SaleEntityValidatorTest()
        {
            _contextMock = new Mock<BreweryContext>();
            _contextMock.Setup<DbSet<BeerModel>>(c => c.Beers).ReturnsDbSet(CreateTestBeerModelList());
            _saleEntityValidator = new SaleEntityValidator(_contextMock.Object);
        }

        private static List<BeerModel> CreateTestBeerModelList()
            => new List<BeerModel>
            {
                new BeerModel{ Id = 1, Name = "Beer 1" },
                new BeerModel{ Id = 2, Name = "Beer 2" }
            };

        [Fact]
        public async Task ReturnFalse_WhenSaleHasNoConcepts_AndHasNoTotal()
        {
            var concepts = new List<ConceptEntity>();
            var saleEntity = new SaleEntity(DateTime.Now, concepts);
            bool expected = false;
            int expectedErrorsCount = 2;
            string[] expectedErrorMessages = { "Una venta debe tener conceptos.",
                                               "Una venta debe tener más de 0.00€ en Total."};

            var actual = await _saleEntityValidator.ValidateAsync(saleEntity);

            Assert.Equal(expected, actual);
            Assert.Equal(expectedErrorsCount, _saleEntityValidator.Errors.Count);
            if (expectedErrorMessages != null)
            {
                for (int i = 0; i < expectedErrorMessages.Length; i++)
                {
                    Assert.Equal(expectedErrorMessages[i], _saleEntityValidator.Errors[i]);
                }
            }
        }

        [Fact]
        public async Task ReturnFalse_WhenSaleHasNoTotal()
        {
            var concepts = new List<ConceptEntity>()
            {
                new ConceptEntity(1,1,1),
                new ConceptEntity(2,2,2)
            };
            var saleEntity = new SaleEntity(DateTime.Now, concepts);
            saleEntity.Total = 0;
            bool expected = false;
            int expectedErrorsCount = 1;
            string[] expectedErrorMessages = { "Una venta debe tener más de 0.00€ en Total." };

            var actual = await _saleEntityValidator.ValidateAsync(saleEntity);

            Assert.Equal(expected, actual);
            Assert.Equal(expectedErrorsCount, _saleEntityValidator.Errors.Count);
            if (expectedErrorMessages != null)
            {
                for (int i = 0; i < expectedErrorMessages.Length; i++)
                {
                    Assert.Equal(expectedErrorMessages[i], _saleEntityValidator.Errors[i]);
                }
            }
        }

        [Theory]
        // First Beer doesnt exist. Validation result = false. Errors = 1.
        [InlineData(50, 1, false, 1, new string[] { "No existe ninguna cerveza con 'Id' 50" })]

        // Both Beers dont exist. Validation result = false. Errors = 2.
        [InlineData(50, 100, false, 2, new string[] { "No existe ninguna cerveza con 'Id' 50",
                                                      "No existe ninguna cerveza con 'Id' 100"})]

        // Both Beers exist. Validation result = true. Errors = 0.
        [InlineData(1, 2, true, 0, null)]

        public async Task ReturnExpectedBoolAndErrorList_AfterValidateSaleEntity_WithConceptsAndSaleTotal(
            int idBeer1, int idBeer2, bool expected, int expectedErrorsCount, string[] expectedErrorMessages)
        {
            var concepts = new List<ConceptEntity>()
            {
                new ConceptEntity(idBeer1,1,1),
                new ConceptEntity(idBeer2,2,2)
            };
            var saleEntity = new SaleEntity(DateTime.Now, concepts);

            var actual = await _saleEntityValidator.ValidateAsync(saleEntity);

            Assert.Equal(expected, actual);
            Assert.Equal(expectedErrorsCount, _saleEntityValidator.Errors.Count);
            if (expectedErrorMessages != null)
            {
                for (int i = 0; i < expectedErrorMessages.Length; i++)
                {
                    Assert.Equal(expectedErrorMessages[i], _saleEntityValidator.Errors[i]);
                }
            }
        }
    }
}

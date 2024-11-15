
using _1___Entities;
using _3___Data;
using _3___Data.Models;
using _3___Validators.EntityValidators;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace _3___Validators.Tests.EntityValidator.Tests
{
    public class ConceptEntityValidatorTest
    {
        private readonly Mock<BreweryContext> _contextMock;
        private readonly ConceptEntityValidator _conceptEntityValidator;

        public ConceptEntityValidatorTest()
        {
            _contextMock = new Mock<BreweryContext>();
            _contextMock.Setup<DbSet<SaleModel>>(c => c.Sales).ReturnsDbSet(CreateTestSaleModelList());
            _contextMock.Setup<DbSet<BeerModel>>(c => c.Beers).ReturnsDbSet(CreateTestBeerModelList());

            _conceptEntityValidator = new ConceptEntityValidator(_contextMock.Object);
        }

        private static List<SaleModel> CreateTestSaleModelList()
            => new List<SaleModel>()
            {
                new SaleModel { Id = 1 },
                new SaleModel { Id = 2 }
            };


        private static List<BeerModel> CreateTestBeerModelList()
            => new List<BeerModel>
            {
                new BeerModel{ Id = 1, Name = "Beer 1" },
                new BeerModel{ Id = 2, Name = "Beer 2" }
            };

        [Fact]
        public async Task ReturnTrueAnd0Errors_WhenIdSaleIs0_AndBeerIdExists()
        {
            int idSale = 0;
            int idBeer = 1;
            var conceptEntity = new ConceptEntity(idBeer, 1, 1) { IdSale = idSale };
            bool expected = true;
            int expectedErrorsCount = 0;

            var actual = await _conceptEntityValidator.ValidateAsync(conceptEntity);

            Assert.Equal(expected, actual);
            Assert.Equal(expectedErrorsCount, _conceptEntityValidator.Errors.Count);
        }

        [Fact]
        public async Task ReturnFalseAnd1Error_WhenIdSaleIs0_AndBeerIdDoesntExist()
        {
            int idSale = 0;
            int idBeer = 100;
            var conceptEntity = new ConceptEntity(idBeer, 1, 1) { IdSale = idSale };
            bool expected = false;
            int expectedErrorsCount = 1;
            string[] expectedErrorMessages = { $"No existe ninguna cerveza con 'Id' {idBeer}" };

            var actual = await _conceptEntityValidator.ValidateAsync(conceptEntity);

            Assert.Equal(expected, actual);
            Assert.Equal(expectedErrorsCount, _conceptEntityValidator.Errors.Count);
            if (expectedErrorMessages != null)
            {
                for (int i = 0; i < expectedErrorMessages.Length; i++)
                {
                    Assert.Equal(expectedErrorMessages[i], _conceptEntityValidator.Errors[i]);
                }
            }
        }

        [Fact]
        public async Task ReturnTrueAnd0Errors_WhenIdSaleExists_AndBeerIdExists()
        {
            int idSale = 1;
            int idBeer = 1;
            var conceptEntity = new ConceptEntity(idBeer, 1, 1) { IdSale = idSale };
            bool expected = true;
            int expectedErrorsCount = 0;
            string[] expectedErrorMessages = null;

            var actual = await _conceptEntityValidator.ValidateAsync(conceptEntity);

            Assert.Equal(expected, actual);
            Assert.Equal(expectedErrorsCount, _conceptEntityValidator.Errors.Count);
            if (expectedErrorMessages != null)
            {
                for (int i = 0; i < expectedErrorMessages.Length; i++)
                {
                    Assert.Equal(expectedErrorMessages[i], _conceptEntityValidator.Errors[i]);
                }
            }
        }

        [Fact]
        public async Task ReturnFalseAnd1Error_WhenIdSaleExists_AndBeerIdDoesntExist()
        {
            int idSale = 1;
            int idBeer = 100;
            var conceptEntity = new ConceptEntity(idBeer, 1, 1) { IdSale = idSale };
            bool expected = false;
            int expectedErrorsCount = 1;
            string[] expectedErrorMessages = { $"No existe ninguna cerveza con 'Id' {idBeer}" };

            var actual = await _conceptEntityValidator.ValidateAsync(conceptEntity);

            Assert.Equal(expected, actual);
            Assert.Equal(expectedErrorsCount, _conceptEntityValidator.Errors.Count);
            if (expectedErrorMessages != null)
            {
                for (int i = 0; i < expectedErrorMessages.Length; i++)
                {
                    Assert.Equal(expectedErrorMessages[i], _conceptEntityValidator.Errors[i]);
                }
            }
        }

        [Fact]
        public async Task ReturnFalseAnd1Error_WhenIdSaleDoesntExist_AndBeerIdExists()
        {
            int idSale = 100;
            int idBeer = 1;
            var conceptEntity = new ConceptEntity(idBeer, 1, 1) { IdSale = idSale };
            bool expected = false;
            int expectedErrorsCount = 1;
            string[] expectedErrorMessages = { $"No existe ninguna venta con 'Id' {idSale}" };

            var actual = await _conceptEntityValidator.ValidateAsync(conceptEntity);

            Assert.Equal(expected, actual);
            Assert.Equal(expectedErrorsCount, _conceptEntityValidator.Errors.Count);
            if (expectedErrorMessages != null)
            {
                for (int i = 0; i < expectedErrorMessages.Length; i++)
                {
                    Assert.Equal(expectedErrorMessages[i], _conceptEntityValidator.Errors[i]);
                }
            }
        }

        [Fact]
        public async Task ReturnFalseAnd2Errors_WhenIdSaleDoesntExist_AndBeerIdDoesntExist()
        {
            int idSale = 100;
            int idBeer = 100;
            var conceptEntity = new ConceptEntity(idBeer, 1, 1) { IdSale = idSale };
            bool expected = false;
            int expectedErrorsCount = 2;
            string[] expectedErrorMessages = { $"No existe ninguna venta con 'Id' {idSale}",
                                               $"No existe ninguna cerveza con 'Id' {idBeer}"};

            var actual = await _conceptEntityValidator.ValidateAsync(conceptEntity);

            Assert.Equal(expected, actual);
            Assert.Equal(expectedErrorsCount, _conceptEntityValidator.Errors.Count);
            if (expectedErrorMessages != null)
            {
                for (int i = 0; i < expectedErrorMessages.Length; i++)
                {
                    Assert.Equal(expectedErrorMessages[i], _conceptEntityValidator.Errors[i]);
                }
            }
        }
    }
}

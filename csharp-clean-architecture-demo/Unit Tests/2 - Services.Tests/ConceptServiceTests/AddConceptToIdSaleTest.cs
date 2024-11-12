
using _1___Entities;
using _2___Services._Exceptions;
using _2___Services._Interfaces;
using _2___Services.Services.ConceptService;
using _3___Mappers.Dtos.SaleDtos;
using AutoMapper;
using Moq;
using Org.BouncyCastle.Crypto;

namespace _2___Services.Tests.ConceptServiceTests
{
    public class AddConceptToIdSaleTest
    {
        private readonly Mock<IRepository<ConceptEntity>> _conceptRepositoryMock;
        private readonly Mock<IRepository<SaleEntity>> _saleRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IEntityValidator<ConceptEntity>> _entityValidatorMock;
        private readonly AddConceptToIdSaleUseCase<ConceptInsertToIdSaleDto, SaleDto> _addConceptToIdSaleUseCase;

        public AddConceptToIdSaleTest() 
        {
            _conceptRepositoryMock = new Mock<IRepository<ConceptEntity>>();
            _saleRepositoryMock = new Mock<IRepository<SaleEntity>>();
            _mapperMock = new Mock<IMapper>();
            _entityValidatorMock = new Mock<IEntityValidator<ConceptEntity>>();
            _addConceptToIdSaleUseCase = new AddConceptToIdSaleUseCase<ConceptInsertToIdSaleDto, SaleDto>(
                _conceptRepositoryMock.Object, _saleRepositoryMock.Object, _mapperMock.Object, _entityValidatorMock.Object);
        }

        private ConceptEntity CreateTestConceptEntity()
        {
            int id = 1;
            int idBeer = 1;
            int quantity = 1;
            decimal unitPrice = 10;

            return new ConceptEntity(id, idBeer, quantity, unitPrice) { IdSale = 1 };
        }

        private SaleEntity CreateTestSaleEntity()
            => new SaleEntity(DateTime.Now, new List<ConceptEntity>());


        [Theory]
        [InlineData(1, 0)]  // El primer número es el IdSale que viene en el Dto, el segundo número el que viene en el parámetro.
        [InlineData(0, 1)]
        public async Task ThrowException_IfIdSaleEquals0(int idSaleDto, int idSaleParameter)
        {
            var conceptEntity = CreateTestConceptEntity();
            conceptEntity.IdSale = idSaleDto;

            _mapperMock.Setup(m => m.Map<ConceptEntity>(It.IsAny<ConceptInsertToIdSaleDto>())).Returns(conceptEntity);

            var actualException = await Assert.ThrowsAsync<NotFoundException>(
                () => _addConceptToIdSaleUseCase.ExecuteAsync(new ConceptInsertToIdSaleDto(), idSaleParameter));
            Assert.Equal("No se encontró ninguna venta con ID 0", actualException.Message);

            _mapperMock.Verify(m => m.Map<ConceptEntity>(It.IsAny<ConceptInsertToIdSaleDto>()), Times.Once());
            _entityValidatorMock.Verify(v => v.ValidateAsync(conceptEntity), Times.Never());
        }

        [Fact]
        public async Task ThrowException_IfEntityValidationIsFalse()
        {
            var conceptEntity = CreateTestConceptEntity();

            _mapperMock.Setup(m => m.Map<ConceptEntity>(It.IsAny<ConceptInsertToIdSaleDto>())).Returns(conceptEntity);
            _entityValidatorMock.Setup(v => v.ValidateAsync(conceptEntity)).ReturnsAsync(false);

            var actualException = await Assert.ThrowsAsync<EntityValidationException>(
                () => _addConceptToIdSaleUseCase.ExecuteAsync(new ConceptInsertToIdSaleDto(), 1));

            _mapperMock.Verify(m => m.Map<ConceptEntity>(It.IsAny<ConceptInsertToIdSaleDto>()), Times.Once());
            _entityValidatorMock.Verify(v => v.ValidateAsync(conceptEntity), Times.Once());
            _conceptRepositoryMock.Verify(r => r.AddAsync(It.IsAny<ConceptEntity>()), Times.Never());
        }

        [Fact]
        public async Task ServiceValidatesAndReturnsDto_Correctly()
        {
            var conceptEntity = CreateTestConceptEntity();
            var saleEntity = CreateTestSaleEntity();

            _mapperMock.Setup(m => m.Map<ConceptEntity>(It.IsAny<ConceptInsertToIdSaleDto>())).Returns(conceptEntity);
            _entityValidatorMock.Setup(v => v.ValidateAsync(conceptEntity)).ReturnsAsync(true);
            _conceptRepositoryMock.Setup(r => r.AddAsync(conceptEntity)).ReturnsAsync(conceptEntity);
            _saleRepositoryMock.Setup(r => r.GetByIdAsync(conceptEntity.IdSale)).ReturnsAsync(saleEntity);
            _saleRepositoryMock.Setup(r => r.UpdateAsync(saleEntity, conceptEntity.IdSale)).ReturnsAsync(saleEntity);
            _mapperMock.Setup(m => m.Map<SaleDto>(saleEntity)).Returns(new SaleDto());

            var actual = await _addConceptToIdSaleUseCase.ExecuteAsync(new ConceptInsertToIdSaleDto(), 1);

            Assert.NotNull(actual);
            Assert.IsType<SaleDto>(actual);

            _mapperMock.Verify(m => m.Map<ConceptEntity>(It.IsAny<ConceptInsertToIdSaleDto>()), Times.Once());
            _entityValidatorMock.Verify(v => v.ValidateAsync(conceptEntity), Times.Once());
            _conceptRepositoryMock.Verify(r => r.AddAsync(conceptEntity), Times.Once());
            _saleRepositoryMock.Verify(r => r.GetByIdAsync(conceptEntity.IdSale), Times.Once());
            _saleRepositoryMock.Verify(r => r.UpdateAsync(saleEntity, conceptEntity.IdSale), Times.Once());
            _mapperMock.Verify(m => m.Map<SaleDto>(saleEntity), Times.Once());
        }
    }
}

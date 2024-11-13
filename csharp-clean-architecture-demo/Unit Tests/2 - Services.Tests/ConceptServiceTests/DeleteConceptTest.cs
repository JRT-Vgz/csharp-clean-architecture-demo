
using _1___Entities;
using _2___Services._Exceptions;
using _2___Services._Interfaces;
using _2___Services.Services.ConceptService;
using _3___Mappers.Dtos.SaleDtos;
using AutoMapper;
using Moq;

namespace _2___Services.Tests.ConceptServiceTests
{
    public class DeleteConceptTest
    {
        private readonly Mock<IRepository<ConceptEntity>> _conceptRepositoryMock;
        private readonly Mock<IRepository<SaleEntity>> _saleRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly DeleteConceptUseCase<ConceptDto> _deleteConceptUseCase;

        public DeleteConceptTest()
        {
            _conceptRepositoryMock = new Mock<IRepository<ConceptEntity>>();
            _saleRepositoryMock = new Mock<IRepository<SaleEntity>>();
            _mapperMock = new Mock<IMapper>();
            _deleteConceptUseCase = new DeleteConceptUseCase<ConceptDto>(
            _conceptRepositoryMock.Object, _saleRepositoryMock.Object, _mapperMock.Object);
        }

        private ConceptEntity CreateTestConceptEntity()
        {
            int id = 1;
            int idBeer = 1;
            int quantity = 1;
            decimal unitPrice = 10;

            return new ConceptEntity(id, idBeer, quantity, unitPrice) { IdSale = 1 };
        }

        [Fact]
        public async Task ThrowException_IfConceptNotFound()
        {
            int idConcept = 1;

            _conceptRepositoryMock.Setup(r => r.DeleteAsync(It.IsAny<int>())).ReturnsAsync((ConceptEntity)null);

            var actualException = await Assert.ThrowsAsync<NotFoundException>(
                () => _deleteConceptUseCase.ExecuteAsync(idConcept));
            Assert.Equal($"No se encontró ningún concepto con ID {idConcept}", actualException.Message);

            _conceptRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Once());
            _saleRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never());
        }

        [Fact]
        public async Task DeleteSaleWhenConceptsEmpty_ReturnDtoCorrectly()
        {
            var conceptEntity = CreateTestConceptEntity();
            var saleEntity = new SaleEntity(DateTime.Now, new List<ConceptEntity>());

            _conceptRepositoryMock.Setup(r => r.DeleteAsync(It.IsAny<int>())).ReturnsAsync(conceptEntity);
            _saleRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(saleEntity);
            _saleRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<SaleEntity>(), It.IsAny<int>())).ReturnsAsync(saleEntity);
            _saleRepositoryMock.Setup(r => r.DeleteAsync(It.IsAny<int>())).ReturnsAsync((SaleEntity)null);
            _mapperMock.Setup(m => m.Map<ConceptDto>(It.IsAny<ConceptEntity>())).Returns(new ConceptDto());

            var actual = await _deleteConceptUseCase.ExecuteAsync(It.IsAny<int>());

            Assert.NotNull(actual);
            Assert.IsType<ConceptDto>(actual);

            _conceptRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Once());
            _saleRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Once());
            _saleRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<SaleEntity>(), It.IsAny<int>()), Times.Once());           
            _mapperMock.Verify(m => m.Map<ConceptDto>(It.IsAny<ConceptEntity>()), Times.Once());

            _saleRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async Task NotDeleteSaleWhenConceptsNotEmpty_ReturnDtoCorrectly()
        {
            var conceptEntity = CreateTestConceptEntity();
            var saleEntity = new SaleEntity(DateTime.Now, new List<ConceptEntity>());
            saleEntity.Concepts.Add(conceptEntity);

            _conceptRepositoryMock.Setup(r => r.DeleteAsync(It.IsAny<int>())).ReturnsAsync(conceptEntity);
            _saleRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(saleEntity);
            _saleRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<SaleEntity>(), It.IsAny<int>())).ReturnsAsync(saleEntity);
            _saleRepositoryMock.Setup(r => r.DeleteAsync(It.IsAny<int>())).ReturnsAsync((SaleEntity)null);
            _mapperMock.Setup(m => m.Map<ConceptDto>(It.IsAny<ConceptEntity>())).Returns(new ConceptDto());

            var actual = await _deleteConceptUseCase.ExecuteAsync(It.IsAny<int>());

            Assert.NotNull(actual);
            Assert.IsType<ConceptDto>(actual);

            _conceptRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Once());
            _saleRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Once());
            _saleRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<SaleEntity>(), It.IsAny<int>()), Times.Once());
            _mapperMock.Verify(m => m.Map<ConceptDto>(It.IsAny<ConceptEntity>()), Times.Once());

            _saleRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never());
        }
    }
}

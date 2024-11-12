
using _1___Entities;

namespace Entities.Tests
{
    public class ConceptEntityTest
    {
        private readonly int _id = 1;
        private readonly int _idBeer = 1;
        private readonly int _quantity = 10;
        private readonly decimal _unitPrice = 5.0m;

        [Theory]
        [InlineData(10, 5, 50)]
        [InlineData(8, 3, 24)]
        [InlineData(1, 100000, 100000)]
        [InlineData(1000000, 0.1, 100000)]
        public void CalculatesConceptTotal_Correctly(int quantity, decimal unitPrice, decimal expected)
        {
            var conceptEntity = new ConceptEntity(_idBeer, quantity, unitPrice);

            decimal actual = conceptEntity.ConceptPrice;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FirstConstructorInitializesCorrectly()
        {
            var conceptEntity = new ConceptEntity(_idBeer, _quantity, _unitPrice);

            Assert.Equal(_idBeer, conceptEntity.IdBeer);
            Assert.Equal(_quantity, conceptEntity.Quantity);
            Assert.Equal(_unitPrice, conceptEntity.UnitPrice);
            Assert.Equal(_quantity * _unitPrice, conceptEntity.ConceptPrice);
        }

        [Fact]
        public void SecondConstructorInitializesCorrectly()
        {
            var conceptEntity = new ConceptEntity(_id, _idBeer, _quantity, _unitPrice);

            Assert.Equal(_id, conceptEntity.Id);
            Assert.Equal(_idBeer, conceptEntity.IdBeer);
            Assert.Equal(_quantity, conceptEntity.Quantity);
            Assert.Equal(_unitPrice, conceptEntity.UnitPrice);
            Assert.Equal(_quantity * _unitPrice, conceptEntity.ConceptPrice);
        }

        [Theory]
        [InlineData(-10, 5, "La cantidad debe ser superior a 0.")]
        [InlineData(0, 5, "La cantidad debe ser superior a 0.")]
        [InlineData(10, -5, "El precio unitario debe ser superior a 0.")]
        [InlineData(10, 0, "El precio unitario debe ser superior a 0.")]
        public void FirstConstructorThrowsExceptionWhenNegativeOrZeroQuantityOrUnitPrice(int quantity, decimal unitPrice, string expectedMessage)
        {
            var actualException = Assert.Throws<ArgumentException>(() => new ConceptEntity(_idBeer, quantity, unitPrice));
            Assert.Equal(expectedMessage, actualException.Message);
        }

        [Theory]
        [InlineData(-10, 5, "La cantidad debe ser superior a 0.")]
        [InlineData(0, 5, "La cantidad debe ser superior a 0.")]
        [InlineData(10, -5, "El precio unitario debe ser superior a 0.")]
        [InlineData(10, 0, "El precio unitario debe ser superior a 0.")]
        public void SecondConstructorThrowsExceptionWhenNegativeOrZeroQuantityOrUnitPrice(int quantity, decimal unitPrice, string expectedMessage)
        {
            var actualException = Assert.Throws<ArgumentException>(() => new ConceptEntity(_id, _idBeer, quantity, unitPrice));
            Assert.Equal(expectedMessage, actualException.Message);
        }
    }
}


using _1___Entities;

namespace Entities.Tests
{
    public class SaleEntityTest
    {
        private readonly int _idSale;
        private readonly int _idNewConcept = 4;
        private readonly int _idBeer = 1;
        private readonly List<ConceptEntity> _concepts;

        public SaleEntityTest()
        {
            _concepts = new List<ConceptEntity>()
            {
                new ConceptEntity(1, _idBeer, 5, 5),
                new ConceptEntity(2, _idBeer, 10, 5),
                new ConceptEntity(3, _idBeer, 15, 5)
            };
        }

        // Esta prueba añade un nuevo concepto a la lista de conceptos para calcular el sumatorio del precio de cada concepto.
        // La lista inicial ya suma un total de 150.
        [Theory]
        [InlineData(10, 5, 200)]    // 10 unidades a 5 de precio por unidad, total esperado 200
        [InlineData(1, 1, 151)]     // 1 unidad a 1 de precio por unidad, total esperado 151
        [InlineData(100, 10, 1150)] // 100 unidades a 10 de precio por unidad, total esperado 1150
        public void CalculatesSaleTotal_Correctly(int newConceptQuantity, decimal newConceptUnitPrice, decimal expected)
        {
            var newConcept = new ConceptEntity(_idNewConcept, _idBeer, newConceptQuantity, newConceptUnitPrice);
            _concepts.Add(newConcept);
            var saleEntity = new SaleEntity(DateTime.Now, _concepts);

            Assert.Equal(expected, saleEntity.Total);
        }

        [Fact]
        public void HandlesEmptyConceptList_Correctly()
        {
            var saleEntity = new SaleEntity(DateTime.Now, new List<ConceptEntity>());

            Assert.Equal(0, saleEntity.Total);
        }

        [Fact]
        public void FirstConstructorInitializesCorrectly()
        {
            var date = DateTime.Now;
            var saleEntity = new SaleEntity(date, _concepts);

            Assert.Equal(date, saleEntity.Date);
            Assert.Collection(saleEntity.Concepts,
                item =>
                {
                    Assert.Equal(1, item.Id);
                    Assert.Equal(5, item.Quantity);
                    Assert.Equal(5m, item.UnitPrice);
                    Assert.Equal(25m, item.ConceptPrice);
                },
                item =>
                {
                    Assert.Equal(2, item.Id);
                    Assert.Equal(10, item.Quantity);
                    Assert.Equal(5m, item.UnitPrice);
                    Assert.Equal(50m, item.ConceptPrice);
                },
                item =>
                {
                    Assert.Equal(3, item.Id);
                    Assert.Equal(15, item.Quantity);
                    Assert.Equal(5m, item.UnitPrice);
                    Assert.Equal(75m, item.ConceptPrice);
                });
            Assert.Equal(150, saleEntity.Total);
        }

        [Fact]
        public void SecondConstructorInitializesCorrectly()
        {
            var date = DateTime.Now;
            var saleEntity = new SaleEntity(_idSale, date, _concepts);

            Assert.Equal(_idSale, saleEntity.Id);
            Assert.Equal(date, saleEntity.Date);
            Assert.Equal(_concepts, saleEntity.Concepts);
            Assert.Collection(saleEntity.Concepts,
                item =>
                {
                    Assert.Equal(1, item.Id);
                    Assert.Equal(5, item.Quantity);
                    Assert.Equal(5m, item.UnitPrice);
                    Assert.Equal(25m, item.ConceptPrice);
                },
                item =>
                {
                    Assert.Equal(2, item.Id);
                    Assert.Equal(10, item.Quantity);
                    Assert.Equal(5m, item.UnitPrice);
                    Assert.Equal(50m, item.ConceptPrice);
                },
                item =>
                {
                    Assert.Equal(3, item.Id);
                    Assert.Equal(15, item.Quantity);
                    Assert.Equal(5m, item.UnitPrice);
                    Assert.Equal(75m, item.ConceptPrice);
                });
            Assert.Equal(150, saleEntity.Total);
        }
    }
}

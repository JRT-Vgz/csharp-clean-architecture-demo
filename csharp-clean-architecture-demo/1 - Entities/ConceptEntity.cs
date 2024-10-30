
namespace _1___Entities
{
    public class ConceptEntity
    {
        public int Id { get; set; }

        public int IdSale { get; set; }
        public int IdBeer { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ConceptPrice { get; set; }

        public ConceptEntity(int idBeer, int quantity, decimal unitPrice) 
        { 
            IdBeer = idBeer;
            Quantity = quantity;
            UnitPrice = unitPrice;
            ConceptPrice = CalculateTotal();
        }

        public ConceptEntity(int id, int idBeer, int quantity, decimal unitPrice)
        {
            Id = id;
            IdBeer = idBeer;
            Quantity = quantity;
            UnitPrice = unitPrice;
            ConceptPrice = CalculateTotal();
        }

        private decimal CalculateTotal()
            => Quantity * UnitPrice;
    }
}

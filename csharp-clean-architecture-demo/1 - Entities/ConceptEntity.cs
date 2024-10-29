
namespace _1___Entities
{
    public class ConceptEntity
    {
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

        private decimal CalculateTotal()
            => Quantity * UnitPrice;
    }
}

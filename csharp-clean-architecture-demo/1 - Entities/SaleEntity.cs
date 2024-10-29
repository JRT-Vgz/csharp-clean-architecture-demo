
namespace _1___Entities
{
    public class SaleEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public List<ConceptEntity> Concepts { get; set; }

        public SaleEntity(DateTime date, List<ConceptEntity> concepts)
        {
            Date = date;
            Concepts = concepts;
            Total = CalculateTotal();
        }
        public SaleEntity(int id, DateTime date, List<ConceptEntity> concepts)
        {
            Id = id;
            Date = date;
            Concepts = concepts;
            Total = CalculateTotal();
        }

        private decimal CalculateTotal()
            => Concepts.Sum(c => c.ConceptPrice);
    }
}

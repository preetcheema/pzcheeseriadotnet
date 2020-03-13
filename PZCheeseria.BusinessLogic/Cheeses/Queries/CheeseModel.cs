namespace PZCheeseria.BusinessLogic.Cheeses.Queries
{
    public class CheeseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerKilo { get; set; }
        public string ImageName { get; set; }
        public string Colour { get; set; }
    }
}
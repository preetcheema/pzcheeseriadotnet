using MediatR;

namespace PZCheeseria.BusinessLogic.Cheeses.Commands
{
    public class AddCheeseCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Colour { get; set; }
    }
}
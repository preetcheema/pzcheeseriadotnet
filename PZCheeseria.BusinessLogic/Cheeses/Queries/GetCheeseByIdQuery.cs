using MediatR;

namespace PZCheeseria.BusinessLogic.Cheeses.Queries
{
    public class GetCheeseByIdQuery : IRequest<CheeseModel>
    {
        public int Id { get; set; }
    }
}
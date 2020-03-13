using System.Collections.Generic;
using MediatR;

namespace PZCheeseria.BusinessLogic.Cheeses.Queries
{
    public class GetAllCheesesQuery : IRequest<IEnumerable<CheeseModel>>
    {
    }
}
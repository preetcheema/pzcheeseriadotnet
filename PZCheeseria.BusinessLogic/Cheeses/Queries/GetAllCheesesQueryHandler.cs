using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PZCheeseria.Persistence;

namespace PZCheeseria.BusinessLogic.Cheeses.Queries
{
    public class GetAllCheesesQueryHandler : IRequestHandler<GetAllCheesesQuery, IEnumerable<CheeseModel>>
    {
        private readonly PZCheeseriaContext _context;

        public GetAllCheesesQueryHandler(PZCheeseriaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CheeseModel>> Handle(GetAllCheesesQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Cheeses.Select(m => new CheeseModel
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                PricePerKilo = m.PricePerKilo,
                ImageName = m.ImageName,
                Colour = m.Colour.Colour
            }).ToListAsync(cancellationToken: cancellationToken);
            return result;
        }
    }
}
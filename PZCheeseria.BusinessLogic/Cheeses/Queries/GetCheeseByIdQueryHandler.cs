using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PZCheeseria.BusinessLogic.Exceptions;
using PZCheeseria.Persistence;

namespace PZCheeseria.BusinessLogic.Cheeses.Queries
{
    public class GetCheeseByIdQueryHandler : IRequestHandler<GetCheeseByIdQuery, CheeseModel>
    {
        private readonly PZCheeseriaContext _context;

        public GetCheeseByIdQueryHandler(PZCheeseriaContext context)
        {
            _context = context;
        }

        public async Task<CheeseModel> Handle(GetCheeseByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Cheeses.Where(m => m.Id == request.Id).Select(m => new CheeseModel
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                PricePerKilo = m.PricePerKilo,
                ImageName = m.ImageName,
                Colour = m.Colour.Colour
            }).SingleOrDefaultAsync(cancellationToken: cancellationToken);


            if (result == null) throw new EntityNotFoundException(nameof(CheeseModel), request.Id);

            return result;
        }
    }
}
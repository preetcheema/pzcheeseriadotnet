using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PZCheeseria.BusinessLogic.Infrastructure;
using PZCheeseria.Common;
using PZCheeseria.Domain.Entities;
using PZCheeseria.Persistence;

namespace PZCheeseria.BusinessLogic.Cheeses.Commands
{
    public class AddCheeseCommandHandler : IRequestHandler<AddCheeseCommand, int>
    {
        private readonly PZCheeseriaContext _context;
        private readonly ITimeProvider _timeProvider;

        public AddCheeseCommandHandler(PZCheeseriaContext context, ITimeProvider timeProvider)
        {
            _context = context;
            _timeProvider = timeProvider;
        }

        public async Task<int> Handle(AddCheeseCommand request, CancellationToken cancellationToken)
        {
            var cheeseColor =await _context.CheeseColours.SingleOrDefaultAsync(m => m.Colour == request.Colour, cancellationToken: cancellationToken);
            Func<string,bool> cheesealreadyExists = (name) => _context.Cheeses.SingleOrDefault(m => m.Name == name) != null;
            
            //we dont really need color name but do it for consistency. we just check cheesecolor to avoid double trip to database
            Func<string, bool> cheeseColorExists = (colorName) => cheeseColor != null;
           
            var validator = new AddCheeseCommandValidator(cheesealreadyExists,cheeseColorExists);
            validator.ValidateAndThrowUnProcessableEntityException(request);
            
            var item =new Cheese
            {
                Name = request.Name,
                Description = request.Description,
                PricePerKilo = request.Price,
                ImageName = request.Image,
                Colour = cheeseColor,
                CreatedOn = _timeProvider.Now()
                
            };
            
            _context.Cheeses.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
            return item.Id;
        }
    }
}
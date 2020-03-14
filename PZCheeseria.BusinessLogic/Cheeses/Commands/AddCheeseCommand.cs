using System.Collections.Generic;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using PZCheeseria.Common;
using PZCheeseria.Persistence;

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

    public class AddCheeseCommandHandler : IRequestHandler<AddCheeseCommand, int>
    {
        private readonly PZCheeseriaContext _context;
        private readonly ITimeProvider _timeProvider;

        public AddCheeseCommandHandler(PZCheeseriaContext context, ITimeProvider timeProvider)
        {
            _context = context;
            _timeProvider = timeProvider;
        }

        public Task<int> Handle(AddCheeseCommand request, CancellationToken cancellationToken)
        {
            var validator = new AddCheeseCommandValidator();

            return Task.FromResult(1);
        }
    }

    public class AddCheeseCommandValidator : AbstractValidator<AddCheeseCommand>
    {
        private readonly List<string> imageExtensions = new List<string> {".jpg", ".jpeg"};

        public AddCheeseCommandValidator()
        {
            RuleFor(m => m.Name)
                .NotNull()
                .MaximumLength(100);
            
            RuleFor(m => m.Price)
                .NotNull()
                .InclusiveBetween(1, 1000);
            
            //Rule inside 'must' condition could also be built as a custom validator
            RuleFor(m => m.Image)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .Must(m =>
                {
                    var parts = m.Split('.');
                    if (parts.Length < 2) return false;
                    var extension = parts[^1];
                    return imageExtensions.Contains(extension.ToLowerInvariant());
                });

            RuleFor(m => m.Description)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .MaximumLength(1000);
        }
    }
}
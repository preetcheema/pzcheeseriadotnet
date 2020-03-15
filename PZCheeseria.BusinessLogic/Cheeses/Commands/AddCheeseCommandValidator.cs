using System;
using System.Collections.Generic;
using FluentValidation;

namespace PZCheeseria.BusinessLogic.Cheeses.Commands
{
    public class AddCheeseCommandValidator : AbstractValidator<AddCheeseCommand>
    {
        private readonly List<string> imageExtensions = new List<string> {"jpg", "jpeg"};

        /// <summary>
        /// Validations for AddCheeseCommand
        /// </summary>
        /// <param name="cheesealreadyExistsFunc"></param>
        /// <param name="colorExistsFunc"></param>
        /// <remarks>We could add more descriptive messages on each of the failed conditions</remarks>
        public AddCheeseCommandValidator(Func<string, bool> cheesealreadyExistsFunc, Func<string, bool> colorExistsFunc)
        {
            RuleFor(m => m.Name)
                .NotNull()
                .MaximumLength(100)
                .Must(m=>cheesealreadyExistsFunc(m)==false);

            RuleFor(m => m.Colour)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .Must(colorExistsFunc);
            
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
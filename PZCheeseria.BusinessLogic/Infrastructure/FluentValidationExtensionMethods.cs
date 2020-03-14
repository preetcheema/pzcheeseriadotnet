using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using PZCheeseria.BusinessLogic.Exceptions;

namespace PZCheeseria.BusinessLogic.Infrastructure
{
    public static class FluentValidationExtensionMethods
    {
        public static void ValidateAndThrowUnProcessableEntityException<T>(this AbstractValidator<T> validator, T model)
        {
            var validationResult = validator.Validate(model);

            if (validationResult.IsValid)
            {
                return;

            }

            var modelStateException = new UnProcessableEntityException("Please see Errors and Property Errors for more information");
            var propertyErrors = new List<ModelStateError>();
            foreach (var validationError in validationResult.Errors)
            {
                propertyErrors.Add(new ModelStateError
                {
                    PropertyName = validationError.PropertyName,
                    Error = validationError.ErrorMessage
                });
            }
            modelStateException.ModelStateErrors = propertyErrors;

            throw modelStateException;
        }
    }

   }
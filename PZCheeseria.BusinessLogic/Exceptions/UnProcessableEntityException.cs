using System;
using System.Collections.Generic;
using System.Linq;

namespace PZCheeseria.BusinessLogic.Exceptions
{
    public class UnProcessableEntityException : Exception
    {
        public UnProcessableEntityException()
        {
            Errors= Enumerable.Empty<string>();
            ModelStateErrors=Enumerable.Empty<ModelStateError>();
        }

        public UnProcessableEntityException(string msg):base(msg)
        {
            Errors= Enumerable.Empty<string>();
            ModelStateErrors=Enumerable.Empty<ModelStateError>(); 
        }
        public IEnumerable<string> Errors { get; set; }

        public IEnumerable<ModelStateError> ModelStateErrors { get; set; }
    }
}
using System;

namespace PZCheeseria.Common
{
    public interface ITimeProvider
    {
        DateTime Now();
    }
}
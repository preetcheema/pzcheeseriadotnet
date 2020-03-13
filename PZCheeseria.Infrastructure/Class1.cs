using System;
using PZCheeseria.Common;

namespace PZCheeseria.Infrastructure
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime Now() => DateTime.Now;
    }
}
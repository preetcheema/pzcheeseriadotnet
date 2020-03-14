using System;
using PZCheeseria.Common;

namespace PZCheeseria.BusinessLogic.Tests.Infrastructure
{
    public class FakeDatetimeProvider : ITimeProvider
    {
        public DateTime Now()
            => new DateTime(2020, 3, 14);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PZCheeseria.BusinessLogic.Cheeses.Queries;
using PZCheeseria.Domain.Entities;
using Xunit;

namespace PZCheeseria.BusinessLogic.Tests.Cheeses.Queries
{
    public abstract class GetCheeseTestBase : IClassFixture<TestBaseFixture>
    {
        private TestBaseFixture _fixture;
        protected IEnumerable<CheeseModel> CheeseCollection;

        protected GetCheeseTestBase(TestBaseFixture fixture)
        {
            _fixture = fixture;
            var ac = ProcessBeforeTest();
            ac();
            CheeseCollection = new GetAllCheesesQueryHandler(_fixture.Context).Handle(new GetAllCheesesQuery(), CancellationToken.None).GetAwaiter().GetResult();
        }

        protected IEnumerable<Cheese> GetAllCheeseFromDatabase() => _fixture.Context.Cheeses.ToList();

        protected virtual Action ProcessBeforeTest() => () => { };

        protected void ClearAllCheeseInDatabase()
        {
            var cheeseCollection = _fixture.Context.Cheeses.ToList();
            _fixture.Context.Cheeses.RemoveRange(cheeseCollection);
            _fixture.Context.SaveChanges();
        }
    }
}
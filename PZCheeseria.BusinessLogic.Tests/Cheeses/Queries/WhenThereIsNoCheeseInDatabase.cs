using System;
using System.Linq;
using Shouldly;
using Xunit;

namespace PZCheeseria.BusinessLogic.Tests.Cheeses.Queries
{
    public class WhenThereIsNoCheeseInDatabase : GetCheeseTestBase
    {
        public WhenThereIsNoCheeseInDatabase(TestBaseFixture fixture) : base(fixture)
        {
           
        }

        protected override Action ProcessBeforeTest() => ClearAllCheeseInDatabase;

        [Fact]
        public void AnEmptyCollectionIsReturned()
        {
            CheeseCollection.Count().ShouldBe(0);
        }
    }
}
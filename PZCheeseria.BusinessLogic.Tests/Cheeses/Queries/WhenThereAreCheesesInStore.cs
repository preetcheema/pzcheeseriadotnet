using System.Linq;
using Shouldly;
using Xunit;

namespace PZCheeseria.BusinessLogic.Tests.Cheeses.Queries
{
    public class WhenThereAreCheesesInDatabase : GetCheeseTestBase
    {
        public WhenThereAreCheesesInDatabase(TestBaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void ReturnedResultsContainCorrectData()
        {
            var cheeseInDatabase = GetAllCheeseFromDatabase();

            CheeseCollection.ShouldSatisfyAllConditions(
                () => { CheeseCollection.Count().ShouldBe(cheeseInDatabase.Count()); },
                () =>
                {
                    foreach (var cheese in cheeseInDatabase)
                    {
                        var cheeseInCollection = CheeseCollection.SingleOrDefault(m => m.Id == cheese.Id);
                        cheeseInCollection.ShouldNotBeNull();
                        cheeseInCollection.Colour.ShouldBe(cheese.Colour.Colour);
                        cheeseInCollection.Description.ShouldBe(cheese.Description);
                        cheeseInCollection.Name.ShouldBe(cheese.Name);
                        cheeseInCollection.PricePerKilo.ShouldBe(cheese.PricePerKilo);
                        cheeseInCollection.ImageName.ShouldBe(cheese.ImageName);
                    }
                }
            );
        }
    }
}
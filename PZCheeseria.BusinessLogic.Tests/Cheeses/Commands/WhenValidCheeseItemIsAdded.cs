using System.Linq;
using System.Threading;
using PZCheeseria.BusinessLogic.Cheeses.Commands;
using PZCheeseria.BusinessLogic.Exceptions;
using PZCheeseria.BusinessLogic.Tests.Infrastructure;
using Shouldly;
using Xunit;

namespace PZCheeseria.BusinessLogic.Tests.Cheeses.Commands
{
    public class WhenValidCheeseItemIsAdded : IClassFixture<TestBaseFixture>
    {
        private readonly TestBaseFixture _fixture;

        public WhenValidCheeseItemIsAdded(TestBaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void AValidCheeseItemIsCreated()
        {
            int createdItemId = 0;
            UnProcessableEntityException couldNotCreateEntityException = null;
            var color = _fixture.Context.CheeseColours.First().Colour;

            var command = new AddCheeseCommand
            {
                Name = "Aged Gouda",
                Description = "Greate cheese",
                Colour = color,
                Image = "test.jpg",
                Price = 90
            };

            try
            {
                createdItemId = new AddCheeseCommandHandler(_fixture.Context, new FakeDatetimeProvider()).Handle(command, CancellationToken.None).GetAwaiter().GetResult();
            }
            catch (UnProcessableEntityException ex)
            {
                couldNotCreateEntityException = ex;
            }

            couldNotCreateEntityException.ShouldBe(null);
            createdItemId.ShouldBeGreaterThan(0);

            var createdItem = _fixture.Context.Cheeses.SingleOrDefault(m => m.Id == createdItemId);

            createdItem.ShouldNotBeNull();
            
            createdItem.ShouldSatisfyAllConditions(
                () => createdItem.Name.ShouldBe(command.Name),
                ()=>createdItem.Description.ShouldBe(command.Description),
                ()=>createdItem.Colour.Colour.ShouldBe(command.Colour),
                ()=>createdItem.ImageName.ShouldBe(command.Image),
                ()=>createdItem.PricePerKilo.ShouldBe(command.Price)
            );
        }
    }
}
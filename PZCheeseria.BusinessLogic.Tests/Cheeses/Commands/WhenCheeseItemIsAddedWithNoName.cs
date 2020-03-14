using System.Linq;
using System.Threading;
using PZCheeseria.BusinessLogic.Cheeses.Commands;
using PZCheeseria.BusinessLogic.Exceptions;
using PZCheeseria.BusinessLogic.Tests.Infrastructure;
using Shouldly;
using Xunit;

namespace PZCheeseria.BusinessLogic.Tests.Cheeses.Commands
{
    public class WhenCheeseItemIsAddedWithNoName : IClassFixture<TestBaseFixture>
    {
        private readonly TestBaseFixture _fixture;

        public WhenCheeseItemIsAddedWithNoName(TestBaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ExceptionIsThrownWithCorrectDetaisl()
        {
            int createdItemId = 0;
            UnProcessableEntityException couldNotCreateEntityException = null;
            var color = _fixture.Context.CheeseColours.First().Colour;

            var command = new AddCheeseCommand
            {
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
            
            couldNotCreateEntityException.ShouldNotBeNull();
            couldNotCreateEntityException.ModelStateErrors.Count().ShouldBe(1);
            var modelStateError = couldNotCreateEntityException.ModelStateErrors.First();
            modelStateError.PropertyName.ShouldBe(nameof(AddCheeseCommand.Name));
        }
    }
}
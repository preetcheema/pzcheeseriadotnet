using System;
using Microsoft.EntityFrameworkCore;
using PZCheeseria.Persistence;

namespace PZCheeseria.BusinessLogic.Tests
{
    public class TestBaseFixture : IDisposable
    {
        public PZCheeseriaContext Context { get; set; }
        protected DateTime FixedDateTime => new DateTime(2020, 3, 14);

        public TestBaseFixture()
        {
            var options = new DbContextOptionsBuilder<PZCheeseriaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            Context = new PZCheeseriaContext(options);

            CreateDataForTests();
        }

        private void CreateDataForTests()
        {
            PZCheeseriaSeedDataCreator.SeedData(Context, () => FixedDateTime);
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();

            Context.Dispose();
        }
    }
}
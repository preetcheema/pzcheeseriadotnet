using Microsoft.EntityFrameworkCore;
using PZCheeseria.Domain.Entities;

namespace PZCheeseria.Persistence
{
    public class PZCheeseriaContext:DbContext
    {
        public PZCheeseriaContext(DbContextOptions<PZCheeseriaContext> options):base(options)
        {
            
        }
        public DbSet<Cheese> Cheeses { get; set; }
        public DbSet<CheeseColour>CheeseColours { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PZCheeseriaContext).Assembly);
        }
    }
}
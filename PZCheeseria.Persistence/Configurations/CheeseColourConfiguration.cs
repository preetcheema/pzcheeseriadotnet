using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PZCheeseria.Domain.Entities;

namespace PZCheeseria.Persistence.Configurations
{
    public class CheeseColourConfiguration : IEntityTypeConfiguration<CheeseColour>
    {
        public void Configure(EntityTypeBuilder<CheeseColour> builder)
        {
            builder.HasKey(m => m.Id); //not required to specify, works by convention
            builder.Property(m => m.Colour)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(m => m.Colour)
                .IsUnique();
        }
    }
}
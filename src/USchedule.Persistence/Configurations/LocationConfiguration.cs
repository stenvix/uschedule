using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using USchedule.Core.Entities.Implementations;

namespace USchedule.Persistence.Configurations
{
    public class LocationConfiguration: IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Latitude).IsRequired();
            builder.Property(i => i.Longitude).IsRequired();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using USchedule.Core.Entities.Implementations;

namespace USchedule.Persistence.Configurations
{
    public class BuildingConfiguration : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Title).IsRequired();
            builder.Property(i => i.ShortTitle).IsRequired();
            builder.HasOne(i => i.Location).WithMany().HasForeignKey(i => i.LocationId);
        }
    }
}
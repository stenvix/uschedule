using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using USchedule.Core.Entities.Implementations;

namespace USchedule.Persistence.Configurations
{
    public class OrganisationConfiguration : IEntityTypeConfiguration<Organisation>
    {
        public void Configure(EntityTypeBuilder<Organisation> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Title).IsRequired();
            builder.Property(i => i.ShortTitle).IsRequired();
            builder.HasIndex(i => i.Title).IsUnique();
            builder.HasIndex(i => i.ShortTitle).IsUnique();
            builder.HasOne(i => i.Building).WithMany().HasForeignKey(i => i.BuildingId);
        }
    }
}
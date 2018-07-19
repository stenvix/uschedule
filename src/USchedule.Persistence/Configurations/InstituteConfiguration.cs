using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using USchedule.Core.Entities.Implementations;

namespace USchedule.Persistence.Configurations
{
    public class InstituteConfiguration : IEntityTypeConfiguration<Institute>
    {
        public void Configure(EntityTypeBuilder<Institute> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Title).IsRequired();
            builder.Property(i => i.ShortTitle).IsRequired();
            builder.HasIndex(i => new {i.Title, i.UniversityId}).IsUnique();
            builder.HasIndex(i => new {i.ShortTitle, i.UniversityId}).IsUnique();
            builder.HasOne(i => i.University).WithMany().HasForeignKey(i => i.UniversityId);
            builder.HasOne(i => i.Building).WithMany().HasForeignKey(i => i.BuildingId);
        }
    }
}
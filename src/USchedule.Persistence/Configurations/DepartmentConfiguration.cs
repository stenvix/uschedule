using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using USchedule.Core.Entities.Implementations;

namespace USchedule.Persistence.Configurations
{
    public class DepartmentConfiguration: IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Title).IsRequired();
            builder.Property(i => i.ShortTitle).IsRequired();
            builder.HasIndex(i => new {i.Title, i.InstituteId}).IsUnique();
            builder.HasIndex(i => new {i.ShortTitle, i.InstituteId}).IsUnique();
            builder.HasOne(i => i.Institute).WithMany().HasForeignKey(i => i.InstituteId);
            builder.HasOne(i => i.Room).WithMany().HasForeignKey(i => i.RoomId);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using USchedule.Core.Entities.Implementations;

namespace USchedule.Persistence.Configurations
{
    public class GroupConfiguration: IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Title).IsRequired();
            builder.HasIndex(i => new {i.Title, i.DepartmentId}).IsUnique();
            builder.HasOne(i => i.Department).WithMany().HasForeignKey(i => i.DepartmentId);
        }
    }
}
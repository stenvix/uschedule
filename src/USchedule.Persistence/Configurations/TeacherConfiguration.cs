using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using USchedule.Core.Entities.Implementations;

namespace USchedule.Persistence.Configurations
{
    public class TeacherConfiguration: IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.FirstName).IsRequired();
            builder.Property(i => i.LastName).IsRequired();
//            builder.Property(i => i.MiddleName).IsRequired();
            builder.HasOne(i => i.Department).WithMany().HasForeignKey(i => i.DepartmentId);
        }
    }
}
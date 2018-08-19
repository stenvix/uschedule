using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using USchedule.Core.Entities.Implementations;

namespace USchedule.Persistence.Configurations
{
    public class TeacherSubjectConfiguration : IEntityTypeConfiguration<TeacherSubject>
    {
        public void Configure(EntityTypeBuilder<TeacherSubject> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasOne(i => i.Subject).WithMany().HasForeignKey(i => i.SubjectId);
            builder.HasOne(i => i.Teacher).WithMany().HasForeignKey(i => i.TeacherId);
            builder.HasIndex(i => new {i.TeacherId, i.SubjectId}).IsUnique();
        }
    }
}
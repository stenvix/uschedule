using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using USchedule.Core.Entities.Implementations;

namespace USchedule.Persistence.Configurations
{
    public class LessonConfiguration: IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasOne(i => i.Group).WithMany().HasForeignKey(i => i.GroupId);
            builder.HasOne(i => i.Room).WithMany().HasForeignKey(i => i.RoomId);
            builder.HasOne(i => i.LessonTime).WithMany().HasForeignKey(i => i.TimeId);
            builder.HasOne(i => i.Semester).WithMany().HasForeignKey(i => i.SemesterId);
            builder.HasOne(i => i.TeacherSubject).WithMany().HasForeignKey(i => i.TeacherSubjectId);
        }
    }
}
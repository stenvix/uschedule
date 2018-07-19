using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using USchedule.Core.Entities.Implementations;

namespace USchedule.Persistence.Configurations
{
    public class LessonTimeConfiguration:IEntityTypeConfiguration<LessonTime>
    {
        public void Configure(EntityTypeBuilder<LessonTime> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Start).IsRequired();
            builder.Property(i => i.End).IsRequired();
            builder.HasOne(i => i.University).WithMany().HasForeignKey(i => i.UniversityId);
        }
    }
}
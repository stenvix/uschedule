using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using USchedule.Core.Entities.Implementations;

namespace USchedule.Persistence.Configurations
{
    public class SubjectConfiguration: IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Title).IsRequired();
            builder.HasIndex(i => new {i.Title, i.UniversityId});
            builder.HasOne(i => i.University).WithMany().HasForeignKey(i => i.UniversityId);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using USchedule.Core.Entities.Implementations;

namespace USchedule.Persistence.Configurations
{
    public class RoomConfiguration: IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Number).IsRequired();
            builder.HasOne(i => i.Building).WithMany().HasForeignKey(i => i.BuildingId);
        }
    }
}
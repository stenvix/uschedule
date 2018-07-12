using Microsoft.EntityFrameworkCore;
using USchedule.Core.Entities.Implementations;
using USchedule.Persistence.Configurations;

namespace USchedule.Persistence.Database
{
    public class DataContext : DbContext
    {
        #region Properties

        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Institute> Institutes { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherSubject> TeacherSubjects { get; set; }
        public DbSet<Time> Times { get; set; }

        #endregion
        
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new OrganisationConfiguration());
        }
    }
}
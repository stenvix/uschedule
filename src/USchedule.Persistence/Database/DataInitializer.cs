using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using USchedule.Core.Entities.Implementations;

namespace USchedule.Persistence.Database
{
    public static class DataInitializer
    {
        public static void Initialize(this DataContext context, ILogger<DataContext> logger)
        {
            if (!context.Database.EnsureCreated())
            {
                return;
            }

            try
            {
                var fileContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Resources/db.json"));
                var seed = JsonConvert.DeserializeObject<DataSeed>(fileContent);

                context.Universities.Add(seed.University);
                context.SaveChanges();
                context.Locations.AddRange(seed.Locations);
                context.SaveChanges();
                context.Buildings.AddRange(seed.Buildings);
                context.SaveChanges();
                context.Institutes.AddRange(seed.Institutes);
                context.SaveChanges();
                context.LessonTimes.AddRange(seed.LessonTimes);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Database initialization failed. See log for more details.");
                throw;
            }
        }
    }

    internal class DataSeed
    {
        public University University { get; set; }
        public IList<Location> Locations { get; set; }
        public IList<Building> Buildings { get; set; }
        public IList<Institute> Institutes { get; set; }
        public IList<Department> Departments { get; set; }
        public IList<LessonTime> LessonTimes { get; set; }
    }
}
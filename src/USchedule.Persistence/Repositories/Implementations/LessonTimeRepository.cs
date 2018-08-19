using USchedule.Core.Entities.Implementations;
using USchedule.Persistence.Database;

namespace USchedule.Persistence.Repositories
{
    public class LessonTimeRepository: Repository<LessonTime>, ILessonTimeRepository
    {
        public LessonTimeRepository(DataContext context) : base(context)
        {
        }
    }
}
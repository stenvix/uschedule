using Microsoft.EntityFrameworkCore;
using USchedule.Core.Entities.Implementations;

namespace USchedule.Persistence.Repositories
{
    public class LessonRepository: Repository<Lesson>, ILessonRepository
    {
        public LessonRepository(DbContext context) : base(context)
        {
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using USchedule.Core.Entities.Implementations;

namespace USchedule.Persistence.Repositories
{
    public interface ITeacherRepository: IRepository<Teacher>
    {
        Task<List<Teacher>> GetExistedAsync(IEnumerable<Teacher> teachers);
    }
}
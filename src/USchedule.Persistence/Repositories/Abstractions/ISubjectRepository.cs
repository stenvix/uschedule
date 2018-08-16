using System.Collections.Generic;
using System.Threading.Tasks;
using USchedule.Core.Entities.Implementations;

namespace USchedule.Persistence.Repositories
{
    public interface ISubjectRepository: IRepository<Subject>
    {
        Task<List<Subject>> GetExistedAsync(IList<Subject> entities);
    }
}
using USchedule.Persistence.Repositories;

namespace USchedule.Domain.Managers.Base
{
    public interface IAppUnitOfWork : IUnitOfWork
    {
        IDepartmentRepository DepartmentRepository { get; }
        IGroupRepository GroupRepository { get; }
        IInstituteRepository InstituteRepository { get; }
        IUniversityRepository UniversityRepository { get; }
        ILessonRepository LessonRepository { get; }
        ITeacherRepository TeacherRepository { get; }
        ISubjectRepository SubjectRepository { get; }
        ITeacherSubjectRepository TeacherSubjectRepository { get; }
        IRoomRepository RoomRepository { get; }
        IBuildingRepository BuildingRepository { get; }
        ILessonTimeRepository LessonTimeRepository { get; }
    }
}
using USchedule.Persistence.Repositories;

namespace USchedule.Domain.Managers.Base
{
    public interface IAppUnitOfWork : IUnitOfWork
    {
        IUniversityRepository UniversityRepository { get; }
//        IAccountRepository AccountRepository { get; }
//        IBlockedCustomerRepository BlockedCustomerRepository { get; }
//        ICategoryRepository CategoryRepository { get; }
//        ICompanyRepository CompanyRepository { get; }
//        ICustomerRepository CustomerRepository { get; }
//        IFeatureRepository FeatureRepository { get; }
//        IMessageRepository MessageRepository { get; }
//        ITagRepository TagRepository { get; }
//        ITaskRepository TaskRepository { get; }
//        INoteRepository NoteRepository { get; }
//        ITaskTypeRepository TaskTypeRepository { get; }
//        ITemplateRepository TemplateRepository { get; }
//        IThreadRepository ThreadRepository { get; }
//        IUserRepository UserRepository { get; }
//        IUpdateLogRepository UpdatesLogRepository { get; }
//        ISubscriptionPlanRepository SubscriptionPlanRepository { get; }
//        ISubscriptionPlanFeatureRepository SubscriptionPlanFeatureRepository { get; }
//        IProductRepository ProductRepository { get; }
//        IOrderRepository OrderRepository { get; }
//        IMessageTranslateRepository MessageTranslateRepository { get; }
    }
}
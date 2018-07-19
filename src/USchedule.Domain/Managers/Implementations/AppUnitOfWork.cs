using System.Threading.Tasks;
using USchedule.Persistence.Database;

namespace USchedule.Domain.Managers
{
    public class AppUnitOfWork: IAppUnitOfWork
    {
        #region Fields

        protected readonly DataContext Context;
//        private IAccountRepository _accountRepository;
//        private IBlockedCustomerRepository _blockedCustomerRepository;
//        private IUserRepository _userRepository;
//        private ICategoryRepository _categoryRepository;
//        private ICompanyRepository _companyRepository;
//        private ICustomerRepository _customerRepository;
//        private IFeatureRepository _featureRepository;
//        private IMessageRepository _messageRepository;
//        private ITagRepository _tagRepository;
//        private ITaskRepository _taskRepository;
//        private ITaskTypeRepository _taskTypeRepository;
//        private ITemplateRepository _templateRepository;
//        private IThreadRepository _threadRepository;
//        private INoteRepository _noteRepository;
//        private IUpdateLogRepository _updatesLogRepositoryRepository;
//        private ISubscriptionPlanRepository _subscriptionPlanRepository;
//        private ISubscriptionPlanFeatureRepository _subscriptionPlanFeatureRepository;
//        private IProductRepository _productRepository;
//        private IOrderRepository _orderRepository;
//        private IMessageTranslateRepository _messageTranslateRepository;

       #endregion

//        #region Properties
//
//        public IAccountRepository AccountRepository => _accountRepository ??
//                                                       (_accountRepository = new AccountRepository(Context));
//        public IBlockedCustomerRepository BlockedCustomerRepository => _blockedCustomerRepository ??
//                                                                       (_blockedCustomerRepository = new BlockedCustomerRepository(Context));
//        public ICategoryRepository CategoryRepository => _categoryRepository ??
//                                                         (_categoryRepository = new CategoryRepository(Context));
//        public ICompanyRepository CompanyRepository => _companyRepository ??
//                                                       (_companyRepository = new CompanyRepository(Context));
//        public ICustomerRepository CustomerRepository => _customerRepository ??
//                                                         (_customerRepository = new CustomerRepository(Context));
//        public IFeatureRepository FeatureRepository => _featureRepository ??
//                                                       (_featureRepository = new FeatureRepository(Context));
//        public IMessageRepository MessageRepository => _messageRepository ??
//                                                       (_messageRepository = new MessageRepository(Context));
//        public ITagRepository TagRepository => _tagRepository ??
//                                               (_tagRepository = new TagRepository(Context));
//        public ITaskRepository TaskRepository => _taskRepository ??
//                                                 (_taskRepository = new TaskRepository(Context));
//        public INoteRepository NoteRepository => _noteRepository ??
//                                                 (_noteRepository = new NoteRepository(Context));
//        public ITaskTypeRepository TaskTypeRepository => _taskTypeRepository ??
//                                                         (_taskTypeRepository = new TaskTypeRepository(Context));
//        public ITemplateRepository TemplateRepository => _templateRepository ??
//                                                         (_templateRepository = new TemplateRepository(Context));
//        public IThreadRepository ThreadRepository => _threadRepository ??
//                                                     (_threadRepository = new ThreadRepository(Context));
//        public IUserRepository UserRepository => _userRepository ??
//                                                 (_userRepository = new UserRepository(Context));
//        public IUpdateLogRepository UpdatesLogRepository => _updatesLogRepositoryRepository ??
//                                                 (_updatesLogRepositoryRepository = new UpdatesLogRepository(Context));
//
//        public ISubscriptionPlanRepository SubscriptionPlanRepository =>
//            _subscriptionPlanRepository ?? (_subscriptionPlanRepository = new SubscriptionPlanRepository(Context));
//
//        public ISubscriptionPlanFeatureRepository SubscriptionPlanFeatureRepository =>
//            _subscriptionPlanFeatureRepository ?? (_subscriptionPlanFeatureRepository = new SubscriptionPlanFeatureRepositoryRepository(Context));
//
//        public IProductRepository ProductRepository => _productRepository ?? (_productRepository = new ProductRepository(Context));
//
//        public IOrderRepository OrderRepository => _orderRepository ?? (_orderRepository = new OrderRepository(Context));
//        public IMessageTranslateRepository MessageTranslateRepository => _messageTranslateRepository ?? (_messageTranslateRepository = new MessageTranslateRepository(Context));
//
//        #endregion
//
//        public AppUnitOfWork(ReplycoContext context)
//        {
//            Context = context;
//        }
//
        public Task SaveChanges()
        {
            return Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

    }
}
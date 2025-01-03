
namespace SMS.Repository
{
    using SMS.Data;
    using SMS.Models;

    public class RepositoryService : IRepositoryService
    {
        private readonly SMSDbContext data;

        public RepositoryService(SMSDbContext data)
        {
            this.data = data;
        }

    
    }
}

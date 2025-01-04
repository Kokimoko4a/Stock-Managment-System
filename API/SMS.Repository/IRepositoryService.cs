namespace SMS.Repository
{
    using SMS.Models;

    public interface IRepositoryService
    {
        public ApplicationUser GetUser(string email);

        public Task AddManager(Manager entity);
    }
}

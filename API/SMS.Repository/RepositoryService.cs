
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


        public ApplicationUser GetUser(string email)
        {
            return data.Users.First(u => u.Email == email);
        }

        public async Task AddManager(Manager entity)
        {
            data.Managers.Add(entity);

            await data.SaveChangesAsync();
        }

    }
}

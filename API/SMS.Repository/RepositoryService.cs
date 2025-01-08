﻿
namespace SMS.Repository
{
    using Microsoft.EntityFrameworkCore;
    using SMS.Data;
    using SMS.Models;

    public class RepositoryService : IRepositoryService
    {
        private readonly SMSDbContext data;

        public RepositoryService(SMSDbContext data)
        {
            this.data = data;
        }


        public ApplicationUser GetUserById(string id)
        {
            return data.Users.First(u => u.Id.ToString() == id);
        }

        public async Task AddManager(Manager entity)
        {



            data.Managers.Add(entity);

            await data.SaveChangesAsync();
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            return data.Users.First(u => u.Email == email);
        }

        public bool IsManager(string id)
        {

            var manager = data.Managers.FirstOrDefault(x => x.Id.ToString() == id);
            if (manager != null)
            {
                return true;
            }

            return false;
        }
    }
}

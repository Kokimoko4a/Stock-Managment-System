namespace SMS.Services
{
    using SMS.Factory;
    using SMS.Models;
    using SMS.Repository;
    using SMS.Services.Interfaces;

    public class ManagerService : IManagerService
    {
        private readonly IRepositoryService repositoryService;
        private readonly IFactoryService factoryService;

        public ManagerService(IRepositoryService repositoryService, IFactoryService factoryService)
        {
            this.repositoryService = repositoryService;
            this.factoryService = factoryService;
        }

        public async Task BecomeManager(string id)
        {
            if (repositoryService.IsManager(id))
            {
                throw new Exception("You are manager already!");
            }

            ApplicationUser applicationUser = repositoryService.GetUserById(id);

          

            Manager manager = factoryService.CreateManager(applicationUser);

            await repositoryService.AddManager(manager);

        }
    }
}

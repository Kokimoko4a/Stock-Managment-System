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

        public async Task BecomeManager(string email)
        {
            ApplicationUser applicationUser = repositoryService.GetUser(email);


            Manager manager = factoryService.CreateManager(applicationUser);

            await repositoryService.AddManager(manager);

        }
    }
}

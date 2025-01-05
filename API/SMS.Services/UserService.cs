namespace SMS.Services
{
    using SMS.Dtos.User;
    using SMS.Factory;
    using SMS.Models;
    using SMS.Repository;
    using SMS.Services.Interfaces;


    public class UserService : IUserService
    {
        private readonly IFactoryService factory;
        private readonly IRepositoryService repositoryService;

        public UserService(IFactoryService factory, IRepositoryService repositoryService)
        {
            this.factory = factory; 
            this.repositoryService = repositoryService;    
        }

        public ApplicationUser CreateUser(RegisterDTO registerDTO)
        {
           return  factory.CreateUser(registerDTO);
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            return repositoryService.GetUserByEmail(email);
        }
    }
}

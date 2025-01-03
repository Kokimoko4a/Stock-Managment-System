namespace SMS.Services
{
    using SMS.Dtos;
    using SMS.Factory;
    using SMS.Models;
    using SMS.Services.Interfaces;


    public class UserService : IUserService
    {
        private readonly IFactoryService factory;

        public UserService(IFactoryService factory)
        {
            this.factory = factory; 
        }

        public ApplicationUser CreateUser(RegisterDTO registerDTO)
        {
           return  factory.CreateUser(registerDTO);
        }
    }
}

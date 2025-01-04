

namespace SMS.Factory
{
    using SMS.Dtos.User;
    using SMS.Models;
    using SMS.Repository;

    public class FactoryService : IFactoryService
    {
        private IRepositoryService repository;

        public FactoryService(IRepositoryService repository)
        {
            this.repository = repository;
        }


        public ApplicationUser CreateUser(RegisterDTO registerDTO)
        {
            ApplicationUser user = new ApplicationUser()
            {
                LastName = registerDTO.LastName,
                FirstName = registerDTO.FirstName,
                Age = registerDTO.Age,

            };



            return user;
        }


        public Manager CreateManager(ApplicationUser applicationUser)
        {
            Manager manager = new Manager()
            {
                Id = applicationUser.Id
            };

            return manager;
        }



    }
}

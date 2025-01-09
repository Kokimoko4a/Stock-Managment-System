

namespace SMS.Factory
{
    using SMS.Dtos.Company;
    using SMS.Dtos.User;
    using SMS.Models;
    using SMS.Repository;
    using System.Threading.Tasks;

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

        public async Task CreateCompany(CompanyDto companyDto, Manager manager)
        {
            Company company = new Company()
            {
                Description = companyDto.Description,
                Title = companyDto.Title,
                Manager = manager,
                ManagerId = manager.Id
            };


            await repository.AddCompany(company); 
        }
    }
}

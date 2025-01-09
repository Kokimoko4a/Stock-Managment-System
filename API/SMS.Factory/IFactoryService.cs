namespace SMS.Factory
{
    using SMS.Dtos.Company;
    using SMS.Dtos.User;
    using SMS.Models;

    public interface IFactoryService
    {
        public ApplicationUser CreateUser(RegisterDTO registerDTO);

        public Manager CreateManager(ApplicationUser applicationUser);

        public Task CreateCompany(CompanyDto companyDto, Manager manager);
    }
}

namespace SMS.Repository
{
    using SMS.Dtos.Company;
    using SMS.Models;

    public interface IRepositoryService
    {
        public ApplicationUser GetUserById(string id);

        public ApplicationUser GetUserByEmail(string email);

        public Task AddManager(Manager entity);

        public bool IsManager(string id);

        public Manager GetManagerById(Guid managerId);

        public Task AddCompany(Company company);

        public Task<List<CompanySmallExport>> GetAllCompanies(string managerId);

    }
}

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

        public Task<Company> GetCompany(string companyId);

        public Task UpdateCompany(CompanyDtoEditImport company);

        public Task DeleteCompany(string companyId);

        public Task<Company> GetCompanyByTitle(string companyTitle);

        public Task AddTruckDriver(TruckDriver truckDriver);

        public Task AddMachinist(Machinist truckDriver);

        public Task AddPilot(Pilot pilot);

        public Task AddCaptain(Capitan capitan);

        public Task<bool> IsDriver(string userId);

        public Task CreateStock(Stock stock, string companyId);
    }
}

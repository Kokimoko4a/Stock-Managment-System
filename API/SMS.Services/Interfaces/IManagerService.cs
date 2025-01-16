namespace SMS.Services.Interfaces
{
    using SMS.Dtos.Company;
    using SMS.Models;

    public interface IManagerService
    {
        public Task BecomeManager(string email);

        public bool IsManager(string id);

        public Task CreateCompany(string userId, CompanyDto companyDto);

        public Task<List<CompanySmallExport>> GetAllCompanies(string userId);

        public Task<List<CompanySmallExport>> UpdateCompany(CompanyDtoEditImport companyDtoEditImport, string managerId);

        public Task<Company> GetCompany(string companyId);
      


    }
}

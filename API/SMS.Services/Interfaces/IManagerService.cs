using SMS.Dtos.Company;

namespace SMS.Services.Interfaces
{
    public interface IManagerService
    {
        public Task BecomeManager(string email);

        public bool IsManager(string id);

        public Task CreateCompany(string userId, CompanyDto companyDto);
    }
}

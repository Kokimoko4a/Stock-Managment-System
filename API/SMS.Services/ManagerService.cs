namespace SMS.Services
{
    using SMS.Dtos.Company;
    using SMS.Dtos.Vehicles;
    using SMS.Factory;
    using SMS.Models;
    using SMS.Repository;
    using SMS.Services.Interfaces;
    using System.Collections.Generic;

    public class ManagerService : IManagerService
    {
        private readonly IRepositoryService repositoryService;
        private readonly IFactoryService factoryService;

        public ManagerService(IRepositoryService repositoryService, IFactoryService factoryService)
        {
            this.repositoryService = repositoryService;
            this.factoryService = factoryService;
        }

        public async Task BecomeManager(string id)
        {
            if (repositoryService.IsManager(id))
            {
                throw new Exception("You are manager already!");
            }

            ApplicationUser applicationUser = repositoryService.GetUserById(id);



            Manager manager = factoryService.CreateManager(applicationUser);

            await repositoryService.AddManager(manager);

        }

        public bool IsManager(string id)
        {
            return repositoryService.IsManager(id);
        }

        public async Task CreateCompany(string userId, CompanyDto companyDto)
        {


            Manager manager = repositoryService.GetManagerById(Guid.Parse(userId));

            await factoryService.CreateCompany(companyDto, manager);
        }

        public async Task<List<CompanySmallExport>> GetAllCompanies(string userId)
        {
            if (!repositoryService.IsManager(userId))
            {
                return null;
            }

            var companies = await repositoryService.GetAllCompanies(userId);

            if (companies.Any())
            {
                return companies;
            }

            return null;
        }



        public async Task<List<CompanySmallExport>> UpdateCompany(CompanyDtoEditImport companyDtoEditImport, string managerId)
        {
            if (IsManager(managerId))
            {
                Company company = await repositoryService.GetCompany(companyDtoEditImport.Id);

                if (company != null)
                {
                  

                    await repositoryService.UpdateCompany(companyDtoEditImport);

                    return  await GetAllCompanies(managerId);
                }

                return null;

            }

            return null;
        }

        public async Task<Company> GetCompany(string companyId) => await repositoryService.GetCompany(companyId);

        public async Task DeleteCompany(string companyId)
        {
           await repositoryService.DeleteCompany(companyId);
        }

        public async Task<ICollection<VehicleExportDtoWithCoordinates>> GetVehicleExportDtoWithCoordinates(string companyId)
        {
            return await repositoryService.GetVehicleExportDtoWithCoordinates(companyId);
        }
    }
}

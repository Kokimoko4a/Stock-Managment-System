

namespace SMS.Services
{
    using SMS.Dtos.Driver;
    using SMS.Factory;
    using SMS.Repository;
    using SMS.Services.Interfaces;
    using System.Collections.Generic;
    using System.Reflection.Metadata.Ecma335;

    public class DriverService : IDriverService
    {
        private readonly IRepositoryService repositoryService;
        private readonly IFactoryService factoryService;

        public DriverService(IRepositoryService repositoryService, IFactoryService factoryService)
        {
            this.repositoryService = repositoryService; ;
            this.factoryService = factoryService;
        }

        public async Task BecomeDriver(DriverImportDto driverImportDto)
        {
            await factoryService.CreateDriver(driverImportDto);
        }


        public async Task<bool> IsDriver(string userId)
        {
            return await repositoryService.IsDriver(userId);
        }




        public async Task<List<DriverSmallExportDto>> GetDriversForCompany(string companyId)
        {
           return await repositoryService.GetDriversForCompany(companyId);
        }

        public async Task<DriverBigExportDto> GetDetailsForDriverByCompanyId(string driverId)
        {
            return await repositoryService.GetDetailsForDriverByCompanyId(driverId);
        }

        public async Task<DriverDashboardDtoExport> GetDriverDashboardInfoForDriverByDriverId(string driverId)
        {
            return await repositoryService.GetDriverDashboardInfoForDriverByDriverId(driverId); 
        }
    }
}

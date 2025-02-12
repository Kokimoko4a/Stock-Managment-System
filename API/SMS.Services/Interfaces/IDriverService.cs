

namespace SMS.Services.Interfaces
{
    using SMS.Dtos.Driver;

    public interface IDriverService
    {
        public Task BecomeDriver(DriverImportDto driverImportDto);

        public Task<bool> IsDriver(string userId);

        public Task<List<DriverSmallExportDto>> GetDriversForCompany(string companyId);

        public Task<DriverBigExportDto> GetDetailsForDriverByCompanyId(string driverId);

        public Task<DriverDashboardDtoExport> GetDriverDashboardInfoForDriverByDriverId(string driverId);
    }
}

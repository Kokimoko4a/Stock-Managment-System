namespace SMS.Factory
{
    using SMS.Dtos.Company;
    using SMS.Dtos.Driver;
    using SMS.Dtos.Stock;
    using SMS.Dtos.User;
    using SMS.Dtos.Vehicles;
    using SMS.Models;

    public interface IFactoryService
    {
        public ApplicationUser CreateUser(RegisterDTO registerDTO);

        public Manager CreateManager(ApplicationUser applicationUser);

        public Task CreateCompany(CompanyDto companyDto, Manager manager);

        public Task CreateDriver(DriverImportDto driverImportDto);

        public Task CreateStock(StockImportDto stockImportDto);

        public Task CreateVehicle(VehicleDtoImport vehicleDtoImport);
    }
}

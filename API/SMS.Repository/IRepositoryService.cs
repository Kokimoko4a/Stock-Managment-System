namespace SMS.Repository
{
    using SMS.Dtos.Company;
    using SMS.Dtos.Driver;
    using SMS.Dtos.Order;
    using SMS.Dtos.Stock;
    using SMS.Dtos.Vehicles;
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

        public Task<List<SmallStockExportDto>> GetAllStocksForCompany(string companyId);

        public Task<SmallStockExportDto> GetStock(string stockId);

        public Task UpdateStock(SmallStockExportDto stock);

        public Task DeleteStock(string stockId);

        public Task CreateVehicle(Vehicle vehicle, string companyId);

        public Task<List<VehicleDtoExportSmall>> GetAllVehiclesByCompanyId(string companyId);

        public Task<VehicleDtoBigExport> GetDetailedInfoForVehicleById(string vehicleId);

        public Task UpdateVehicle(VehicleDtoImport vehicleDto);

        public Task DeleteVehicle(string vehicleId);

        public Task AssignVehicleToDriver(string driverEmail, string vehicleId);

        public Task CreateTruckOrder(TruckOrder order, string driverEmail, List<string> stocks);

        public Task CreatePlaneOrder(PlaneOrder order,string  driverEmail, List<string> stocks);

        public Task CreateShipOrder(ShipOrder order, string driverEmail, List<string> stocks);

        public Task CreateTrainOrder(TrainOrder order, string driverEmail, List<string> stocks );

        public Task<List<SmallOrderExportDto>> GetOrdersByCompanyId(string companyId);

        public Task<OrderDtoBigExport> GetDetailedOrderById(string orderId);

        public Task<List<DriverSmallExportDto>> GetDriversForCompany(string companyId);

        public Task<DriverBigExportDto> GetDetailsForDriverByCompanyId(string driverId);

        public Task<DriverDashboardDtoExport> GetDriverDashboardInfoForDriverByDriverId(string driverId);

        public Task SetVehicleCoorinates(VehicleCoordinatesDtoImport vehicleCoordinatesDtoImport , string driverId);

        public Task<ICollection< VehicleExportDtoWithCoordinates>> GetVehicleExportDtoWithCoordinates(string companyId);

        public Task MarkOrderAsCompleted(string orderId);
    }
}

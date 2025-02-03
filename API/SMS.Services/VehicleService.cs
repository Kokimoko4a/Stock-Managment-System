namespace SMS.Services
{
    using SMS.Dtos.Vehicles;
    using SMS.Factory;
    using SMS.Repository;
    using SMS.Services.Interfaces;
    using System.Threading.Tasks;

    public class VehicleService : IVehicleService
    {
        private readonly IFactoryService factoryService;
        private readonly IRepositoryService repositoryService;

        public VehicleService(IRepositoryService repositoryService, IFactoryService factoryService)
        {
            this.repositoryService = repositoryService;
            this.factoryService = factoryService;
        }

        public async Task CreateVehicle(VehicleDtoImport vehicleDtoImport)
        {
            await factoryService.CreateVehicle(vehicleDtoImport);
        }
    }
}

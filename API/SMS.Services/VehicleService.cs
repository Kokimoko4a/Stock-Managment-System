namespace SMS.Services
{
    using SMS.Dtos.Vehicles;
    using SMS.Factory;
    using SMS.Repository;
    using SMS.Services.Interfaces;
    using System.Collections.Generic;
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

        public async Task<List<VehicleDtoExportSmall>> GetAllVehiclesByCompanyId(string companyId)
        {
            return await repositoryService.GetAllVehiclesByCompanyId(companyId);
        }

        public async Task<VehicleDtoBigExport> GetDetailedInfoForVehicleById(string vehicleId)
        {
          return  await repositoryService.GetDetailedInfoForVehicleById(vehicleId);
        }


        public async Task UpdateVehicle(VehicleDtoImport vehicleDto)
        {
            await repositoryService.UpdateVehicle(vehicleDto);
        }


    
    }
}

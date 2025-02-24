﻿using SMS.Dtos.Vehicles;

namespace SMS.Services.Interfaces
{
    public interface IVehicleService
    {
        public Task CreateVehicle(VehicleDtoImport vehicleDtoImport);

        public Task<List<VehicleDtoExportSmall>> GetAllVehiclesByCompanyId(string companyId);

        public Task<VehicleDtoBigExport> GetDetailedInfoForVehicleById(string vehicleId);

        public Task UpdateVehicle(VehicleDtoImport vehicleDto);

        public Task DeleteVehicle(string vehicleId);

        public Task AssignVehicleToDriver(string driverEmail, string vehicleId);

        public Task SetVehicleCoorinates(VehicleCoordinatesDtoImport vehicleCoordinatesDtoImport, string driverId);


    }
}

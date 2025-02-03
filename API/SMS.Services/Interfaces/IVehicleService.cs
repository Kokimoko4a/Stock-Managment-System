using SMS.Dtos.Vehicles;

namespace SMS.Services.Interfaces
{
    public interface IVehicleService
    {
        public Task CreateVehicle(VehicleDtoImport vehicleDtoImport);
    }
}

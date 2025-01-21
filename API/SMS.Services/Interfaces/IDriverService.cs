

namespace SMS.Services.Interfaces
{
    using SMS.Dtos.Driver;

    public interface IDriverService
    {
        public Task BecomeDriver(DriverImportDto driverImportDto);
    }
}

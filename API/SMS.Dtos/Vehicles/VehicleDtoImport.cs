

namespace SMS.Dtos.Vehicles
{
    using Microsoft.AspNetCore.Http;

    public class VehicleDtoImport
    {
        public string Id { get; set; }

        public string Brand { get; set; }


        public string Model { get; set; }


        public string RegistrationNumber { get; set; } = null!;


        public double TravelledKm { get; set; }


        public string Type { get; set; }


        public double LoadCapacity { get; set; }


        public double ReservoirCapacity { get; set; }

        public string CompanyId { get; set; }

        public IFormFile Image { get; set; }

    }
}

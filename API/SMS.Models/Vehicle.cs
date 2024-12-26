namespace SMS.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class Vehicle
    {
        protected Vehicle(string regNumber, int type, double loadCapacity, double reservoirCapacity)
        {
            Id = Guid.NewGuid();
            IsDriving = false;

            RegistrationNumber = regNumber;
            Type = (TypeOfVehicle)type;
            LoadCapacity = loadCapacity;
            ReservoirCapacity = reservoirCapacity;
            Stocks = new HashSet<Stock>();
        }

        [Key]
        public Guid Id { get; set; }

        public string RegistrationNumber { get; set; } = null!;

        public double TravelledKm { get; set; }

        public TypeOfVehicle Type { get; set; }

        public double LoadCapacity { get; set; }

        public bool IsDriving { get; set; }

        public double Latitude { get; set; }

        public double Longtitude { get; set; }

        public double ReservoirCapacity { get; set; }


        [ForeignKey(nameof(Driver))]
        public Guid DriverId { get; set; }

        public Driver Driver { get; set; }


        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }

        public Company Company { get; set; }

        public ICollection<Stock> Stocks { get; set; }

    }
}

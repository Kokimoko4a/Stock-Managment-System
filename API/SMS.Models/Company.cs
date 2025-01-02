namespace SMS.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static SMS.Common.GeneralValidationConstants.Company;

    public class Company
    {
        public Company()
        {
            Trucks = new HashSet<Truck>();  
            Planes = new HashSet<Plane>();  
            Ships = new HashSet<Ship>();  
            Trains = new HashSet<Train>();  
            Stocks = new HashSet<Stock>();
            TruckDrivers = new HashSet<TruckDriver>();
            Pilots = new HashSet<Pilot>();
            Machinists = new HashSet<Machinist>();
            Capitans = new HashSet<Capitan>();
            TruckOrders = new HashSet<TruckOrder>();
            PlaneOrders = new HashSet<PlaneOrder>();
            ShipOrders = new HashSet<ShipOrder>();
            TrainOrders = new HashSet<TrainOrder>();
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(TitleMaxLenth, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [ForeignKey(nameof(Manager))]
        public Guid ManagerId { get; set; }

        public Manager Manager { get; set; }

        public ICollection<Train> Trains { get; set; }
        public ICollection<Plane> Planes { get; set; }
        public ICollection<Truck> Trucks { get; set; }
        public ICollection<Ship> Ships { get; set; }


        public ICollection<Stock> Stocks { get; set; }

        public ICollection<TruckDriver> TruckDrivers { get; set; }

        public ICollection<Pilot> Pilots { get; set; }

        public ICollection<Capitan> Capitans { get; set; }

        public ICollection<Machinist> Machinists { get; set; }

        public ICollection<ShipOrder> ShipOrders { get; set; }

        public ICollection<TrainOrder> TrainOrders { get; set; }

        public ICollection<PlaneOrder> PlaneOrders { get; set; }

        public ICollection<TruckOrder> TruckOrders { get; set; }
    }
}

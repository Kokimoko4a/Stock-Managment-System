

namespace SMS.Dtos.Company
{

    using SMS.Models;

    public class CompanyBigDto
    {
        public CompanyBigDto()
        {
            Trains = new HashSet<Train>();
            Planes = new HashSet<Plane>();
            Trucks = new HashSet<Truck>();
            Ships = new HashSet<Ship>();
            Stocks = new HashSet<Stock>();
            TruckDrivers = new HashSet<TruckDriver>();
            Pilots = new HashSet<Pilot>();
            Capitans = new HashSet<Capitan>();
            Machinists = new HashSet<Machinist>();
            ShipOrders = new HashSet<ShipOrder>();
            TrainOrders = new HashSet<TrainOrder>();
            PlaneOrders = new HashSet<PlaneOrder>();
            TruckOrders = new HashSet<TruckOrder>();
        }


        public string Id { get; set; }


        public string Title { get; set; } = null!;


        public string Description { get; set; } = null!;


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

namespace SMS.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Company
    {
        public Company()
        {
            Vehicles = new HashSet<Vehicle>();  
            Stocks = new HashSet<Stock>();
            Drivers = new HashSet<Driver>();
            Orders = new HashSet<Order>();
        }

        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        [ForeignKey(nameof(Manager))]
        public Guid ManagerId { get; set; }

        public Manager Manager { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }

        public ICollection<Stock> Stocks { get; set; }

        public ICollection<Driver> Drivers { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}

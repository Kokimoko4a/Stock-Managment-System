namespace SMS.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using static SMS.Common.ApplicationConstants;
    public class Truck : Vehicle
    {
        public Truck() : base("11111", TruckCode, 300, 300.0) // Default values to call the base constructor
        {
        }

        public Truck(string regNumber, int type, double loadCapacity, double reservoirCapacity) : base(regNumber, TruckCode, loadCapacity, reservoirCapacity)
        {
        }

        [ForeignKey(nameof(Driver))]
        public Guid DriverId { get; set; }

        public TruckDriver Driver { get; set; }


        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }

        public Company Company { get; set; }

        public TruckOrder Order { get; set; }

        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }
    }
}

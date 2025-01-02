namespace SMS.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using static SMS.Common.ApplicationConstants;

    public class Plane : Vehicle
    {
        public Plane() : base("11111", PlaneCode, 300, 300.0) // Default values to call the base constructor
        {
        }

        public Plane(string regNumber, int type, double loadCapacity, double reservoirCapacity) : base(regNumber, PlaneCode, loadCapacity, reservoirCapacity)
        {
        }


        [ForeignKey(nameof(Pilot))]
        public Guid DriverId { get; set; }

        public Pilot Pilot { get; set; }


        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }

        public Company Company { get; set; }

        public PlaneOrder Order { get; set; }

        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }
    }
}

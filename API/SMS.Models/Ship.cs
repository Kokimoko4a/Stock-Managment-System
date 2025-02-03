namespace SMS.Models
{
    using Microsoft.EntityFrameworkCore.Metadata.Conventions;
    using System.ComponentModel.DataAnnotations.Schema;
    using static SMS.Common.ApplicationConstants;

    public class Ship : Vehicle
    {
        public Ship() : base("11111", ShipCode, 300, 300.0) // Default values to call the base constructor
        {
        }

        public Ship(string regNumber, int type, double loadCapacity, double reservoirCapacity) : base(regNumber, ShipCode, loadCapacity, reservoirCapacity)
        {
        }

        [ForeignKey(nameof(Driver))]
        public Guid? DriverId { get; set; }

        public Capitan? Driver { get; set; }


        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }

        public Company Company { get; set; }

        public ShipOrder? Order { get; set; }

        [ForeignKey(nameof(Order))]
        public Guid? OrderId { get; set; }
    }
}

namespace SMS.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TruckDriver
    {
        public TruckDriver()
        {
           
        }

        [Key]
        [Required]
        public Guid Id { get; set; }


        [ForeignKey(nameof(Vehicle))]
        public Guid? VehicleId { get; set; }


        public Truck? Vehicle { get; set; }


        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }


        public Company Company { get; set; }

        public TruckOrder? Order { get; set; }

        [ForeignKey(nameof(Order))]
        public Guid? OrderId { get; set; }


    }
}

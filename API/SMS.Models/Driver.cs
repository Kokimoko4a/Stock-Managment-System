namespace SMS.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Driver
    {
        public Driver()
        {
        
        }

        [Key]
        [Required]
        public Guid Id { get; set; }


        [ForeignKey(nameof(Vehicle))]
        public Guid VehicleId { get; set; }


        public Vehicle Vehicle { get; set; }


        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }


        public Company Company { get; set; }

        public Order Order { get; set; }

        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }


    }
}

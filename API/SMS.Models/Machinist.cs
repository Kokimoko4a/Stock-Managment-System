
namespace SMS.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;


    public class Machinist
    {
        [Key]
        [Required]
        public Guid Id { get; set; }


        [ForeignKey(nameof(Vehicle))]
        public Guid VehicleId { get; set; }


        public Train Vehicle { get; set; }


        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }


        public Company Company { get; set; }

        public TrainOrder Order { get; set; }

        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }
    }
}

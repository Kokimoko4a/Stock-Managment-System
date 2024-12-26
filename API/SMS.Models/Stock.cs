using System.ComponentModel.DataAnnotations;

namespace SMS.Models
{
    public class Stock
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = null!:

        [Required]
        public string Description { get; set; } = null!;

        public TypeOfVehicle PreferedTypeOfTransportId { get; set; }

        [Required]
        public double Gauge { get; set; }

       
    }
}



namespace SMS.Models
{

    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using static SMS.Common.GeneralValidationConstants.Order;

    public class ShipOrder
    {
        public ShipOrder()
        {
            Stocks = new HashSet<Stock>();
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }

        public ICollection<Stock> Stocks { get; set; }

        [ForeignKey(nameof(Driver))]
        public Guid DriverId { get; set; }

        public Capitan? Driver { get; set; }

        [ForeignKey(nameof(Company))]
        public Guid ComapanyId { get; set; }

        public Company Company { get; set; }

        public Ship? Vehicle { get; set; }

        [ForeignKey(nameof(VehicleId))]
        public Guid VehicleId { get; set; }

        [Required]
        [StringLength(DestinationMaxLength, MinimumLength = DestinationMinLength)]
        public string Destination { get; set; }

        [Required]
        [StringLength(StartPointMaxLength, MinimumLength = StartPointMinLength)]
        public string StartPoint { get; set; }


        public bool IsCompleted { get; set; }
    }
}

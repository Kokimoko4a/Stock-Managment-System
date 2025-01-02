namespace SMS.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static SMS.Common.GeneralValidationConstants.Order;

    public class TrainOrder
    {
        public TrainOrder()
        {
            Stocks = new HashSet<Stock>();
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public ICollection<Stock> Stocks { get; set; }

        [ForeignKey(nameof(Driver))]
        public Guid DriverId { get; set; }

        public Machinist Driver { get; set; }

        [ForeignKey(nameof(Company))]
        public Guid ComapanyId { get; set; }

        public Company Company { get; set; }

        public Train Vehicle { get; set; }

        [ForeignKey(nameof(VehicleId))]
        public Guid VehicleId { get; set; }

        [Required]
        [StringLength(DestinationMaxLength, MinimumLength = DestinationMinLength)]
        public string Destination { get; set; }

        [Required]
        [StringLength(StartPointMaxLength, MinimumLength = StartPointMinLength)]
        public string StartPoint { get; set; }

    }
}

namespace SMS.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static SMS.Common.GeneralValidationConstants.Stock; 

    public class Stock
    {
        public Stock()
        {
            Id = Guid.NewGuid();    
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(TitleMaxLenth, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        public TypeOfVehicle PreferedTypeOfTransportId { get; set; }

        [Required]
        [Range(GaugeMinValue,GaugeMaxValue)]
        public double Gauge { get; set; }

     /*   [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }*/

        //public Order Order { get; set; }
    }
}

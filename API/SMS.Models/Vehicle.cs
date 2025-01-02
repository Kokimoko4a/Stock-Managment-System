namespace SMS.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static SMS.Common.GeneralValidationConstants.Vehicle;

    public abstract class Vehicle
    {
        protected Vehicle(string regNumber, int type, double loadCapacity, double reservoirCapacity)
        {
            Id = Guid.NewGuid();
            IsDriving = false;

            RegistrationNumber = regNumber;
            Type = (TypeOfVehicle)type;
            LoadCapacity = loadCapacity;
            ReservoirCapacity = reservoirCapacity;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(RegistrationNumberMaxLength,MinimumLength = RegistrationNumberMinLength)]
        public string RegistrationNumber { get; set; } = null!;

        [Range(TravelledKmMinValue,TraveledKmMaxValue)]
        public double TravelledKm { get; set; }

        public TypeOfVehicle Type { get; set; }

        [Required]
        [Range(LoadCapacityMinValue,LoadCapacityMaxValue)]
        public double LoadCapacity { get; set; }

        public bool IsDriving { get; set; }

        [Range(LatitudeMinValue,LatitudeMaxValue)]
        public double Latitude { get; set; }

        [Range(LongtitudeMinValue, LongtitudeMaxValue)]
        public double Longtitude { get; set; }

        [Required]
        [Range(ReservoirCapacityMinValue, ReservoirCapacityMaxValue)]
        public double ReservoirCapacity { get; set; }


    }
}

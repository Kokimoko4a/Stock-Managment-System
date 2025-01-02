namespace SMS.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using static SMS.Common.ApplicationConstants;

    public class Train : Vehicle
    {
        public Train() : base("11111", TrainCode, 300, 300.0) // Default values to call the base constructor
        {
        }

        public Train(string regNumber, int type, double loadCapacity, double reservoirCapacity) : base(regNumber, TrainCode, loadCapacity, reservoirCapacity)
        {
        }

        [ForeignKey(nameof(Driver))]
        public Guid DriverId { get; set; }

        public Machinist Driver { get; set; }


        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }

        public Company Company { get; set; }

        public TrainOrder Order { get; set; }

        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }
    }
}

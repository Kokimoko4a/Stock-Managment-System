namespace SMS.Models
{
    using static SMS.Common.ApplicationConstants;
    public class Truck : Vehicle
    {
        public Truck(string regNumber, int type, double loadCapacity, double reservoirCapacity) : base(regNumber, TruckCode, loadCapacity, reservoirCapacity)
        {
        }
    }
}

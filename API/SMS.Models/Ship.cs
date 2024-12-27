namespace SMS.Models
{
    using static SMS.Common.ApplicationConstants;

    public class Ship : Vehicle
    {
        public Ship(string regNumber, int type, double loadCapacity, double reservoirCapacity) : base(regNumber, ShipCode, loadCapacity, reservoirCapacity)
        {
        }
    }
}

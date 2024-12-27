namespace SMS.Models
{
    using static SMS.Common.ApplicationConstants;

    public class Plane : Vehicle
    {
        public Plane(string regNumber, int type, double loadCapacity, double reservoirCapacity) : base(regNumber, PlaneCode, loadCapacity, reservoirCapacity)
        {
        }
    }
}

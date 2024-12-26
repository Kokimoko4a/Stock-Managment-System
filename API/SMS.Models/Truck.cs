namespace SMS.Models
{
    public class Truck : Vehicle
    {
        public Truck(string regNumber, int type, double loadCapacity, double reservoirCapacity) : base(regNumber, type, loadCapacity, reservoirCapacity)
        {
        }
    }
}

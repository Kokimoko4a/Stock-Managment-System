namespace SMS.Models
{
    public class Ship : Vehicle
    {
        public Ship(string regNumber, int type, double loadCapacity, double reservoirCapacity) : base(regNumber, type, loadCapacity, reservoirCapacity)
        {
        }
    }
}

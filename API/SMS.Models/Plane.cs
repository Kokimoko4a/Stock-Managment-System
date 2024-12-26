namespace SMS.Models
{
    public class Plane : Vehicle
    {
        public Plane(string regNumber, int type, double loadCapacity, double reservoirCapacity) : base(regNumber, type, loadCapacity, reservoirCapacity)
        {
        }
    }
}

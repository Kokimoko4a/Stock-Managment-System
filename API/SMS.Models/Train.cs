namespace SMS.Models
{
    public class Train : Vehicle
    {
        public Train(string regNumber, int type, double loadCapacity, double reservoirCapacity) : base(regNumber, type, loadCapacity, reservoirCapacity)
        {
        }
    }
}

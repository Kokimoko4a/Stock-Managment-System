namespace SMS.Models
{
    using static SMS.Common.ApplicationConstants;

    public class Train : Vehicle
    {
        public Train(string regNumber, int type, double loadCapacity, double reservoirCapacity) : base(regNumber, TrainCode, loadCapacity, reservoirCapacity)
        {
        }
    }
}

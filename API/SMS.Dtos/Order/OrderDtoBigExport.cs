using SMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SMS.Dtos.Order
{
    public class OrderDtoBigExport
    {

        public Guid Id { get; set; }



        public string Title { get; set; }



        public string Description { get; set; }



        public List<string> Stocks { get; set; }


        public string TypeTransport { get; set; }



        public string DriverNames { get; set; }



        public string DriverEmail { get; set; }



        public string VehicleBrand { get; set; }


        public string VehicleModel { get; set; }



        public string Destination { get; set; }



        public string StartPoint { get; set; }
    }
}

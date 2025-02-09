using SMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SMS.Dtos.Driver
{
    public class DriverBigExportDto
    {
        
        public Guid Id { get; set; }


        public string Names { get; set; }

 
        public int Age { get; set; }

        public string Email { get; set; }


        public string VehicleInfo { get; set; }


        public string  OrderInfo { get; set; }
    }
}

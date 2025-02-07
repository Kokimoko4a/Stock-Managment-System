using SMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SMS.Dtos.Order
{
    public class SmallOrderExportDto
    {
        [Key]
        public Guid Id { get; set; }



        public string Title { get; set; }


        public string  TypeTransport { get; set; }


        public string Vehicle { get; set; }


        public string Destination { get; set; }


        public string StartPoint { get; set; }

    }
}

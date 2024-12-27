using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Models
{
    public class Order
    {
        public Order()
        {
            Stocks = new HashSet<Stock>();
        }

        [Key]
        public Guid Id { get; set; }

        public ICollection<Stock> Stocks { get; set; }

        [ForeignKey(nameof(Driver))]
        public Guid DriverId { get; set; }

        public Driver Driver { get; set; }

        [ForeignKey(nameof(Company))]
        public Guid ComapanyId { get; set; }

        public Company Company { get; set; }


        public string Destination { get; set; }


        public string StartPoint { get; set; }

    }
}

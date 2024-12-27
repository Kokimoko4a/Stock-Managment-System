using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace SMS.Models
{
    public class Manager
    {
        public Manager()
        {
            Companies = new HashSet<Company>();
        }

        [Key]
        public Guid Id { get; set; }

        public ICollection<Company> Companies { get; set; }


    }
}

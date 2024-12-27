namespace SMS.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
           
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }


    }
}

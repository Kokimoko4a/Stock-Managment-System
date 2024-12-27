namespace SMS.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class SMSDbContext : IdentityDbContext<IdentityUser>
    {
        public SMSDbContext(DbContextOptions<SMSDbContext> options) : base(options)
        {
        }

    }
}

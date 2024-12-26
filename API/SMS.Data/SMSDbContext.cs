namespace SMS.Data
{
    using Microsoft.EntityFrameworkCore;
    using System;

    public class SMSDbContext : DbContext
    {
        public SMSDbContext(DbContextOptions<SMSDbContext> options) : base(options)
        {
        }

    }
}

namespace SMS.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System;
    using SMS.Models;
    using System.Reflection;
    using System.Reflection.Emit;

    public class SMSDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public SMSDbContext(DbContextOptions<SMSDbContext> options) : base(options)
        {
            if (Database.IsRelational())
            {
                Database.EnsureCreated();
            }
        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Capitan> Capitans { get; set; }

        public DbSet<Machinist> Machinists { get; set; }

        public DbSet<TruckDriver> TruckDrivers { get; set; }

        public DbSet<Pilot> Pilots { get; set; }

        public DbSet<Manager> Managers { get; set; }

        public DbSet<TruckOrder> TruckOrders { get; set; }

        public DbSet<PlaneOrder> PlaneOrders { get; set; }

        public DbSet<ShipOrder> ShipOrders { get; set; }

        public DbSet<TrainOrder> TrainOrders { get; set; }

        public DbSet<Plane> Planes { get; set; }

        public DbSet<Ship> Ships { get; set; }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Train> Trains { get; set; }

        public DbSet<Truck> Trucks { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Plane>()
     .HasKey(p => p.Id);

            builder.Entity<Pilot>()
                .HasKey(p => p.Id);

            builder.Entity<Ship>()
         .HasKey(p => p.Id);

            builder.Entity<Capitan>()
                .HasKey(p => p.Id);

            builder.Entity<Machinist>()
.HasKey(p => p.Id);

            builder.Entity<Train>()
                .HasKey(p => p.Id);

            builder.Entity<Truck>()
.HasKey(p => p.Id);

            builder.Entity<TruckDriver>()
                .HasKey(p => p.Id);


            builder.Entity<TruckDriver>()
           .HasOne(d => d.Order)
           .WithOne(o => o.Driver)
           .HasForeignKey<TruckOrder>(o => o.DriverId)
           .OnDelete(DeleteBehavior.Restrict);



            builder.Entity<TruckDriver>()
           .HasOne(d => d.Vehicle)        // Driver has one Vehicle
           .WithOne(v => v.Driver)         // Vehicle has one Driver
           .HasForeignKey<Truck>(v => v.DriverId)  // Foreign key on Vehicle
           .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Truck>()
           .HasOne(v => v.Order)        // A Vehicle has one Order
           .WithOne(o => o.Vehicle)     // An Order has one Vehicle
           .HasForeignKey<TruckOrder>(o => o.VehicleId)  // Foreign key in Order table
           .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TruckOrder>()
           .HasOne(o => o.Vehicle)
           .WithOne(v => v.Order)
           .HasForeignKey<TruckOrder>(o => o.VehicleId)
           .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<TruckDriver>()
         .HasOne(d => d.Order)
         .WithOne(o => o.Driver)
         .HasForeignKey<TruckOrder>(o => o.DriverId)
         .OnDelete(DeleteBehavior.Restrict);







            builder.Entity<Pilot>()
          .HasOne(d => d.Order)
          .WithOne(o => o.Driver)
          .HasForeignKey<PlaneOrder>(o => o.DriverId)
          .OnDelete(DeleteBehavior.Restrict);



            builder.Entity<Pilot>()
           .HasOne(d => d.Vehicle)        // Driver has one Vehicle
           .WithOne(v => v.Pilot)         // Vehicle has one Driver
           .HasForeignKey<Plane>(v => v.DriverId)  // Foreign key on Vehicle
           .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Plane>()
           .HasOne(v => v.Order)        // A Vehicle has one Order
           .WithOne(o => o.Vehicle)     // An Order has one Vehicle
           .HasForeignKey<PlaneOrder>(o => o.VehicleId)  // Foreign key in Order table
           .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PlaneOrder>()
           .HasOne(o => o.Vehicle)
           .WithOne(v => v.Order)
           .HasForeignKey<PlaneOrder>(o => o.VehicleId)
           .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Pilot>()
         .HasOne(d => d.Order)
         .WithOne(o => o.Driver)
         .HasForeignKey<PlaneOrder>(o => o.DriverId)
         .OnDelete(DeleteBehavior.Restrict);

















            builder.Entity<Capitan>()
          .HasOne(d => d.Order)
          .WithOne(o => o.Driver)
          .HasForeignKey<ShipOrder>(o => o.DriverId)
          .OnDelete(DeleteBehavior.Restrict);



            builder.Entity<Capitan>()
           .HasOne(d => d.Vehicle)        // Driver has one Vehicle
           .WithOne(v => v.Driver)         // Vehicle has one Driver
           .HasForeignKey<Ship>(v => v.DriverId)  // Foreign key on Vehicle
           .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Ship>()
           .HasOne(v => v.Order)        // A Vehicle has one Order
           .WithOne(o => o.Vehicle)     // An Order has one Vehicle
           .HasForeignKey<ShipOrder>(o => o.VehicleId)  // Foreign key in Order table
           .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ShipOrder>()
           .HasOne(o => o.Vehicle)
           .WithOne(v => v.Order)
           .HasForeignKey<ShipOrder>(o => o.VehicleId)
           .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Capitan>()
         .HasOne(d => d.Order)
         .WithOne(o => o.Driver)
         .HasForeignKey<ShipOrder>(o => o.DriverId)
         .OnDelete(DeleteBehavior.Restrict);












            builder.Entity<Machinist>()
          .HasOne(d => d.Order)
          .WithOne(o => o.Driver)
          .HasForeignKey<TrainOrder>(o => o.DriverId)
          .OnDelete(DeleteBehavior.Restrict);



            builder.Entity<Machinist>()
           .HasOne(d => d.Vehicle)        // Driver has one Vehicle
           .WithOne(v => v.Driver)         // Vehicle has one Driver
           .HasForeignKey<Train>(v => v.DriverId)  // Foreign key on Vehicle
           .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Train>()
           .HasOne(v => v.Order)        // A Vehicle has one Order
           .WithOne(o => o.Vehicle)     // An Order has one Vehicle
           .HasForeignKey<TrainOrder>(o => o.VehicleId)  // Foreign key in Order table
           .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TrainOrder>()
           .HasOne(o => o.Vehicle)
           .WithOne(v => v.Order)
           .HasForeignKey<TrainOrder>(o => o.VehicleId)
           .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Machinist>()
         .HasOne(d => d.Order)
         .WithOne(o => o.Driver)
         .HasForeignKey<TrainOrder>(o => o.DriverId)
         .OnDelete(DeleteBehavior.Restrict);


        }
    }
}

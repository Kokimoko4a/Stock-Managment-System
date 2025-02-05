﻿using SMS.Models;
using System.ComponentModel.DataAnnotations;

namespace SMS.Dtos.Vehicles
{
    public class VehicleDtoBigExport
    {
        
        public string Id { get; set; }


        public string Brand { get; set; }

        public string Model { get; set; }


        public string RegistrationNumber { get; set; } = null!;


        public double TravelledKm { get; set; }


        public double LoadCapacity { get; set; }

        public bool IsDriving { get; set; }

        public double ReservoirCapacity { get; set; }

        public string ImagePath { get; set; }
    }
}

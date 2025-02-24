﻿
namespace SMS.Dtos.Driver
{
    public class DriverDashboardDtoExport
    {
        public DriverDashboardDtoExport()
        {
            Destination = "None";
            StartPoint = "None";
            OrderProducts = "None";
            OrderTitle = "None";
        }

        public string Id { get; set; }

        public string VehicleBrand { get; set; }

        public string VehicleModel { get; set; }

        public string OrderTitle { get; set; }

        public string OrderProducts { get; set; }

        public string StartPoint { get; set; }

        public string Destination { get; set; }
    }
}

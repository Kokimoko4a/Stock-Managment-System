using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMS.Data.Migrations
{
    public partial class LetsTryAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaneOrders_Pilots_DriverId",
                table: "PlaneOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaneOrders_Planes_VehicleId",
                table: "PlaneOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Planes_Pilots_DriverId",
                table: "Planes");

            migrationBuilder.DropForeignKey(
                name: "FK_ShipOrders_Capitans_DriverId",
                table: "ShipOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ShipOrders_Ships_VehicleId",
                table: "ShipOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Ships_Capitans_DriverId",
                table: "Ships");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainOrders_Machinists_DriverId",
                table: "TrainOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainOrders_Trains_VehicleId",
                table: "TrainOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Trains_Machinists_DriverId",
                table: "Trains");

            migrationBuilder.DropForeignKey(
                name: "FK_TruckOrders_TruckDrivers_DriverId",
                table: "TruckOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_TruckOrders_Trucks_VehicleId",
                table: "TruckOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Trucks_TruckDrivers_DriverId",
                table: "Trucks");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaneOrders_Pilots_DriverId",
                table: "PlaneOrders",
                column: "DriverId",
                principalTable: "Pilots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaneOrders_Planes_VehicleId",
                table: "PlaneOrders",
                column: "VehicleId",
                principalTable: "Planes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Planes_Pilots_DriverId",
                table: "Planes",
                column: "DriverId",
                principalTable: "Pilots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShipOrders_Capitans_DriverId",
                table: "ShipOrders",
                column: "DriverId",
                principalTable: "Capitans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShipOrders_Ships_VehicleId",
                table: "ShipOrders",
                column: "VehicleId",
                principalTable: "Ships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_Capitans_DriverId",
                table: "Ships",
                column: "DriverId",
                principalTable: "Capitans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainOrders_Machinists_DriverId",
                table: "TrainOrders",
                column: "DriverId",
                principalTable: "Machinists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainOrders_Trains_VehicleId",
                table: "TrainOrders",
                column: "VehicleId",
                principalTable: "Trains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trains_Machinists_DriverId",
                table: "Trains",
                column: "DriverId",
                principalTable: "Machinists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TruckOrders_TruckDrivers_DriverId",
                table: "TruckOrders",
                column: "DriverId",
                principalTable: "TruckDrivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TruckOrders_Trucks_VehicleId",
                table: "TruckOrders",
                column: "VehicleId",
                principalTable: "Trucks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trucks_TruckDrivers_DriverId",
                table: "Trucks",
                column: "DriverId",
                principalTable: "TruckDrivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaneOrders_Pilots_DriverId",
                table: "PlaneOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaneOrders_Planes_VehicleId",
                table: "PlaneOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Planes_Pilots_DriverId",
                table: "Planes");

            migrationBuilder.DropForeignKey(
                name: "FK_ShipOrders_Capitans_DriverId",
                table: "ShipOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ShipOrders_Ships_VehicleId",
                table: "ShipOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Ships_Capitans_DriverId",
                table: "Ships");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainOrders_Machinists_DriverId",
                table: "TrainOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainOrders_Trains_VehicleId",
                table: "TrainOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Trains_Machinists_DriverId",
                table: "Trains");

            migrationBuilder.DropForeignKey(
                name: "FK_TruckOrders_TruckDrivers_DriverId",
                table: "TruckOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_TruckOrders_Trucks_VehicleId",
                table: "TruckOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Trucks_TruckDrivers_DriverId",
                table: "Trucks");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaneOrders_Pilots_DriverId",
                table: "PlaneOrders",
                column: "DriverId",
                principalTable: "Pilots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaneOrders_Planes_VehicleId",
                table: "PlaneOrders",
                column: "VehicleId",
                principalTable: "Planes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Planes_Pilots_DriverId",
                table: "Planes",
                column: "DriverId",
                principalTable: "Pilots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShipOrders_Capitans_DriverId",
                table: "ShipOrders",
                column: "DriverId",
                principalTable: "Capitans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShipOrders_Ships_VehicleId",
                table: "ShipOrders",
                column: "VehicleId",
                principalTable: "Ships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_Capitans_DriverId",
                table: "Ships",
                column: "DriverId",
                principalTable: "Capitans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainOrders_Machinists_DriverId",
                table: "TrainOrders",
                column: "DriverId",
                principalTable: "Machinists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainOrders_Trains_VehicleId",
                table: "TrainOrders",
                column: "VehicleId",
                principalTable: "Trains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trains_Machinists_DriverId",
                table: "Trains",
                column: "DriverId",
                principalTable: "Machinists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TruckOrders_TruckDrivers_DriverId",
                table: "TruckOrders",
                column: "DriverId",
                principalTable: "TruckDrivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TruckOrders_Trucks_VehicleId",
                table: "TruckOrders",
                column: "VehicleId",
                principalTable: "Trucks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trucks_TruckDrivers_DriverId",
                table: "Trucks",
                column: "DriverId",
                principalTable: "TruckDrivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

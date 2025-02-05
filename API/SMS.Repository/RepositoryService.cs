﻿
namespace SMS.Repository
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using SMS.Data;
    using SMS.Dtos.Company;
    using SMS.Dtos.Stock;
    using SMS.Dtos.Vehicles;
    using SMS.Models;
    using System.Collections.Generic;

    public class RepositoryService : IRepositoryService
    {
        private readonly SMSDbContext data;

        public RepositoryService(SMSDbContext data)
        {
            this.data = data;
        }


        public ApplicationUser GetUserById(string id)
        {
            return data.Users.First(u => u.Id.ToString() == id);
        }

        public async Task AddManager(Manager entity)
        {



            data.Managers.Add(entity);

            await data.SaveChangesAsync();
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            return data.Users.First(u => u.Email == email);
        }

        public bool IsManager(string id)
        {

            var manager = data.Managers.FirstOrDefault(x => x.Id.ToString() == id);
            if (manager != null)
            {
                return true;
            }

            return false;
        }

        public Manager GetManagerById(Guid managerId) => data.Managers.FirstOrDefault(x => x.Id == managerId);

        public async Task AddCompany(Company company)
        {


            // Save all changes


            try
            {
                Manager manager = company.Manager;
                manager.Companies.Add(company);

                // Add the company to the DbSet
                await data.AddAsync(company);

                await data.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }


        }

        public async Task<List<CompanySmallExport>> GetAllCompanies(string managerId)

            => await data.Companies.Where(x => x.ManagerId.ToString() == managerId).Select(x => new CompanySmallExport()
            {
                Id = x.Id.ToString(),
                Title = x.Title
            }).ToListAsync();



        public async Task<Company> GetCompany(string companyId) => await data.Companies.FirstOrDefaultAsync(x => x.Id.ToString() == companyId);

        public async Task UpdateCompany(CompanyDtoEditImport companyDtoEditImport)
        {
            Company company = await GetCompany(companyDtoEditImport.Id);

            company.Title = companyDtoEditImport.Title;
            company.Description = companyDtoEditImport.Description;

            await data.SaveChangesAsync();


        }

        public async Task DeleteCompany(string companyId)
        {
            var company = await data.Companies.FirstAsync(x => x.Id.ToString() == companyId);

            data.Companies.Remove(company);

            await data.SaveChangesAsync();
        }

        public async Task<Company> GetCompanyByTitle(string companyTitle)
        {
            return await data.Companies.FirstOrDefaultAsync(x => x.Title == companyTitle);
        }

        public async Task AddTruckDriver(TruckDriver truckDriver)
        {
            data.Add(truckDriver);

            var company = await GetCompany(truckDriver.CompanyId.ToString());

            company.TruckDrivers.Add(truckDriver);

            await data.SaveChangesAsync();
        }

        public async Task AddMachinist(Machinist machinist)
        {
            data.Add(machinist);

            var company = await GetCompany(machinist.CompanyId.ToString());

            company.Machinists.Add(machinist);

            await data.SaveChangesAsync();
        }

        public async Task AddPilot(Pilot pilot)
        {
            data.Add(pilot);

            var company = await GetCompany(pilot.CompanyId.ToString());

            company.Pilots.Add(pilot);

            await data.SaveChangesAsync();
        }

        public async Task AddCaptain(Capitan capitan)
        {
            data.Add(capitan);

            var company = await GetCompany(capitan.CompanyId.ToString());

            company.Capitans.Add(capitan);

            await data.SaveChangesAsync();
        }

        public async Task<bool> IsDriver(string userId)
        {


            if (await data.TruckDrivers.AnyAsync(x => x.Id.ToString() == userId))
            {
                return true;
            }


            else if (await data.Pilots.AnyAsync(x => x.Id.ToString() == userId))
            {
                return true;
            }


            else if (await data.Machinists.AnyAsync(x => x.Id.ToString() == userId))
            {
                return true;
            }


            else if (await data.Capitans.AnyAsync(x => x.Id.ToString() == userId))
            {
                return true;
            }


            return false;
        }

        public async Task CreateStock(Stock stock, string companyId)
        {
            var company = await GetCompany(companyId);

            stock.Company = company;
            stock.CompanyId = company.Id;

            await data.Stocks.AddAsync(stock);

            await data.SaveChangesAsync();

            company.Stocks.Add(stock);

            await data.SaveChangesAsync();
        }

        public async Task<List<SmallStockExportDto>> GetAllStocksForCompany(string companyId)
        {
            return await data.Stocks.Where(x => x.CompanyId.ToString() == companyId)
                 .Select(x => new SmallStockExportDto()
                 {
                     Id = x.Id.ToString(),
                     Title = x.Title,
                     TypeOfTransport = x.PreferedTypeOfTransportId.ToString(),
                     Description = x.Description,
                     Gauge = x.Gauge

                 })
                 .ToListAsync();
        }

        public async Task<SmallStockExportDto> GetStock(string stockId)
        {
            return await data.Stocks.Where(x => x.Id.ToString() == stockId).Select(x => new SmallStockExportDto()
            {
                Description = x.Description,
                Id = x.Id.ToString(),
                Title = x.Title,
                TypeOfTransport = x.PreferedTypeOfTransportId.ToString(),
                Gauge = x.Gauge
            }).FirstAsync();
        }

        public async Task UpdateStock(SmallStockExportDto stock)
        {
            var stockFromDb = await data.Stocks.FirstAsync(x => x.Id.ToString() == stock.Id);
            if (stockFromDb == null)
            {
                throw new KeyNotFoundException($"Stock with ID {stock.Id} not found.");
            }


            stockFromDb.Gauge = stock.Gauge;
            stockFromDb.Title = stock.Title;
            stockFromDb.Description = stock.Description;
            string wayOfTransport = stock.TypeOfTransport.ToLower();


            if (wayOfTransport == "plane")
            {
                stockFromDb.PreferedTypeOfTransportId = (TypeOfVehicle)3;
            }

            else if (wayOfTransport == "ship")
            {
                stockFromDb.PreferedTypeOfTransportId = (TypeOfVehicle)1;
            }

            else if (wayOfTransport == "truck")
            {
                stockFromDb.PreferedTypeOfTransportId = (TypeOfVehicle)0;
            }

            else if (wayOfTransport == "train")
            {
                stockFromDb.PreferedTypeOfTransportId = (TypeOfVehicle)2;
            }


            // Save changes
            await data.SaveChangesAsync();


        }

        public async Task DeleteStock(string stockId)
        {
            var stockFromDb = await data.Stocks.FirstAsync(x => x.Id.ToString() == stockId);

            data.Remove(stockFromDb);

            await data.SaveChangesAsync();

        }

        public async Task CreateVehicle(Vehicle vehicle, string companyId)
        {
            data.Add(vehicle);
            await data.SaveChangesAsync();

            var company = data.Companies.First(x => x.Id.ToString() == companyId);

            if (vehicle is Plane)
            {
                company.Planes.Add((Plane)vehicle);
            }

            else if (vehicle is Ship)
            {
                company.Ships.Add((Ship)vehicle);
            }

            else if (vehicle is Truck)
            {
                company.Trucks.Add((Truck)vehicle);
            }

            else if (vehicle is Train)
            {
                company.Trains.Add((Train)vehicle);
            }

            else
            {
                throw new Exception();

            }

            await data.SaveChangesAsync();
        }

        public async Task<List<VehicleDtoExportSmall>> GetAllVehiclesByCompanyId(string companyId)
        {
            List<VehicleDtoExportSmall> trucks = await data.Trucks.Where(x => x.CompanyId.ToString() == companyId)
                     .Select(x => new VehicleDtoExportSmall
                     {
                         Brand = x.Brand,
                         ImagePath = $"https://localhost:7020/Vehicle/getVehicleImage?imagePath={Uri.EscapeDataString(x.ImagePath)}",
                         Model = x.Model,
                         Id = x.Id.ToString()
                     }).ToListAsync();

            List<VehicleDtoExportSmall> ships = await data.Ships.Where(x => x.CompanyId.ToString() == companyId)
                    .Select(x => new VehicleDtoExportSmall
                    {
                        Brand = x.Brand,
                        ImagePath = $"https://localhost:7020/Vehicle/getVehicleImage?imagePath={Uri.EscapeDataString(x.ImagePath)}",
                        Model = x.Model,
                        Id = x.Id.ToString()
                    }).ToListAsync();


            List<VehicleDtoExportSmall> planes = await data.Planes.Where(x => x.CompanyId.ToString() == companyId)
                    .Select(x => new VehicleDtoExportSmall
                    {
                        Brand = x.Brand,
                        ImagePath = $"https://localhost:7020/Vehicle/getVehicleImage?imagePath={Uri.EscapeDataString(x.ImagePath)}",
                        Model = x.Model,
                        Id = x.Id.ToString()
                    }).ToListAsync();

            List<VehicleDtoExportSmall> trains = await data.Trains.Where(x => x.CompanyId.ToString() == companyId)
                    .Select(x => new VehicleDtoExportSmall
                    {
                        Brand = x.Brand,
                        ImagePath = $"https://localhost:7020/Vehicle/getVehicleImage?imagePath={Uri.EscapeDataString(x.ImagePath)}",
                        Model = x.Model,
                        Id = x.Id.ToString()
                    }).ToListAsync();

            trucks.AddRange(ships);
            trucks.AddRange(trains);
            trucks.AddRange(planes);

            return trucks;
        }

        public async Task<VehicleDtoBigExport> GetDetailedInfoForVehicleById(string vehicleId)
        {
            VehicleDtoBigExport vehicleDto = null;

            Train train = await data.Trains.FirstOrDefaultAsync(x => x.Id.ToString() == vehicleId);

            if (train != null)
            {
                vehicleDto = new VehicleDtoBigExport()
                {
                    Brand = train.Brand,
                    IsDriving = train.IsDriving,
                    Id = train.Id.ToString(),
                    ImagePath = $"https://localhost:7020/Vehicle/getVehicleImage?imagePath={Uri.EscapeDataString(train.ImagePath)}",
                    LoadCapacity = train.LoadCapacity,
                    Model = train.Model,
                    RegistrationNumber = train.RegistrationNumber,
                    ReservoirCapacity = train.ReservoirCapacity,
                    TravelledKm = train.TravelledKm
                };

                return vehicleDto;
            }

            Truck truck = await data.Trucks.FirstOrDefaultAsync(x => x.Id.ToString() == vehicleId);


            if (truck != null)
            {
                vehicleDto = new VehicleDtoBigExport()
                {
                    Brand = truck.Brand,
                    IsDriving = truck.IsDriving,
                    Id = truck.Id.ToString(),
                    ImagePath = $"https://localhost:7020/Vehicle/getVehicleImage?imagePath={Uri.EscapeDataString(truck.ImagePath)}",
                    LoadCapacity = truck.LoadCapacity,
                    Model = truck.Model,
                    RegistrationNumber = truck.RegistrationNumber,
                    ReservoirCapacity = truck.ReservoirCapacity,
                    TravelledKm = truck.TravelledKm
                };

                return vehicleDto;
            }


            Ship ship = await data.Ships.FirstOrDefaultAsync(x => x.Id.ToString() == vehicleId);

            if (ship != null)
            {
                vehicleDto = new VehicleDtoBigExport()
                {
                    Brand = ship.Brand,
                    IsDriving = ship.IsDriving,
                    Id = ship.Id.ToString(),
                    ImagePath = $"https://localhost:7020/Vehicle/getVehicleImage?imagePath={Uri.EscapeDataString(ship.ImagePath)}",
                    LoadCapacity = ship.LoadCapacity,
                    Model = ship.Model,
                    RegistrationNumber = ship.RegistrationNumber,
                    ReservoirCapacity = ship.ReservoirCapacity,
                    TravelledKm = ship.TravelledKm
                };

                return vehicleDto;
            }

            Plane plane = await data.Planes.FirstOrDefaultAsync(x => x.Id.ToString() == vehicleId);

            if (plane != null)
            {
                vehicleDto = new VehicleDtoBigExport()
                {
                    Brand = plane.Brand,
                    IsDriving = plane.IsDriving,
                    Id = plane.Id.ToString(),
                    ImagePath = $"https://localhost:7020/Vehicle/getVehicleImage?imagePath={Uri.EscapeDataString(plane.ImagePath)}",
                    LoadCapacity = plane.LoadCapacity,
                    Model = plane.Model,
                    RegistrationNumber = plane.RegistrationNumber,
                    ReservoirCapacity = plane.ReservoirCapacity,
                    TravelledKm = plane.TravelledKm
                };

                return vehicleDto;
            }

            return vehicleDto;

        }

        public async Task UpdateVehicle(VehicleDtoImport vehicleDto)
        {
            string filePath = null;

            string directoryPath = Path.Combine("D:", "Kaloyan", "Stock Managment System", "API", "Images");

            // Ensure the directory exists


            if (vehicleDto.Image != null)
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(vehicleDto.Image.FileName)}";
                filePath = Path.Combine(directoryPath, fileName);



                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vehicleDto.Image.CopyToAsync(stream);
                }
            }





            Train train = await data.Trains.FirstOrDefaultAsync(x => x.Id.ToString() == vehicleDto.Id);

            if (train != null)
            {
                train.Model = vehicleDto.Model;
                train.RegistrationNumber = vehicleDto.RegistrationNumber;
                train.ReservoirCapacity = vehicleDto.ReservoirCapacity;
                train.Brand = vehicleDto.Brand;
                train.LoadCapacity = vehicleDto.LoadCapacity;
                train.ImagePath = filePath != null ? filePath : train.ImagePath;
                train.TravelledKm = vehicleDto.TravelledKm;


            }

            Truck truck = await data.Trucks.FirstOrDefaultAsync(x => x.Id.ToString() == vehicleDto.Id);


            if (truck != null)
            {

                truck.Model = vehicleDto.Model;
                truck.RegistrationNumber = vehicleDto.RegistrationNumber;
                truck.ReservoirCapacity = vehicleDto.ReservoirCapacity;
                truck.Brand = vehicleDto.Brand;
                truck.LoadCapacity = vehicleDto.LoadCapacity;
                truck.ImagePath = filePath != null ? filePath : truck.ImagePath;
                truck.TravelledKm = vehicleDto.TravelledKm;




            }


            Ship ship = await data.Ships.FirstOrDefaultAsync(x => x.Id.ToString() == vehicleDto.Id);

            if (ship != null)
            {

                ship.Model = vehicleDto.Model;
                ship.RegistrationNumber = vehicleDto.RegistrationNumber;
                ship.ReservoirCapacity = vehicleDto.ReservoirCapacity;
                ship.Brand = vehicleDto.Brand;
                ship.LoadCapacity = vehicleDto.LoadCapacity;
                ship.ImagePath = filePath != null ? filePath : ship.ImagePath;
                ship.TravelledKm = vehicleDto.TravelledKm;


            }

            Plane plane = await data.Planes.FirstOrDefaultAsync(x => x.Id.ToString() == vehicleDto.Id);

            if (plane != null)
            {

                plane.Model = vehicleDto.Model;
                plane.RegistrationNumber = vehicleDto.RegistrationNumber;
                plane.ReservoirCapacity = vehicleDto.ReservoirCapacity;
                plane.Brand = vehicleDto.Brand;
                plane.LoadCapacity = vehicleDto.LoadCapacity;
                plane.ImagePath = filePath != null ? filePath : plane.ImagePath;
                plane.TravelledKm = vehicleDto.TravelledKm;

            }



            await data.SaveChangesAsync();

        }


    }
}

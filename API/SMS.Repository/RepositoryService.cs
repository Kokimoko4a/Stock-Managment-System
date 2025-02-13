
namespace SMS.Repository
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using SMS.Data;
    using SMS.Dtos.Company;
    using SMS.Dtos.Driver;
    using SMS.Dtos.Order;
    using SMS.Dtos.Stock;
    using SMS.Dtos.Vehicles;
    using SMS.Models;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Xml.Schema;

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

        public async Task DeleteVehicle(string vehicleId)
        {
            Truck truck = await data.Trucks.FirstOrDefaultAsync(x => x.Id.ToString() == vehicleId);

            if (truck != null)
            {
                data.Trucks.Remove(truck);
                await data.SaveChangesAsync();
                return;
            }


            Ship ship = await data.Ships.FirstOrDefaultAsync(x => x.Id.ToString() == vehicleId);


            if (ship != null)
            {
                data.Ships.Remove(ship);
                await data.SaveChangesAsync();
                return;
            }


            Train train = await data.Trains.FirstOrDefaultAsync(x => x.Id.ToString() == vehicleId);



            if (train != null)
            {
                data.Trains.Remove(train);
                await data.SaveChangesAsync();
                return;
            }


            Plane plane = await data.Planes.FirstOrDefaultAsync(x => x.Id.ToString() == vehicleId);


            if (plane != null)
            {
                data.Planes.Remove(plane);
                await data.SaveChangesAsync();
                return;
            }




        }

        public async Task AssignVehicleToDriver(string driverEmail, string vehicleId)
        {
            string driverId = await data.Users.Where(x => x.Email == driverEmail).Select(x => x.Id.ToString()).FirstAsync();

            Truck truck = await data.Trucks.Include(x => x.Driver).FirstOrDefaultAsync(x => x.Id.ToString() == vehicleId);

            if (truck != null)
            {
                TruckDriver truckDriver = await data.TruckDrivers.Include(x => x.Vehicle).FirstOrDefaultAsync(x => x.Id.ToString() == driverId)!;


                truck.Driver = truckDriver;
                truck.DriverId = truckDriver.Id;

                truckDriver.Vehicle = truck;
                truckDriver.VehicleId = truck.Id;

                await data.SaveChangesAsync();
            }


            Ship ship = await data.Ships.Include(x => x.Driver).FirstOrDefaultAsync(x => x.Id.ToString() == vehicleId);


            if (ship != null)
            {
                Capitan capitan = await data.Capitans.Include(x => x.Vehicle).FirstOrDefaultAsync(x => x.Id.ToString() == driverId)!;


                ship.Driver = capitan;
                ship.DriverId = capitan.Id;

                capitan.Vehicle = ship;
                capitan.VehicleId = ship.Id;

                await data.SaveChangesAsync();
            }


            Train train = await data.Trains.Include(x => x.Driver).FirstOrDefaultAsync(x => x.Id.ToString() == vehicleId);



            if (train != null)
            {

                Machinist machinist = await data.Machinists.Include(x => x.Vehicle).FirstOrDefaultAsync(x => x.Id.ToString() == driverId)!;


                train.Driver = machinist;
                train.DriverId = machinist.Id;

                machinist.Vehicle = train;
                machinist.VehicleId = train.Id;

                await data.SaveChangesAsync();
            }


            Plane plane = await data.Planes.Include(x => x.Pilot).FirstOrDefaultAsync(x => x.Id.ToString() == vehicleId);


            if (plane != null)
            {
                Pilot pilot = await data.Pilots.Include(x => x.Vehicle).FirstOrDefaultAsync(x => x.Id.ToString() == driverId)!;


                plane.Pilot = pilot;
                plane.DriverId = pilot.Id;

                pilot.Vehicle = plane;
                pilot.VehicleId = plane.Id;

                await data.SaveChangesAsync();
            }
        }

        public async Task CreateTruckOrder(TruckOrder order, string driverEmail, List<string> stocks)
        {

            // Step 1: Get Driver ID
            string? driverId = await data.Users
                .Where(x => x.Email == driverEmail)
                .Select(x => x.Id.ToString())
                .FirstOrDefaultAsync();

            if (driverId == null)
            {
                throw new InvalidOperationException($"No user found with email {driverEmail}.");
            }

            // Step 2: Get TruckDriver
            TruckDriver? driverTruckFullModel = await data.TruckDrivers
                .FirstOrDefaultAsync(x => x.Id.ToString() == driverId);

            if (driverTruckFullModel == null)
            {
                throw new InvalidOperationException($"No truck driver found with ID {driverId}.");
            }

            // Step 3: Get Truck
            Truck? truck = await data.Trucks
                .Include(x => x.Driver)
                .Include(x => x.Company)
                .ThenInclude(x => x.TruckOrders)
                .FirstOrDefaultAsync(x => x.DriverId.ToString() == driverId);

            if (truck == null)
            {
                throw new InvalidOperationException($"No truck found for driver with ID {driverId}.");
            }

            // Step 4: Set Order Relationships
            order.Company = truck.Company;
            order.Driver = driverTruckFullModel;
            order.DriverId = driverTruckFullModel.Id;  // No need for casting
            order.Vehicle = truck;
            order.VehicleId = truck.Id;

            // Step 5: Ensure `order.Stocks` is Initialized
            order.Stocks ??= new List<Stock>();

            // Step 6: Add Stocks to Order
            foreach (var item in stocks)
            {
                Stock? stock = await data.Stocks.FirstOrDefaultAsync(x => x.Id.ToString() == item);
                if (stock != null)
                {
                    order.Stocks.Add(stock);
                }
            }

            // Step 7: Assign Order to Truck and Driver
            truck.Order = order;
            truck.OrderId = order.Id;
            driverTruckFullModel.Order = order;
            driverTruckFullModel.OrderId = order.Id;

            // Step 8: Add Order to Database and Save Once
            await data.TruckOrders.AddAsync(order);
            await data.SaveChangesAsync();


        }

        public async Task CreatePlaneOrder(PlaneOrder order, string driverEmail, List<string> stocks)
        {
            string driverId = await data.Users.Where(x => x.Email == driverEmail).Select(x => x.Id.ToString()).FirstAsync();

            Pilot? pilotFullModel = await data.Pilots.FirstOrDefaultAsync(x => x.Id.ToString() == driverId);



            Plane? plane = await data.Planes.Include(x => x.Pilot).Include(x => x.Company).ThenInclude(x => x.PlaneOrders).FirstOrDefaultAsync(x => x.DriverId.ToString() == driverId);

            order.Company = plane.Company;
            order.Driver = plane.Pilot;
            order.DriverId = (Guid)plane.DriverId;
            order.Vehicle = plane;
            order.VehicleId = plane.Id;


            plane.Order = order;
            plane.OrderId = order.Id;


            pilotFullModel.OrderId = order.Id;
            pilotFullModel.Order = order;


            foreach (var item in stocks)
            {
                order.Stocks.Add(await data.Stocks.FirstAsync(x => x.Id.ToString() == item));
            }


            await data.PlaneOrders.AddAsync(order);

            await data.SaveChangesAsync();

            plane.Company.PlaneOrders.Add(order);

            await data.SaveChangesAsync();
        }

        public async Task CreateShipOrder(ShipOrder order, string driverEmail, List<string> stocks)
        {
            string driverId = await data.Users.Where(x => x.Email == driverEmail).Select(x => x.Id.ToString()).FirstAsync();

            Capitan? capitanFullModel = await data.Capitans.FirstOrDefaultAsync(x => x.Id.ToString() == driverId);


            Ship? ship = await data.Ships.Include(x => x.Driver).Include(x => x.Company).ThenInclude(x => x.ShipOrders).FirstOrDefaultAsync(x => x.DriverId.ToString() == driverId);

            order.Company = ship.Company;
            order.Driver = ship.Driver;
            order.DriverId = (Guid)ship.DriverId;
            order.Vehicle = ship;
            order.VehicleId = ship.Id;


            ship.Order = order;
            ship.OrderId = order.Id;


            capitanFullModel.OrderId = order.Id;
            capitanFullModel.Order = order;


            foreach (var item in stocks)
            {
                order.Stocks.Add(await data.Stocks.FirstAsync(x => x.Id.ToString() == item));
            }


            await data.ShipOrders.AddAsync(order);

            await data.SaveChangesAsync();

            ship.Company.ShipOrders.Add(order);

            await data.SaveChangesAsync();
        }

        public async Task CreateTrainOrder(TrainOrder order, string driverEmail, List<string> stocks)
        {
            string driverId = await data.Users.Where(x => x.Email == driverEmail).Select(x => x.Id.ToString()).FirstAsync();


            Machinist? machinistFullModel = await data.Machinists.FirstOrDefaultAsync(x => x.Id.ToString() == driverId);

            Train? train = await data.Trains.Include(x => x.Driver).Include(x => x.Company).ThenInclude(x => x.TrainOrders).FirstOrDefaultAsync(x => x.DriverId.ToString() == driverId);

            order.Company = train.Company;
            order.Driver = train.Driver;
            order.DriverId = (Guid)train.DriverId;
            order.Vehicle = train;
            order.VehicleId = train.Id;

            train.Order = order;
            train.OrderId = order.Id;


            machinistFullModel.OrderId = order.Id;
            machinistFullModel.Order = order;


            foreach (var item in stocks)
            {
                order.Stocks.Add(await data.Stocks.FirstAsync(x => x.Id.ToString() == item));
            }


            await data.TrainOrders.AddAsync(order);

            await data.SaveChangesAsync();

            train.Company.TrainOrders.Add(order);

            await data.SaveChangesAsync();
        }

        public async Task<List<SmallOrderExportDto>> GetOrdersByCompanyId(string companyId)
        {
            List<SmallOrderExportDto> exportDtosTrain = new List<SmallOrderExportDto>();

            exportDtosTrain = await data.TrainOrders.Include(x => x.Vehicle).Where(x => x.ComapanyId.ToString() == companyId).Select(x => new SmallOrderExportDto()
            {
                Destination = x.Destination,
                Id = x.Id,
                StartPoint = x.StartPoint,
                Title = x.Title,
                TypeTransport = "Rail",
                Vehicle = x.Vehicle.Brand + " " + x.Vehicle.Model,

            }).ToListAsync();




            List<SmallOrderExportDto> exportDtosTruck = new List<SmallOrderExportDto>();

            exportDtosTruck = await data.TruckOrders.Include(x => x.Vehicle).Where(x => x.ComapanyId.ToString() == companyId).Select(x => new SmallOrderExportDto()
            {
                Destination = x.Destination,
                Id = x.Id,
                StartPoint = x.StartPoint,
                Title = x.Title,
                TypeTransport = "Land",
                Vehicle = x.Vehicle.Brand + " " + x.Vehicle.Model,

            }).ToListAsync();




            List<SmallOrderExportDto> exportDtosPlane = new List<SmallOrderExportDto>();

            exportDtosPlane = await data.PlaneOrders.Include(x => x.Vehicle).Where(x => x.ComapanyId.ToString() == companyId).Select(x => new SmallOrderExportDto()
            {
                Destination = x.Destination,
                Id = x.Id,
                StartPoint = x.StartPoint,
                Title = x.Title,
                TypeTransport = "Air",
                Vehicle = x.Vehicle.Brand + " " + x.Vehicle.Model,

            }).ToListAsync();






            List<SmallOrderExportDto> exportDtosShips = new List<SmallOrderExportDto>();

            exportDtosShips = await data.ShipOrders.Include(x => x.Vehicle).Where(x => x.ComapanyId.ToString() == companyId).Select(x => new SmallOrderExportDto()
            {
                Destination = x.Destination,
                Id = x.Id,
                StartPoint = x.StartPoint,
                Title = x.Title,
                TypeTransport = "Water",
                Vehicle = x.Vehicle.Brand + " " + x.Vehicle.Model,

            }).ToListAsync();


            exportDtosTruck.AddRange(exportDtosShips);
            exportDtosTruck.AddRange(exportDtosTrain);
            exportDtosTruck.AddRange(exportDtosPlane);

            return exportDtosTruck;
        }


        public async Task<OrderDtoBigExport> GetDetailedOrderById(string orderId)
        {


            TrainOrder? trainOrder = await data.TrainOrders.Include(x => x.Vehicle).Include(x => x.Stocks).FirstOrDefaultAsync(x => x.Id.ToString() == orderId);


            if (trainOrder != null)
            {
                ApplicationUser? driver = await data.Users.FirstOrDefaultAsync(x => x.Id == trainOrder.DriverId);


                OrderDtoBigExport orderDto = new OrderDtoBigExport()
                {
                    Description = trainOrder.Description,
                    Destination = trainOrder.Destination,
                    DriverEmail = driver.Email,
                    DriverNames = driver.FirstName + " " + driver.LastName,
                    Id = trainOrder.Id,
                    StartPoint = trainOrder.StartPoint,
                    Stocks = trainOrder.Stocks.Select(x => x.Title).ToList(),
                    Title = trainOrder.Title,
                    TypeTransport = "Rail",
                    VehicleBrand = trainOrder.Vehicle.Brand,
                    VehicleModel = trainOrder.Vehicle.Model,
                };


                return orderDto;

            }






            TruckOrder? truckOrder = await data.TruckOrders.Include(x => x.Vehicle).Include(x => x.Stocks).FirstOrDefaultAsync(x => x.Id.ToString() == orderId);


            if (truckOrder != null)
            {
                ApplicationUser? driver = await data.Users.FirstOrDefaultAsync(x => x.Id == truckOrder.DriverId);


                OrderDtoBigExport orderDto = new OrderDtoBigExport()
                {
                    Description = truckOrder.Description,
                    Destination = truckOrder.Destination,
                    DriverEmail = driver.Email,
                    DriverNames = driver.FirstName + " " + driver.LastName,
                    Id = truckOrder.Id,
                    StartPoint = truckOrder.StartPoint,
                    Stocks = truckOrder.Stocks.Select(x => x.Title).ToList(),
                    Title = truckOrder.Title,
                    TypeTransport = "Land",
                    VehicleBrand = truckOrder.Vehicle.Brand,
                    VehicleModel = truckOrder.Vehicle.Model,
                };


                return orderDto;

            }













            PlaneOrder? planeOrder = await data.PlaneOrders.Include(x => x.Vehicle).Include(x => x.Stocks).FirstOrDefaultAsync(x => x.Id.ToString() == orderId);


            if (planeOrder != null)
            {
                ApplicationUser? driver = await data.Users.FirstOrDefaultAsync(x => x.Id == planeOrder.DriverId);


                OrderDtoBigExport orderDto = new OrderDtoBigExport()
                {
                    Description = planeOrder.Description,
                    Destination = planeOrder.Destination,
                    DriverEmail = driver.Email,
                    DriverNames = driver.FirstName + " " + driver.LastName,
                    Id = planeOrder.Id,
                    StartPoint = planeOrder.StartPoint,
                    Stocks = planeOrder.Stocks.Select(x => x.Title).ToList(),
                    Title = planeOrder.Title,
                    TypeTransport = "Air",
                    VehicleBrand = planeOrder.Vehicle.Brand,
                    VehicleModel = planeOrder.Vehicle.Model,
                };


                return orderDto;

            }











            ShipOrder? shipOrder = await data.ShipOrders.Include(x => x.Vehicle).Include(x => x.Stocks).FirstOrDefaultAsync(x => x.Id.ToString() == orderId);


            if (shipOrder != null)
            {
                ApplicationUser? driver = await data.Users.FirstOrDefaultAsync(x => x.Id == shipOrder.DriverId);


                OrderDtoBigExport orderDto = new OrderDtoBigExport()
                {
                    Description = shipOrder.Description,
                    Destination = shipOrder.Destination,
                    DriverEmail = driver.Email,
                    DriverNames = driver.FirstName + " " + driver.LastName,
                    Id = shipOrder.Id,
                    StartPoint = shipOrder.StartPoint,
                    Stocks = shipOrder.Stocks.Select(x => x.Title).ToList(),
                    Title = shipOrder.Title,
                    TypeTransport = "Rail",
                    VehicleBrand = shipOrder.Vehicle.Brand,
                    VehicleModel = shipOrder.Vehicle.Model,
                };


                return orderDto;

            }





            throw new Exception("Vehicle id not found!!!");
        }

        public async Task<List<DriverSmallExportDto>> GetDriversForCompany(string companyId)
        {

            Company company = await data.Companies.Include(x => x.TruckDrivers).Include(x => x.Pilots).Include(x => x.Capitans)
                .Include(x => x.Machinists).FirstAsync(x => x.Id.ToString() == companyId);

            var truckDrivers = company.TruckDrivers.Select(x => x.Id).ToList();

            var machinists = company.Machinists.Select(x => x.Id).ToList();

            var pilots = company.Pilots.Select(x => x.Id).ToList();

            var capitans = company.Capitans.Select(x => x.Id).ToList();


            truckDrivers.AddRange(machinists);
            truckDrivers.AddRange(pilots);
            truckDrivers.AddRange(capitans);



            List<DriverSmallExportDto> driverExportDtos = data.Users.Where(x => truckDrivers.Contains(x.Id)).Select(x => new DriverSmallExportDto()
            {

                Email = x.Email,
                Names = x.FirstName + " " + x.LastName,
                Id = x.Id.ToString()
            }).ToList();

            return driverExportDtos;

        }

        public async Task<DriverBigExportDto> GetDetailsForDriverByCompanyId(string driverId)
        {
            DriverBigExportDto driverBigExportDto = new DriverBigExportDto();

            ApplicationUser? user =  await data.Users.FirstOrDefaultAsync(x => x.Id.ToString() == driverId);

            Machinist? machinist = await data.Machinists.Include(x => x.Vehicle).Include(x => x.Order).ThenInclude(x => x.Stocks).FirstOrDefaultAsync(x => x.Id.ToString() == driverId);


            if (machinist != null)
            {
                driverBigExportDto.OrderInfo = $"From {machinist.Order.StartPoint} To {machinist.Order.Description}" +
                    $" Stocks: {string.Join(',' , machinist.Order.Stocks.Select(x => x.Title))}";

                driverBigExportDto.Id = machinist.Id;
                driverBigExportDto.VehicleInfo = $"{machinist.Vehicle.Brand} {machinist.Vehicle.Model}";
                driverBigExportDto.Email = user.Email;
                driverBigExportDto.Age = user.Age;
                driverBigExportDto.Names = $"{user.FirstName} {user.LastName}";

                return driverBigExportDto;

            }






            TruckDriver? truckDriver = await data.TruckDrivers.Include(x => x.Vehicle).Include(x => x.Order).ThenInclude(x => x.Stocks).FirstOrDefaultAsync(x => x.Id.ToString() == driverId);



            if (truckDriver != null)
            {
                driverBigExportDto.OrderInfo = $"From {truckDriver.Order.StartPoint} To {truckDriver.Order.Description}" +
                    $" Stocks: {string.Join(',', truckDriver.Order.Stocks.Select(x => x.Title))}";

                driverBigExportDto.Id = truckDriver.Id;
                driverBigExportDto.VehicleInfo = $"{truckDriver.Vehicle.Brand} {truckDriver.Vehicle.Model}";
                driverBigExportDto.Email = user.Email;
                driverBigExportDto.Age = user.Age;
                driverBigExportDto.Names = $"{user.FirstName} {user.LastName}";

                return driverBigExportDto;

            }






            Pilot? pilot = await data.Pilots.Include(x => x.Vehicle).Include(x => x.Order).ThenInclude(x => x.Stocks).FirstOrDefaultAsync(x => x.Id.ToString() == driverId);



            if (pilot != null)
            {
                driverBigExportDto.OrderInfo = $"From {pilot.Order.StartPoint} To {pilot.Order.Description}" +
                    $" Stocks: {string.Join(',', pilot.Order.Stocks.Select(x => x.Title))}";

                driverBigExportDto.Id = pilot.Id;
                driverBigExportDto.VehicleInfo = $"{pilot.Vehicle.Brand} {pilot.Vehicle.Model}";
                driverBigExportDto.Email = user.Email;
                driverBigExportDto.Age = user.Age;
                driverBigExportDto.Names = $"{user.FirstName} {user.LastName}";

                return driverBigExportDto;

            }





            Capitan? capitan = await data.Capitans.Include(x => x.Vehicle).Include(x => x.Order).ThenInclude(x => x.Stocks).FirstOrDefaultAsync(x => x.Id.ToString() == driverId);


            if (capitan != null)
            {
                driverBigExportDto.OrderInfo = $"From {capitan.Order.StartPoint} To {capitan.Order.Description}" +
                    $" Stocks: {string.Join(',', capitan.Order.Stocks.Select(x => x.Title))}";

                driverBigExportDto.Id = capitan.Id;
                driverBigExportDto.VehicleInfo = $"{capitan.Vehicle.Brand} {capitan.Vehicle.Model}";
                driverBigExportDto.Email = user.Email;
                driverBigExportDto.Age = user.Age;
                driverBigExportDto.Names = $"{user.FirstName} {user.LastName}";

                return driverBigExportDto;

            }

            throw new Exception("The driver does not exist!");

        }

        public async Task<DriverDashboardDtoExport> GetDriverDashboardInfoForDriverByDriverId(string driverId)
        {
            DriverDashboardDtoExport driverDashboardDtoExport = new DriverDashboardDtoExport();


            TruckDriver? truckDriver = await  data.TruckDrivers.Include(x => x.Order).ThenInclude(x => x.Stocks).Include(x => x.Vehicle)
                                                        .FirstOrDefaultAsync(x => x.Id.ToString() == driverId);


            if (truckDriver != null)
            {
                driverDashboardDtoExport.OrderProducts = string.Join(", ", truckDriver.Order.Stocks.Select(x => x.Title));

                driverDashboardDtoExport.StartPoint = truckDriver.Order.StartPoint;

                driverDashboardDtoExport.Destination = truckDriver.Order.Destination;

                driverDashboardDtoExport.OrderTitle = truckDriver.Order.Title;

                driverDashboardDtoExport.VehicleBrand = truckDriver.Vehicle.Brand;

                driverDashboardDtoExport.VehicleModel = truckDriver.Vehicle.Model;

           

                
                return driverDashboardDtoExport;
            }






            Pilot? pilot = await data.Pilots.Include(x => x.Order).ThenInclude(x => x.Stocks).Include(x => x.Vehicle)
                                                        .FirstOrDefaultAsync(x => x.Id.ToString() == driverId);


            if (pilot != null)
            {
                driverDashboardDtoExport.OrderProducts = string.Join(", ", pilot.Order.Stocks.Select(x => x.Title));

                driverDashboardDtoExport.StartPoint = pilot.Order.StartPoint;

                driverDashboardDtoExport.Destination = pilot.Order.Destination;

                driverDashboardDtoExport.OrderTitle = pilot.Order.Title;

                driverDashboardDtoExport.VehicleBrand = pilot.Vehicle.Brand;

                driverDashboardDtoExport.VehicleModel = pilot.Vehicle.Model;

            

                return driverDashboardDtoExport;
            }





            Machinist? machinist = await data.Machinists.Include(x => x.Order).ThenInclude(x => x.Stocks).Include(x => x.Vehicle)
                                                        .FirstOrDefaultAsync(x => x.Id.ToString() == driverId);


            if (machinist != null)
            {
                driverDashboardDtoExport.OrderProducts = string.Join(", ", machinist.Order.Stocks.Select(x => x.Title));

                driverDashboardDtoExport.StartPoint = machinist.Order.StartPoint;

                driverDashboardDtoExport.Destination = machinist.Order.Destination;

                driverDashboardDtoExport.OrderTitle = machinist.Order.Title;

                driverDashboardDtoExport.VehicleBrand = machinist.Vehicle.Brand;

                driverDashboardDtoExport.VehicleModel = machinist.Vehicle.Model;

             

                return driverDashboardDtoExport;
            }





            Capitan? capitan = await data.Capitans.Include(x => x.Order).ThenInclude(x => x.Stocks).Include(x => x.Vehicle)
                                                        .FirstOrDefaultAsync(x => x.Id.ToString() == driverId);


            if (capitan != null)
            {
                driverDashboardDtoExport.OrderProducts = string.Join(", ", capitan.Order.Stocks.Select(x => x.Title));

                driverDashboardDtoExport.StartPoint = capitan.Order.StartPoint;

                driverDashboardDtoExport.Destination = capitan.Order.Destination;

                driverDashboardDtoExport.OrderTitle = capitan.Order.Title;

                driverDashboardDtoExport.VehicleBrand = capitan.Vehicle.Brand;

                driverDashboardDtoExport.VehicleModel = capitan.Vehicle.Model;

             

                return driverDashboardDtoExport;
            }


            throw new Exception("Driver not found!");
        }

        public async Task SetVehicleCoorinates(VehicleCoordinatesDtoImport vehicleCoordinatesDtoImport, string driverId)
        {
            double longtitude = vehicleCoordinatesDtoImport.Longitude;

            double latitude = vehicleCoordinatesDtoImport.Latitude;

          

            Truck? truck = await data.Trucks.FirstOrDefaultAsync(x => x.DriverId.ToString() == driverId);

            if (truck != null)
            {

                truck.Latitude = latitude;
                truck.Longtitude = longtitude;

                await data.SaveChangesAsync();
                return;
            }


            Ship? ship = await data.Ships.FirstOrDefaultAsync(x => x.DriverId.ToString() == driverId);


            if (ship != null)
            {
                ship.Latitude = latitude;
                ship.Longtitude = longtitude;

                await data.SaveChangesAsync();
                return;
            }


            Train? train = await data.Trains.FirstOrDefaultAsync(x => x.DriverId.ToString() == driverId);



            if (train != null)
            {

                train.Latitude = latitude;
                train.Longtitude = longtitude;
                

                await data.SaveChangesAsync();
                return;
            }


            Plane? plane = await data.Planes.FirstOrDefaultAsync(x => x.DriverId.ToString() == driverId);


            if (plane != null)
            {
                plane.Latitude = latitude;
                plane.Longtitude = longtitude;
              

                await data.SaveChangesAsync();
                return;
            }

            throw new Exception("Vehicle not found!");
        }
    }
}

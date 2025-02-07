

namespace SMS.Factory
{
    using SMS.Dtos.Company;
    using SMS.Dtos.Driver;
    using SMS.Dtos.Order;
    using SMS.Dtos.Stock;
    using SMS.Dtos.User;
    using SMS.Dtos.Vehicles;
    using SMS.Models;
    using SMS.Repository;
    using System.Threading.Tasks;
  

    public class FactoryService : IFactoryService
    {
        private IRepositoryService repository;

        public FactoryService(IRepositoryService repository)
        {
            this.repository = repository;
        }


        public ApplicationUser CreateUser(RegisterDTO registerDTO)
        {
            ApplicationUser user = new ApplicationUser()
            {
                LastName = registerDTO.LastName,
                FirstName = registerDTO.FirstName,
                Age = registerDTO.Age,

            };



            return user;
        }


        public Manager CreateManager(ApplicationUser applicationUser)
        {
            Manager manager = new Manager()
            {
                Id = applicationUser.Id
            };

            return manager;
        }

        public async Task CreateCompany(CompanyDto companyDto, Manager manager)
        {
            Company company = new Company()
            {
                Description = companyDto.Description,
                Title = companyDto.Title,
                Manager = manager,
                ManagerId = manager.Id
            };


            await repository.AddCompany(company);
        }

        public async Task CreateDriver(DriverImportDto driverImportDto)
        {
            string typeDriver = driverImportDto.TypeDriver.ToLower().Trim();

            if (driverImportDto == null)
            {
                throw new ArgumentException();
            }

            if (typeDriver == "captain")
            {
                Capitan capitan = new Capitan()
                {
                    Company = await repository.GetCompanyByTitle(driverImportDto.Title),
                    CompanyId = (await repository.GetCompanyByTitle(driverImportDto.Title)).Id,
                    Id = Guid.Parse(driverImportDto.Id)


                };

                await repository.AddCaptain(capitan);

            }



            else if (typeDriver == "machinist")
            {
                Machinist machinist = new Machinist()
                {
                    Company = await repository.GetCompanyByTitle(driverImportDto.Title),
                    CompanyId = (await repository.GetCompanyByTitle(driverImportDto.Title)).Id,
                    Id = Guid.Parse(driverImportDto.Id)
                };


                await repository.AddMachinist(machinist);
            }


            else if (typeDriver == "pilot")
            {
                Pilot pilot = new Pilot()
                {
                    Company = await repository.GetCompanyByTitle(driverImportDto.Title),
                    CompanyId = (await repository.GetCompanyByTitle(driverImportDto.Title)).Id,
                    Id = Guid.Parse(driverImportDto.Id)
                };


                await repository.AddPilot(pilot);

            }

            else if (typeDriver == "truck-driver")
            {
                TruckDriver truckDriver = new TruckDriver()
                {
                    Company = await repository.GetCompanyByTitle(driverImportDto.Title),
                    CompanyId = (await repository.GetCompanyByTitle(driverImportDto.Title)).Id,
                    Id = Guid.Parse(driverImportDto.Id)
                };



                await repository.AddTruckDriver(truckDriver);
            }

            else
            {
                throw new Exception();
            }


        }

        public async Task CreateStock(StockImportDto stockImportDto)
        {
            string typeTransport = stockImportDto.TransportType;

            /*        public const int TruckCode = 0;
        public const int ShipCode = 1;
        public const int TrainCode = 2;
        public const int PlaneCode = 3;*/


            Stock stock = new Stock()
            {
                Description = stockImportDto.Description,
                Gauge = stockImportDto.Gauge,
                Title = stockImportDto.Title,


            };

            if (typeTransport == "air")
            {
                stock.PreferedTypeOfTransportId = (TypeOfVehicle)3;
            }

            else if (typeTransport == "water")
            {
                stock.PreferedTypeOfTransportId = (TypeOfVehicle)1;
            }

            else if (typeTransport == "land")
            {
                stock.PreferedTypeOfTransportId = (TypeOfVehicle)0;
            }

            else if (typeTransport == "rail")
            {
                stock.PreferedTypeOfTransportId = (TypeOfVehicle)2;
            }

            await repository.CreateStock(stock, stockImportDto.CompanyId);
        }

        public async Task CreateVehicle(VehicleDtoImport vehicleDtoImport)
        {
            Vehicle vehicle = null;

            string typeVehicle = vehicleDtoImport.Type.ToLower();
            var company = await repository.GetCompany(vehicleDtoImport.CompanyId);

            string directoryPath = Path.Combine("D:", "Kaloyan", "Stock Managment System", "API", "Images");

            // Ensure the directory exists
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }



            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(vehicleDtoImport.Image.FileName)}";
            var filePath = Path.Combine(directoryPath, fileName);


         

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await vehicleDtoImport.Image.CopyToAsync(stream);
            }


       

            if (typeVehicle == "plane")
            {
                vehicle = new Plane()
                {
                    Brand = vehicleDtoImport.Brand,
                    Model = vehicleDtoImport.Model,
                    TravelledKm = vehicleDtoImport.TravelledKm,
                    Company = company,
                    CompanyId = Guid.Parse(vehicleDtoImport.CompanyId),
                    LoadCapacity = vehicleDtoImport.LoadCapacity,
                    RegistrationNumber = vehicleDtoImport.RegistrationNumber,
                    ReservoirCapacity = vehicleDtoImport.ReservoirCapacity,
                    ImagePath = filePath,

                };
            }

            else if (typeVehicle == "ship")
            {
                vehicle = new Ship()
                {
                    Brand = vehicleDtoImport.Brand,
                    Model = vehicleDtoImport.Model,
                    TravelledKm = vehicleDtoImport.TravelledKm,
                    Company = company,
                    CompanyId = Guid.Parse(vehicleDtoImport.CompanyId),
                    LoadCapacity = vehicleDtoImport.LoadCapacity,
                    RegistrationNumber = vehicleDtoImport.RegistrationNumber,
                    ReservoirCapacity = vehicleDtoImport.ReservoirCapacity,
                    ImagePath = filePath
                };
            }

            else if (typeVehicle == "truck")
            {
                vehicle = new Truck()
                {
                    Brand = vehicleDtoImport.Brand,
                    Model = vehicleDtoImport.Model,
                    TravelledKm = vehicleDtoImport.TravelledKm,
                    Company = company,
                    CompanyId = Guid.Parse(vehicleDtoImport.CompanyId),
                    LoadCapacity = vehicleDtoImport.LoadCapacity,
                    RegistrationNumber = vehicleDtoImport.RegistrationNumber,
                    ReservoirCapacity = vehicleDtoImport.ReservoirCapacity,
                    ImagePath = filePath
                };
            }

            else if (typeVehicle == "train")
            {
                vehicle = new Train()
                {
                    Brand = vehicleDtoImport.Brand,
                    Model = vehicleDtoImport.Model,
                    TravelledKm = vehicleDtoImport.TravelledKm,
                    Company = company,
                    CompanyId = Guid.Parse(vehicleDtoImport.CompanyId),
                    LoadCapacity = vehicleDtoImport.LoadCapacity,
                    RegistrationNumber = vehicleDtoImport.RegistrationNumber,
                    ReservoirCapacity = vehicleDtoImport.ReservoirCapacity,
                    ImagePath = filePath
                };
            }


            await repository.CreateVehicle(vehicle, vehicleDtoImport.CompanyId);

        }

        public async Task CreateOrder(OrderImportDto orderDto)
        {


            string typeTransport = orderDto.TypeOrder.ToLower();

            if (typeTransport == "truck")
            {
                TruckOrder truckOrder = new TruckOrder()
                {
                    Description = orderDto.Description,
                    ComapanyId = Guid.Parse(orderDto.ComapanyId),
                    Destination = orderDto.Destination,
                    StartPoint = orderDto.StartPoint,
                    Title = orderDto.Title,
                    //Set the driver and the vehicle and the company 
                   
                };

                await repository.CreateTruckOrder(truckOrder, orderDto.DriverEmail, orderDto.Stock);
            }

            else if (typeTransport == "train")
            {
                TrainOrder trainOrder = new TrainOrder()
                {
                    Description = orderDto.Description,
                    ComapanyId = Guid.Parse(orderDto.ComapanyId),
                    Destination = orderDto.Destination,
                    StartPoint = orderDto.StartPoint,
                    Title = orderDto.Title,
                    //Set the driver and the vehicle and the company 
                };

                await repository.CreateTrainOrder(trainOrder, orderDto.DriverEmail, orderDto.Stock);
            }


            else if (typeTransport == "plane")
            {
                PlaneOrder planeOrder = new PlaneOrder()
                {
                    Description = orderDto.Description,
                    ComapanyId = Guid.Parse(orderDto.ComapanyId),
                    Destination = orderDto.Destination,
                    StartPoint = orderDto.StartPoint,
                    Title = orderDto.Title,
                    //Set the driver and the vehicle and the company 
                };


                await repository.CreatePlaneOrder(planeOrder, orderDto.DriverEmail, orderDto.Stock);

            }

            else if (typeTransport == "ship")
            {
                ShipOrder shipOrder = new ShipOrder()
                {
                    Description = orderDto.Description,
                    ComapanyId = Guid.Parse(orderDto.ComapanyId),
                    Destination = orderDto.Destination,
                    StartPoint = orderDto.StartPoint,
                    Title = orderDto.Title,
                    //Set the driver and the vehicle and the company 
                };

                await repository.CreateShipOrder(shipOrder, orderDto.DriverEmail, orderDto.Stock);
            }
        }
    }

}


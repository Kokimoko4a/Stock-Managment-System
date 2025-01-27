

namespace SMS.Factory
{
    using SMS.Dtos.Company;
    using SMS.Dtos.Driver;
    using SMS.Dtos.Stock;
    using SMS.Dtos.User;
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
    }
}


namespace SMS.Repository
{
    using Microsoft.EntityFrameworkCore;
    using SMS.Data;
    using SMS.Dtos.Company;
    using SMS.Dtos.Stock;
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
                    Description = x.Description
                
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



            // Save changes
            await data.SaveChangesAsync();
        }
    }
}

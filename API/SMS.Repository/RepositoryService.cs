
namespace SMS.Repository
{
    using Microsoft.EntityFrameworkCore;
    using SMS.Data;
    using SMS.Dtos.Company;
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
    }
}

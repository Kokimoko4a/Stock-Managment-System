namespace SMS.Services
{
    using SMS.Dtos.Stock;
    using SMS.Factory;
    using SMS.Repository;
    using SMS.Services.Interfaces;
    using System.Threading.Tasks;

    public class StockService : IStockService
    {
        private readonly IFactoryService factoryService;
        private readonly IRepositoryService repositoryService;

        public StockService(IFactoryService factoryService, IRepositoryService repositoryService)
        {
            this.factoryService = factoryService;
            this.repositoryService = repositoryService;
        }

        public async Task CreateStock(StockImportDto stockImportDto)
        {
            await factoryService.CreateStock(stockImportDto);
        }

   
        public async Task<List<SmallStockExportDto>> GetAllStocksForCompany(string companyId)
        {
            return await repositoryService.GetAllStocksForCompany(companyId);
        }

        public async Task<SmallStockExportDto> GetStock(string stockId)
        {
           return await repositoryService.GetStock(stockId);
        }

        public async Task UpdateStock(SmallStockExportDto stock)
        {
           await repositoryService.UpdateStock(stock);
        }

        public async Task DeleteStock(string stockId)
        {
            await repositoryService.DeleteStock(stockId);
        }

    }
}

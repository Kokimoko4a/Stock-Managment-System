namespace SMS.Services
{
    using SMS.Dtos.Stock;
    using SMS.Factory;
    using SMS.Services.Interfaces;
    using System.Threading.Tasks;

    public class StockService : IStockService
    {
        private readonly IFactoryService factoryService;

        public StockService(IFactoryService factoryService)
        {
            this.factoryService = factoryService;
        }

        public async Task CreateStock(StockImportDto stockImportDto)
        {
            await factoryService.CreateStock(stockImportDto);
        }
    }
}

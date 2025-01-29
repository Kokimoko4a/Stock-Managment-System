
namespace SMS.Services.Interfaces
{
    using SMS.Dtos.Stock;

    public interface IStockService
    {
        public Task CreateStock( StockImportDto stockImportDto);

        public Task<List<SmallStockExportDto>> GetAllStocksForCompany(string companyId);

        public Task<SmallStockExportDto> GetStock(string stockId);

        public Task UpdateStock(SmallStockExportDto stock);
    }
}


namespace SMS.Services.Interfaces
{
    using SMS.Dtos.Stock;

    public interface IStockService
    {
        public Task CreateStock( StockImportDto stockImportDto);
    }
}

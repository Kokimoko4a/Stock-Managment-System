
namespace SMS.Services.Interfaces
{
    using SMS.Dtos.Order;

    public interface IOrderService
    {
        public Task CreateOrder(OrderImportDto orderDto);

        public Task<List<SmallOrderExportDto>> GetOrdersByCompanyId(string companyId);

        public Task<OrderDtoBigExport> GetDetailedOrderById(string orderId);
    }
}

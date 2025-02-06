
namespace SMS.Services.Interfaces
{
    using SMS.Dtos.Order;

    public interface IOrderService
    {
        public Task CreateOrder(OrderImportDto orderDto);
    }
}

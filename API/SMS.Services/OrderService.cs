
namespace SMS.Services
{
    using SMS.Dtos.Order;
    using SMS.Factory;
    using SMS.Repository;
    using SMS.Services.Interfaces;



    public class OrderService : IOrderService
    {
        private readonly IFactoryService factoryService;
        private readonly IRepositoryService repositoryService;

        public OrderService(IFactoryService factoryService, IRepositoryService repositoryService)
        {
            this.factoryService = factoryService;
            this.repositoryService = repositoryService;
        }

        public async Task CreateOrder(OrderImportDto orderDto)
        {
            await factoryService.CreateOrder(orderDto);
        }
    }
}

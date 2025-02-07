
namespace SMS.Services
{
    using SMS.Dtos.Order;
    using SMS.Factory;
    using SMS.Repository;
    using SMS.Services.Interfaces;
    using System.Collections.Generic;

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

     

        public async Task<List<SmallOrderExportDto>> GetOrdersByCompanyId(string companyId)
        {
          return await repositoryService.GetOrdersByCompanyId(companyId);
        }


        public async Task<OrderDtoBigExport> GetDetailedOrderById(string orderId)
        {
            return await repositoryService.GetDetailedOrderById(orderId);
        }
    }
}

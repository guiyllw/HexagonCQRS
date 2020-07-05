using Domain.Enums.Order;
using Domain.Exceptions.Order;
using Domain.Interfaces.Adapters;
using Domain.Interfaces.Ports;
using Domain.Models;
using System.Threading.Tasks;

namespace Domain.UseCases
{
    public class AdvanceOrderStatusUseCase : IAdvanceOrderStatusPort
    {
        private readonly IOrderAdapter orderAdapter;

        public AdvanceOrderStatusUseCase(IOrderAdapter orderAdapter)
        {
            this.orderAdapter = orderAdapter;
        }

        public async Task<Order> AdvanceOrderStatus(int orderNumber, Status status)
        {
            Order order = await orderAdapter.FindOrderByNumber(orderNumber);
            bool orderExists = order != null;

            if (!orderExists)
            {
                throw new OrderNotFoundException($"Order {orderNumber} not found");
            }

            return await orderAdapter.AdvanceOrderStatus(order, status);
        }
    }
}

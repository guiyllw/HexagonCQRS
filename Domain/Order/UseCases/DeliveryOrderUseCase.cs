using Domain.Order.Adapters;
using Domain.Order.Ports;
using System.Threading.Tasks;

namespace Domain.Order.UseCases
{
    public class DeliveryOrderUseCase : IDeliveryOrderPort
    {
        private readonly IFindOrderByNumberAdapter findOrderByNumberAdapter;
        private readonly IDeliveryOrderAdapter deliveryOrderAdapter;

        public DeliveryOrderUseCase(
            IFindOrderByNumberAdapter findOrderByNumberAdapter,
            IDeliveryOrderAdapter deliveryOrderAdapter)
        {
            this.findOrderByNumberAdapter = findOrderByNumberAdapter;
            this.deliveryOrderAdapter = deliveryOrderAdapter;
        }

        public async Task<bool> DeliveryOrderAsync(int orderNumber)
        {
            Models.Order order = await findOrderByNumberAdapter.FindByNumberAsync(orderNumber);

            return await deliveryOrderAdapter.DeliveryOrderAsync(order);
        }
    }
}

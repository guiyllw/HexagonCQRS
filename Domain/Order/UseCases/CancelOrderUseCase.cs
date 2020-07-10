using Domain.Order.Adapters;
using Domain.Order.Ports;
using System.Threading.Tasks;

namespace Domain.Order.UseCases
{
    public class CancelOrderUseCase : ICancelOrderPort
    {
        private readonly IFindOrderByNumberAdapter findOrderByNumberAdapter;
        private readonly ICancelOrderAdapter cancelOrderAdapter;

        public CancelOrderUseCase(
            IFindOrderByNumberAdapter findOrderByNumberAdapter,
            ICancelOrderAdapter cancelOrderAdapter)
        {
            this.findOrderByNumberAdapter = findOrderByNumberAdapter;
            this.cancelOrderAdapter = cancelOrderAdapter;
        }

        public async Task<bool> CancelOrderAsync(int orderNumber)
        {
            Models.Order order = await findOrderByNumberAdapter.FindByNumberAsync(orderNumber);

            return await cancelOrderAdapter.CancelOrderAsync(order);
        }
    }
}

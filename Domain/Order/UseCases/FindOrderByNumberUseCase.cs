using Domain.Order.Adapters;
using Domain.Order.Exceptions;
using Domain.Order.Ports;
using System.Threading.Tasks;

namespace Domain.Order.UseCases
{
    public class FindOrderByNumberUseCase : IFindOrderByNumberPort
    {
        private readonly IFindOrderByNumberAdapter findOrderByNumberAdapter;

        public FindOrderByNumberUseCase(IFindOrderByNumberAdapter findOrderAdapter)
        {
            this.findOrderByNumberAdapter = findOrderAdapter;
        }

        public async Task<Models.Order> FindByNumber(int orderNumber)
        {
            Models.Order order = await findOrderByNumberAdapter.FindByNumberAsync(orderNumber);

            var orderExists = order != null;
            if (!orderExists)
            {
                throw new OrderNotFoundException($"Order {orderNumber} not found");
            }

            return order;
        }
    }
}

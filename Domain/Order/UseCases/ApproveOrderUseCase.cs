using Domain.Order.Adapters;
using Domain.Order.Ports;
using System.Threading.Tasks;

namespace Domain.Order.UseCases
{
    public class ApproveOrderUseCase : IApproveOrderPort
    {
        private readonly IFindOrderByNumberAdapter findOrderByNumberAdapter;
        private readonly IApproveOrderAdapter approveOrderAdapter;

        public ApproveOrderUseCase(
            IFindOrderByNumberAdapter findOrderByNumberAdapter,
            IApproveOrderAdapter approveOrderAdapter)
        {
            this.findOrderByNumberAdapter = findOrderByNumberAdapter;
            this.approveOrderAdapter = approveOrderAdapter;
        }

        public async Task<bool> ApproveOrderAsync(int orderNumber)
        {
            Models.Order order = await findOrderByNumberAdapter.FindByNumberAsync(orderNumber);

            return await approveOrderAdapter.ApproveOrderAsync(order);
        }
    }
}

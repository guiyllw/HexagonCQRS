using Domain.Order.Adapters;
using Infrastructure.Order.Commands.ApproveOrder;
using Infrastructure.Order.Commands.CancelOrder;
using Infrastructure.Order.Commands.DeliveryOrder;
using Infrastructure.Order.Queries.FindOrderByNumber;
using MediatR;
using System.Threading.Tasks;

namespace Infrastructure.Order.Adapters
{
    public class OrderMemoryAdapter :
        IFindOrderByNumberAdapter,
        IApproveOrderAdapter,
        ICancelOrderAdapter,
        IDeliveryOrderAdapter
    {
        private readonly IMediator mediator;

        public OrderMemoryAdapter(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Domain.Order.Models.Order> FindByNumberAsync(int orderNumber)
        {
            return await mediator.Send(new FindOrderByNumberQuery(orderNumber));
        }

        public async Task<bool> ApproveOrderAsync(Domain.Order.Models.Order order)
        {
            return await mediator.Send(new ApproveOrderCommand(order.Number));
        }

        public async Task<bool> CancelOrderAsync(Domain.Order.Models.Order order)
        {
            return await mediator.Send(new CancelOrderCommand(order.Number));
        }

        public async Task<bool> DeliveryOrderAsync(Domain.Order.Models.Order order)
        {
            return await mediator.Send(new DeliveryOrderCommand(order.Number));
        }
    }
}

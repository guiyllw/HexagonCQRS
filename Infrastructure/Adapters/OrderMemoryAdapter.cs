using Domain.Enums.Order;
using Domain.Interfaces.Adapters;
using Domain.Models;
using Infrastructure.Commands.AdvanceOrderStatus;
using Infrastructure.Queries.FindOrderById;
using MediatR;
using System.Threading.Tasks;

namespace Infrastructure.Adapters
{
    public class OrderMemoryAdapter : IOrderAdapter
    {
        private readonly IMediator mediator;

        public OrderMemoryAdapter(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Order> AdvanceOrderStatus(Order order, Status status)
        {
            return await mediator.Send(new AdvanceOrderStatusCommand(order, status));
        }

        public async Task<Order> FindOrderByNumber(int number)
        {
            return await mediator.Send(new FindOrderByNumberQuery(number));
        }
    }
}

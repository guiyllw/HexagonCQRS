using Domain.Enums.Order;
using Domain.Models;
using MediatR;

namespace Infrastructure.Commands.AdvanceOrderStatus
{
    public class AdvanceOrderStatusCommand : IRequest<Order>
    {
        public Order Order { get; }
        public Status Status { get; }

        public AdvanceOrderStatusCommand(Order order, Status status)
        {
            Order = order;
            Status = status;
        }
    }
}

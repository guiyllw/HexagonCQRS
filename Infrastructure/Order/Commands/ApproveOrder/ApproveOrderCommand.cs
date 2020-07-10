using MediatR;

namespace Infrastructure.Order.Commands.ApproveOrder
{
    public class ApproveOrderCommand : IRequest<bool>
    {
        public int Number;

        public ApproveOrderCommand(int orderNumber)
        {
            Number = orderNumber;
        }
    }
}

using MediatR;

namespace Infrastructure.Order.Commands.CancelOrder
{
    public class CancelOrderCommand : IRequest<bool>
    {
        public int Number { get; set; }

        public CancelOrderCommand(int number)
        {
            Number = number;
        }
    }
}

using MediatR;

namespace Infrastructure.Order.Commands.DeliveryOrder
{
    public class DeliveryOrderCommand : IRequest<bool>
    {
        public int Number { get; set; }

        public DeliveryOrderCommand(int number)
        {
            Number = number;
        }
    }
}

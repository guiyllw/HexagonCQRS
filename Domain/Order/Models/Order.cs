using Domain.Order.Enums;

namespace Domain.Order.Models
{
    public class Order
    {
        public int Number { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.New;
    }
}

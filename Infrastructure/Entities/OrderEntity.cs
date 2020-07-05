using Domain.Models;

namespace Infrastructure.Entities
{
    public class OrderEntity : Order
    {
        public int Id { get; set; }

        public OrderEntity() : base()
        {
            Id = Number;
        }
    }
}

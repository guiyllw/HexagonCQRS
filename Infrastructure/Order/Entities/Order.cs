using Domain.Order.Enums;
using Infrastructure.Common.Entities;

namespace Infrastructure.Order.Entities
{
    public class Order : BaseEntity
    {
        private int number;

        //TODO: Remover
        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                Id = value;
                number = value;
            }
        }

        public OrderStatus Status { get; set; }
    }
}

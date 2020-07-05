using Domain.Enums.Order;
using Domain.Exceptions.Order;

namespace Domain.Models
{
    public class Order
    {
        private Status status;

        public Status Status
        {
            get
            {
                return status;
            }

            set
            {
                if (CanUpdateStatus(value))
                {
                    status = value;
                }
                else
                {
                    throw new StatusNotAllowedException($"Current status: {status}, new status: {value}");
                }
            }
        }

        public int Number { get; set; }

        public Order()
        {
            status = Status.New;
        }

        private bool CanUpdateStatus(Status newStatus)
        {
            var steps = (int)newStatus - (int)status;
            return status == newStatus || steps == 1;
        }
    }
}

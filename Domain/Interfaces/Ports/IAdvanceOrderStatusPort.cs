using Domain.Enums.Order;
using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Interfaces.Ports
{
    public interface IAdvanceOrderStatusPort
    {
        Task<Order> AdvanceOrderStatus(int orderNumber, Status status);
    }
}

using Domain.Enums.Order;
using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Interfaces.Adapters
{
    public interface IOrderAdapter
    {
        Task<Order> FindOrderByNumber(int number);

        Task<Order> AdvanceOrderStatus(Order order, Status status);
    }
}

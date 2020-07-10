using System.Threading.Tasks;

namespace Domain.Order.Adapters
{
    public interface ICancelOrderAdapter
    {
        Task<bool> CancelOrderAsync(Models.Order order);
    }
}

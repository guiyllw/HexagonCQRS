using System.Threading.Tasks;

namespace Domain.Order.Adapters
{
    public interface IDeliveryOrderAdapter
    {
        Task<bool> DeliveryOrderAsync(Models.Order order);
    }
}

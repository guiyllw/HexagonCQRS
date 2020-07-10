using System.Threading.Tasks;

namespace Domain.Order.Ports
{
    public interface IDeliveryOrderPort
    {
        Task<bool> DeliveryOrderAsync(int orderNumber);
    }
}

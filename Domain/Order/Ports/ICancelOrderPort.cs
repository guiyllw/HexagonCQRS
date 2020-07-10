using System.Threading.Tasks;

namespace Domain.Order.Ports
{
    public interface ICancelOrderPort
    {
        Task<bool> CancelOrderAsync(int orderNumber);
    }
}

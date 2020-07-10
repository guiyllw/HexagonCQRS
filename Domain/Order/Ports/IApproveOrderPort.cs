using System.Threading.Tasks;

namespace Domain.Order.Ports
{
    public interface IApproveOrderPort
    {
        Task<bool> ApproveOrderAsync(int orderNumber);
    }
}

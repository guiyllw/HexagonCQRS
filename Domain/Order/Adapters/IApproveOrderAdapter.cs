using System.Threading.Tasks;

namespace Domain.Order.Adapters
{
    public interface IApproveOrderAdapter
    {
        Task<bool> ApproveOrderAsync(Models.Order order);
    }
}

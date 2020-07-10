using System.Threading.Tasks;

namespace Domain.Order.Adapters
{
    public interface IFindOrderByNumberAdapter
    {
        Task<Models.Order> FindByNumberAsync(int orderNumber);
    }
}

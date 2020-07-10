using System.Threading.Tasks;

namespace Domain.Order.Ports
{
    public interface IFindOrderByNumberPort
    {
        Task<Models.Order> FindByNumber(int orderNumber);
    }
}

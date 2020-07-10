using MediatR;

namespace Infrastructure.Order.Queries.FindOrderByNumber
{
    public class FindOrderByNumberQuery : IRequest<Domain.Order.Models.Order>
    {
        public int Number { get; }

        public FindOrderByNumberQuery(int orderNumber)
        {
            Number = orderNumber;
        }
    }
}

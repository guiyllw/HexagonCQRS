using Domain.Models;
using MediatR;

namespace Infrastructure.Queries.FindOrderById
{
    public class FindOrderByNumberQuery : IRequest<Order>
    {
        public int Number { get; }

        public FindOrderByNumberQuery(int number)
        {
            Number = number;
        }
    }
}

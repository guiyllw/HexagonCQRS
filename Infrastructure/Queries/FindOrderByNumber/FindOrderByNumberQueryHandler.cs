using Arch.EntityFrameworkCore.UnitOfWork;
using Domain.Models;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Queries.FindOrderById
{
    public class FindOrderByNumberQueryHandler : IRequestHandler<FindOrderByNumberQuery, Order>
    {
        private readonly IUnitOfWork<ReadContext> unitOfWork;

        public FindOrderByNumberQueryHandler(IUnitOfWork<ReadContext> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Order> Handle(FindOrderByNumberQuery request, CancellationToken _)
        {
            OrderEntity order = await unitOfWork.GetRepository<OrderEntity>()
                .GetFirstOrDefaultAsync(predicate: orderEntity => orderEntity.Number == request.Number);

            return await Task.FromResult(order);
        }
    }
}

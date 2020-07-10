using Arch.EntityFrameworkCore.UnitOfWork;
using Domain.Order.Enums;
using Infrastructure.Common.Contexts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Order.Commands.CancelOrder
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, bool>
    {
        private readonly IUnitOfWork<WriteContext> unitOfWork;

        public CancelOrderCommandHandler(IUnitOfWork<WriteContext> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CancelOrderCommand request, CancellationToken _)
        {
            Entities.Order order = await unitOfWork.GetRepository<Entities.Order>()
                .GetFirstOrDefaultAsync(predicate: _order => _order.Number == request.Number);

            order.Status = OrderStatus.Canceled;

            unitOfWork.GetRepository<Entities.Order>()
                .Update(order);

            int affectedRows = await unitOfWork.SaveChangesAsync();

            return affectedRows > 0;
        }
    }
}

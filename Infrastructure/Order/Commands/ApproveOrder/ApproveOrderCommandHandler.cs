using Arch.EntityFrameworkCore.UnitOfWork;
using Domain.Order.Enums;
using Infrastructure.Common.Contexts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Order.Commands.ApproveOrder
{
    public class ApproveOrderCommandHandler : IRequestHandler<ApproveOrderCommand, bool>
    {
        private readonly IUnitOfWork<WriteContext> unitOfWork;

        public ApproveOrderCommandHandler(IUnitOfWork<WriteContext> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ApproveOrderCommand request, CancellationToken _)
        {
            Entities.Order order = await unitOfWork.GetRepository<Entities.Order>()
                .GetFirstOrDefaultAsync(predicate: _order => _order.Number == request.Number);

            order.Status = OrderStatus.Approved;

            unitOfWork.GetRepository<Entities.Order>()
                .Update(order);

            int affectedRows = await unitOfWork.SaveChangesAsync();

            return affectedRows > 0;
        }
    }
}

using Arch.EntityFrameworkCore.UnitOfWork;
using Domain.Order.Enums;
using Infrastructure.Common.Contexts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Order.Commands.DeliveryOrder
{
    public class DeliveryOrderCommandHandler : IRequestHandler<DeliveryOrderCommand, bool>
    {
        private readonly IUnitOfWork<WriteContext> unitOfWork;

        public DeliveryOrderCommandHandler(IUnitOfWork<WriteContext> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeliveryOrderCommand request, CancellationToken _)
        {
            Entities.Order order = await unitOfWork.GetRepository<Entities.Order>()
                .GetFirstOrDefaultAsync(predicate: _order => _order.Number == request.Number);

            order.Status = OrderStatus.Delivered;

            unitOfWork.GetRepository<Entities.Order>()
                .Update(order);

            int affectedRows = await unitOfWork.SaveChangesAsync();

            return affectedRows > 0;
        }
    }
}

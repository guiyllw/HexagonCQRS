using Arch.EntityFrameworkCore.UnitOfWork;
using Domain.Models;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Commands.AdvanceOrderStatus
{
    public class AdvanceOrderStatusCommandHandler : IRequestHandler<AdvanceOrderStatusCommand, Order>
    {
        private readonly IUnitOfWork<WriteContext> unitOfWork;

        public AdvanceOrderStatusCommandHandler(IUnitOfWork<WriteContext> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Order> Handle(AdvanceOrderStatusCommand request, CancellationToken _)
        {
            request.Order.Status = request.Status;

            unitOfWork.GetRepository<OrderEntity>().Update(request.Order as OrderEntity);

            unitOfWork.SaveChanges();

            return Task.FromResult(request.Order);
        }
    }
}

using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Infrastructure.Common.Contexts;
using Infrastructure.Order.Queries.FindOrderByNumber;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Order.Queries.FindOrderByName
{
    public class FindOrderByNumberQueryHandler : IRequestHandler<FindOrderByNumberQuery, Domain.Order.Models.Order>
    {
        private readonly IUnitOfWork<ReadContext> unitOfWork;
        private readonly IMapper mapper;

        public FindOrderByNumberQueryHandler(IUnitOfWork<ReadContext> unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Domain.Order.Models.Order> Handle(FindOrderByNumberQuery request, CancellationToken _)
        {
            Entities.Order order = await unitOfWork.GetRepository<Entities.Order>()
                .GetFirstOrDefaultAsync(predicate: _order => _order.Number == request.Number);

            return mapper.Map<Entities.Order, Domain.Order.Models.Order>(order);
        }
    }
}

using AutoMapper;

namespace WebApi.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Domain.Order.Models.Order, Infrastructure.Order.Entities.Order>().ReverseMap();
        }
    }
}

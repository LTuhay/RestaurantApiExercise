using AutoMapper;

namespace RestaurantReservationAPI.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<Entities.Order, Models.OrderDto>();
            CreateMap<Entities.Order, Models.OrderForUpdateDto>();
            CreateMap<Models.OrderDto, Models.OrderForUpdateDto>();
            CreateMap<Models.OrderCreateDto, Entities.Order>();
            CreateMap<Models.OrderForUpdateDto, Entities.Order>();
        }
    }
}

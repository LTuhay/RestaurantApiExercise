using AutoMapper;

namespace RestaurantReservationAPI.Profiles
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile() 
        {
            CreateMap<Entities.OrderItem, Models.OrderItemDto>();
            CreateMap<Entities.OrderItem, Models.OrderItemForUpdateDto>();
            CreateMap<Models.OrderItemDto, Models.OrderItemForUpdateDto>();
            CreateMap<Models.OrderItemCreateDto, Entities.OrderItem>();
            CreateMap<Models.OrderItemForUpdateDto, Entities.OrderItem>();
        }
    }
}

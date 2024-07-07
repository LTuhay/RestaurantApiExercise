using AutoMapper;

namespace RestaurantReservationAPI.Profiles
{
    public class MenuItemProfile : Profile
    {
        public MenuItemProfile() 
        {
            CreateMap<Entities.MenuItem, Models.MenuItemDto>();
            CreateMap<Entities.MenuItem, Models.MenuItemCreateDto>();
            CreateMap<Models.MenuItemDto, Models.MenuItemForUpdateDto>();
            CreateMap<Models.MenuItemCreateDto, Entities.MenuItem>();
            CreateMap<Models.MenuItemForUpdateDto, Entities.MenuItem>();
            CreateMap<Entities.MenuItem, Models.MenuItemForUpdateDto>();
        }
    }
}

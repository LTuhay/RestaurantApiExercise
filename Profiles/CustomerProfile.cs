using AutoMapper;

namespace RestaurantReservationAPI.Profiles
{
    public class CustomerProfile : Profile
    {

        public CustomerProfile() 
        {
            CreateMap<Entities.Customer, Models.CustomerDto>();
            CreateMap<Entities.Customer, Models.CustomerForUpdateDto>();
            CreateMap<Models.CustomerDto, Models.CustomerForUpdateDto>();
            CreateMap<Models.CustomerCreateDto, Entities.Customer>();
            CreateMap<Models.CustomerForUpdateDto, Entities.Customer>();
        }

    }
}

using AutoMapper;

namespace RestaurantReservationAPI.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile() 
        {
            CreateMap<Entities.Reservation, Models.ReservationDto>();
            CreateMap<Entities.Reservation, Models.ReservationForUpdateDto>();
            CreateMap<Models.ReservationDto, Models.ReservationForUpdateDto>();
            CreateMap<Models.ReservationCreateDto, Entities.Reservation>();
            CreateMap<Models.ReservationForUpdateDto, Entities.Reservation>();
        }
    }
}

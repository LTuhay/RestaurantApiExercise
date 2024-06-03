using AutoMapper;

namespace RestaurantReservationAPI.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile() 
        {
            CreateMap<Entities.Employee, Models.EmployeeDto>();
            CreateMap<Entities.Employee, Models.EmployeeForUpdateDto>();
            CreateMap<Models.EmployeeDto, Models.EmployeeForUpdateDto>();
            CreateMap<Models.EmployeeCreateDto, Entities.Employee>();
            CreateMap<Models.EmployeeForUpdateDto, Entities.Employee>();
        }
    }
}

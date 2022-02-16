using APPLICATION.Hotel.Models;
using AutoMapper;

namespace APPLICATION.Hotel.Mapping
{
    public class HotelMappingProfile : Profile
    {
        public HotelMappingProfile()
        {
            CreateMap<DOMAIN.Entities.Hotel, HotelResponseDto>();
        }
    }
}
